/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Estado para o inimigo atirando.
/// </summary>
public class EnemyShooting : EnemyState
{
    [Header("Mariner")]

    [Tooltip("Prefab do tiro.")]
    [SerializeField]
    private Shoot shootPrefab;
    
    [Header("Flame")]
    [SerializeField]
    [Tooltip("Colisor das chamas.")]
    private GameObject flamesCollider;
    [SerializeField]
    [Tooltip("Particulas das chamas.")]
    private ParticleSystem flamesPartiles;

    [Header("Heavy")]
    [Tooltip("Prefab da granada.")]
    [SerializeField]
    private Granade granadePrefab;
    [SerializeField]
    [Tooltip("Trigger da animação de lançamento de explosivo.")]
    private string animHeavyTrigger;
    [SerializeField]
    [Tooltip("Tempo para o inimigo jogar uma granada.")]
    private float granadeLaunchTime = 0;
    [SerializeField]
    [Tooltip("Origem da granada.")]
    private Transform grenadeOrigin;

    [Header("Shoot")]

    [SerializeField]
    [Tooltip("Trigger da animação de disparo.")]
    private string shootAnimTrigger;
    [Tooltip("Luz de disparo.")]
    [SerializeField]
    private GameObject shootLight;
    [Tooltip("Duração da luz do disparo.")]
    [SerializeField]
    private float shootLightDuration = 0.1f;
    [SerializeField]
    [Tooltip("Origem do disparo.")]
    private Transform shootOrigin;

    [Header("Misc")]
    
    
    [SerializeField]
    [Tooltip("Pasta para colocar os tiros.")]
    private Transform shotsFolder;

    [SerializeField]
    // Alvo atual.
    private GameObject target;
    // Contador de tempo para a luz de disparo.
    private float shootLightTimer = 0;
    // Hash da animação de disparo.
    private int shootAnimHash = 0;
    // Hash da animação de granada.
    private int heavyAnimHash = 0;
    // Corpo rigido do inimigo.
    private Rigidbody rb;
    // Animator do inimigo.
    private Animator anim;
    // Inteligencia do inimigo.
    private EnemyStormAI enemyAI;

    private NavMeshAgent agent;

    private float grenadeTimer = 0;

    private float animationClipLength = 0;

    private bool lauchingGranade = false;

    
    /// <summary>
    /// Inicializa o estado quando o inimigo é criado.
    /// </summary>
    public override void StartState()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyStormAI>();
        shootAnimHash = Animator.StringToHash(shootAnimTrigger);
        heavyAnimHash = Animator.StringToHash(animHeavyTrigger);
        agent = GetComponent<NavMeshAgent>();
	}


    /// <summary>
    /// Entra no estado.
    /// </summary>
    public override void EnterState(NewState lastState)
    {
        anim.SetTrigger(shootAnimHash);
        agent.enabled = false;
        base.EnterState(lastState);
    }

    
    /// <summary>
    /// Atualiza o estado.
    /// </summary>
    /// <returns></returns>
    public override NewState UpdateState()
    {
        UpdateLight();

        LookToTarget();

        if(enemy.GetClass() == Enemy.EnemyClass.Heavy)
        {
            if(lauchingGranade == true)
            {
                animationClipLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length / anim.GetCurrentAnimatorStateInfo(0).speed;

                if(grenadeTimer >= animationClipLength)
                {
                    lauchingGranade = false;
                }
            }
            else
            {
                if (!enemyAI.IsOnTargetPosition(1))
                {
                    newState.SetNewState(stateManager.Moving());
                    return newState;
                }
            }
            

            if (grenadeTimer >= granadeLaunchTime)
            {
                anim.SetTrigger(heavyAnimHash);
                lauchingGranade = true;
                grenadeTimer = 0;
            }
            else
            {
                grenadeTimer += Time.deltaTime;
            }
        }
        else
        {
            if (!enemyAI.IsOnTargetPosition(1))
            {
                newState.SetNewState(stateManager.Moving());
                return newState;
            }
        }

        return base.UpdateState();
	}


    /// <summary>
    /// Executa as ações necessarias para sair do estado.
    /// </summary>
    public override void ExitState()
    {
        shootLight.SetActive(false);
    }


    /// <summary>
    /// Rotaciona o inimigo na direção do alvo.
    /// </summary>
    public void LookToTarget()
    {
        Vector3 dir = (enemyAI.GetTarget().position - transform.position).normalized;
        //Debug.DrawRay(shootOrigin.transform.position, dir*10, Color.red, 1);
        dir = Vector3.RotateTowards(transform.forward, dir, 10 * Time.deltaTime, 0f);
        rb.rotation = Quaternion.LookRotation(dir);
    }


    /// <summary>
    /// 
    /// </summary>
    void UpdateLight()
    {
        if (shootLightTimer >= shootLightDuration)
        {
            shootLight.SetActive(false);
        }

        shootLightTimer += Time.deltaTime;
    }


    /// <summary>
    /// Realiza um disparo de blaster.
    /// </summary>
    void Shoot()
    {
        // Verifica se ja não haverá troca de estados para evitar que a luz de disparo continue acesa.
        if (newState.state == null)
        {
            shootLight.SetActive(true);
            shootLightTimer = 0;
            Shoot clone = Instantiate(shootPrefab, shootOrigin.transform.position, Quaternion.LookRotation(shootOrigin.transform.forward));
            clone.Init(shootOrigin.transform.forward);
            clone.transform.parent = shotsFolder;
        }
    }


    /// <summary>
    /// Lança uma granada.
    /// </summary>
    void Granade()
    {
        Granade clone = Instantiate(granadePrefab, grenadeOrigin.transform.position, Quaternion.LookRotation(grenadeOrigin.transform.forward));
        clone.Init(grenadeOrigin.transform.forward);
        clone.transform.parent = shotsFolder;
    }


    /// <summary>
    /// Dispara com o lança chamas.
    /// </summary>
    void Burn()
    {

    }


    /// <summary>
    /// Trata a colisão.
    /// </summary>
    public override void TriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player_saber")
        {
            newState.SetNewState(stateManager.EnemyDying());
        }
        else if(col.gameObject.tag == "Force_lightning")
        {
            col.gameObject.GetComponentInParent<ForceLightning>().AddExperience(enemy.GetXP());
            newState.SetNewState(stateManager.EnemyDying());
        }
    }
}
