/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Estado do jogador rolando. Nesse estado o jogador não tem controle de movimento~mas não
/// pode ser atinguido por disparos de blasters inimigos.
/// </summary>
public class Row : PlayerState
{
    [Tooltip("Velocidade Inicial de rolagem.")]
    [SerializeField]
    private float rowSpeed;
    [Tooltip("desaceleração do jogador.")]
    [SerializeField]
    private float slowdown = 10;
    [Tooltip("Trigger da animação de rolagem.")]
    [SerializeField]
    private string animRowTrigger;

    private Rigidbody rb;
    private Animator anim;
    // Vetor auxiliar para direção.
    private Vector3 dir = Vector3.zero;
    // Contador de tempo para a animação.
    private float timer = 0;
    // Hash da animação de rolar.
    private int animRowHash = 0;
    // Duração da animação de rolar.
    private float animationClipLength = 0;
    

    /// <summary>
    /// 
    /// </summary>
    protected override void StartState()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        animRowHash = Animator.StringToHash(animRowTrigger);
	}


    /// <summary>
    /// 
    /// </summary>
    public override void EnterState(NewState lastState, PlayerInput playerInput)
    {
        anim.SetTrigger(animRowHash);

        if (Input.GetAxis(playerInput.horizontal_L) != 0 || Input.GetAxis(playerInput.vertical_L) != 0)
        {
            dir = new Vector3(Input.GetAxis(playerInput.horizontal_L), 0, Input.GetAxis(playerInput.vertical_L));
        }
        else
        {
            dir = transform.forward;
        }

        rb.velocity = dir.normalized * rowSpeed;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerInput"></param>
    /// <returns></returns>
    public override NewState UpdateState(PlayerInput playerInput)
    {
        animationClipLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length / anim.GetCurrentAnimatorStateInfo(0).speed;

        // Sai do estado quando terminar a animação.
        if (timer >= animationClipLength)
        {
            newState.SetNewState(stateManager.Moving(), null, false);
            return newState;
        }

        RotateRbToVelocity();
        timer += Time.deltaTime;

        return base.UpdateState(playerInput);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerInput"></param>
    /// <returns></returns>
    public override NewState FixedUpdateState(PlayerInput playerInput)
    {
        float newMag = rb.velocity.magnitude - slowdown * Time.deltaTime;

        if (newMag <= 0)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = rb.velocity.normalized * (rb.velocity.magnitude - slowdown * Time.deltaTime);
        }

        return base.FixedUpdateState(playerInput);
    }


    /// <summary>
    /// 
    /// </summary>
    public override void ExitState()
    {
        timer = 0;
        base.ExitState();
    }


    /// <summary>
    /// Rotaciona instantaneamente o jogador na direção do movimento.
    /// </summary>
    void RotateRbToVelocity()
    {
        if (rb.velocity != Vector3.zero) this.rb.rotation = Quaternion.LookRotation(rb.velocity);
    }
}
