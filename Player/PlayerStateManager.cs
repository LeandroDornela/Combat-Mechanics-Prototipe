/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 10/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Classe para grenciar os estados do jogador. Possui referencias estaticas
/// para os estados. Chama os metodos de update do estado atual.
/// </summary>
[RequireComponent(typeof(SaberCombat))]
[RequireComponent(typeof(ForceCombat))]
[RequireComponent(typeof(Moving))]
[RequireComponent(typeof(Interacting))]
[RequireComponent(typeof(Defending))]
[RequireComponent(typeof(Damage))]
[RequireComponent(typeof(Dying))]
[RequireComponent(typeof(Row))]
public class PlayerStateManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Estado inicial")]
    private PlayerState initialState;

    // Estado atual.
    private PlayerState state;
    // Estado anterior.
    private PlayerState oldState;
    // Lista de inputs customizados.
    private PlayerInput playerInput;

    // Estados
    private Moving moving;
    private SaberCombat saberCombat;
    private ForceCombat forceCombat;
    private Interacting interacting;
    private Defending defending;
    private Damage damage;
    private Dying dying;
    private Row row;


    #region Gets para os estados

    public Moving Moving()
    {
        return moving;
    }

    public SaberCombat SaberCombat()
    {
        return saberCombat;
    }

    public ForceCombat ForceCombat()
    {
        return forceCombat;
    }

    public Interacting Interacting()
    {
        return interacting;
    }

    public Defending Defending()
    {
        return defending;
    }

    public Damage Damage()
    {
        return damage;
    }

    public Dying Dying()
    {
        return dying;
    }

    public Row Row()
    {
        return row;
    }

    public PlayerState ActiveState()
    {
        return state;
    }

    #endregion

    
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        // Atribui o estado inicial caso seja valido.
        if (initialState == null || initialState.gameObject != gameObject)
        {
            Debug.LogError("No initial state.");
        }
        else
        {
            state = initialState;
            moving = GetComponent<Moving>();
            saberCombat = GetComponent<SaberCombat>();
            forceCombat = GetComponent<ForceCombat>();
            interacting = GetComponent<Interacting>();
            defending = GetComponent<Defending>();
            damage = GetComponent<Damage>();
            dying = GetComponent<Dying>();
            row = GetComponent<Row>();
            playerInput = GetComponent<PlayerInput>();
        }
    }

    
    /// <summary>
    /// 
    /// </summary>
    void Update ()
    {
        // Recebe o novo estado ou vazio caso não haja transição.
        GoToNewState(state.UpdateState(playerInput));
	}


    /// <summary>
    /// TODO: Evitar de usar input no fixed update, talvez tirar o playerinput passado resolva.
    /// </summary>
    void FixedUpdate()
    {
        // Recebe o novo estado ou vazio caso não haja transição.
        GoToNewState(state.FixedUpdateState(playerInput));
    }


    /// <summary>
    /// Atribui o novo estado caso exista.
    /// </summary>
    void GoToNewState(PlayerState.NewState newState)
    {
        if (newState.state != null)
        {
            state.ExitState();
            state.enabled = false;
            oldState = state;
            state = newState.state;
            state.enabled = true;
            newState.state = oldState;
            state.PrepareToEnterState();
            state.EnterState(newState, playerInput);
        }
    }


    /// <summary>
    /// Chama as respectivas funções de colisão quando o jogador colide com algo.
    /// Cada estado deve implementar a sua colisão mas usar os metodos da clase de estados base.
    /// </summary>
    void OnTriggerEnter(Collider col)
    {
        state.TriggerEnter(col);
    }

    void OnTriggerExit(Collider col)
    {
        state.TriggerExit(col);
    }

    void OnCollisionEnter(Collision col)
    {
        state.CollisionEnter(col);
    }


    /// <summary>
    /// 
    /// </summary>
    public void ResetState()
    {
        GoToNewState(new PlayerState.NewState(initialState, null, true));
    }
}
