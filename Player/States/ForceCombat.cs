/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 10/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Estado de combate usando a força. Determina qual habilidade e animação de
/// habilidade usar de acordo com o input.
/// </summary>
public class ForceCombat : Combat
{
    [SerializeField]
    private ForceCure forceCure;
    [SerializeField]
    private ForceDash forceDash;
    [SerializeField]
    private ForceLightning forceLightning;
    [SerializeField]
    private ForceMindTrick forceMindTrick;
    [SerializeField]
    private ForcePull forcePull;
    [SerializeField]
    private ForcePush forcePush;
    [SerializeField]
    private ForceReflex forceReflex;
    [SerializeField]
    private ForceSaber forceSaber;
    

    [Tooltip("Trigger para animação de uso da força.")]
    public string forceAnimTrigger;
    [Tooltip("Trigger para animação de uso da habilidade ForceSaber.")]
    public string saberForceAnimTrigger;
    
    // Animator do player.
    protected Animator anim;
    // Rigidbody do player.
    protected Rigidbody rb;
    // Hash do trigger para animação de uso da força.
    protected int forceAnimTriggerHash = 0;
    // Hash do trigger para animação de uso da habilidade ForceSaber.
    protected int saberForceAnimTriggerHash = 0;
    // Contador de tempo da habilidade sendo usada.
    protected float timer = 0;
    // Duração da habilidade ajustada a velociade de execução do estado do animator.
    protected float animationClipLength = 0;

    private Skill activeSkill;
    private bool use = true;
    

    /// <summary>
    /// Inicializa o estado e as habilidades.
    /// </summary>
    protected override void StartState()
    {
        this.forceAnimTriggerHash = Animator.StringToHash(forceAnimTrigger);
        this.saberForceAnimTriggerHash = Animator.StringToHash(saberForceAnimTrigger);
        this.anim = this.GetComponent<Animator>();
        this.rb = this.GetComponent<Rigidbody>();

        forceCure.Initialize(gameObject);
        forceDash.Initialize(gameObject);
        forceLightning.Initialize(gameObject);
        forceMindTrick.Initialize(gameObject);
        forcePull.Initialize(gameObject);
        forcePush.Initialize(gameObject);
        forceReflex.Initialize(gameObject);
        forceSaber.Initialize(gameObject);

        for (int i = 0; i < GameData.unlockedSkills.Count; i++)
        {
            UnlockSkill(GameData.unlockedSkills[i]);
        }
    }


    /// <summary>
    /// Ações ao entrar no estado.
    /// </summary>
    public override void EnterState(NewState lastState, PlayerInput playerInput)
    {
        // Seleciona a skill e animação de acordo com o input.
        if (lastState.input == playerInput.button_Y)
        {
            activeSkill = forceCure;
        }
        else if (lastState.input == playerInput.vertical_R)
        {
            activeSkill = forceDash;
        }
        else if (lastState.input == playerInput.button_X)
        {
            activeSkill = forceLightning;
        }
        else if (lastState.input == playerInput.down)
        {
            activeSkill = forceMindTrick;
        }
        else if (lastState.input == playerInput.right)
        {
            activeSkill = forcePull;
        }
        else if (lastState.input == playerInput.button_B)
        {
            activeSkill = forcePush;
        }
        else if (lastState.input == playerInput.left)
        {
            activeSkill = forceReflex;
        }
        else if (lastState.input == playerInput.button_A)
        {
            activeSkill = forceSaber;
        }

        if (player.GetForce() >= activeSkill.forceConsume && activeSkill.learned)
        {
            this.anim.SetTrigger(forceAnimTriggerHash);
            activeSkill.gameObject.SetActive(true);
            activeSkill.Activate();
            use = true;
        }
        else
        {
            use = false;
        }

        rb.velocity = Vector3.zero;

        timer = 0;
    }


    /// <summary>
    /// Atulaliza o estado e retorna um estado novo caso sejam compridos os requisitos.
    /// </summary>
    public override NewState UpdateState(PlayerInput playerInput)
    {
        if(use)
        {
            animationClipLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length / anim.GetCurrentAnimatorStateInfo(0).speed;

            if (timer >= animationClipLength)
            {
                newState.SetNewState(stateManager.Moving(), null, false);
                return newState;
            }

            timer += Time.deltaTime;

            return InputHandle(playerInput);
        }
        else
        {
            newState.SetNewState(stateManager.Moving(), null, false);
            return newState;
        }
    }


    /// <summary>
    /// Desbloqueia a habilidade de acordo com a string dada.
    /// </summary>
    public void UnlockSkill(string skill)
    {
        switch (skill)
        {
            case "ForceCure":
                forceCure.learned = true;
                break;
            case "ForceDash":
                forceDash.learned = true;
                break;
            case "ForceLightning":
                forceLightning.learned = true;
                break;
            case "ForceMindTrick":
                forceMindTrick.learned = true;
                break;
            case "ForcePull":
                forcePull.learned = true;
                break;
            case "ForcePush":
                forcePush.learned = true;
                break;
            case "ForceReflex":
                forceReflex.learned = true;
                break;
            case "ForceSaber":
                forceSaber.learned = true;
                break;
        }

        player.inGameUI.DisplayNotification(skill + " learned.");
    }
}
