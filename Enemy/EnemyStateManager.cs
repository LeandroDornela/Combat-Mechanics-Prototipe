/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;

/// <summary>
/// Classe para grenciar os estados dos inimigos. Possui referencias estaticas
/// para os estados. Chama os metodos de update do estado atual.
/// </summary>
[RequireComponent(typeof(EnemyMoving))]
[RequireComponent(typeof(EnemyShooting))]
[RequireComponent(typeof(EnemyDying))]
public class EnemyStateManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Estado inicial")]
    private EnemyState initialState;

    // Estado atual.
    private EnemyState state;
    // Estado anterior.
    private EnemyState oldState;

    // Estados
    private EnemyMoving moving;
    private EnemyShooting shooting;
    private EnemyDying dying;


    #region Gets para os estados

    public EnemyMoving Moving()
    {
        return moving;
    }

    public EnemyShooting EnemyShooting()
    {
        return shooting;
    }

    public EnemyDying EnemyDying()
    {
        return dying;
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
            moving = GetComponent<EnemyMoving>();
            shooting = GetComponent<EnemyShooting>();
            dying = GetComponent<EnemyDying>();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        // Recebe o novo estado ou vazio caso não haja transição.
        GoToNewState(state.UpdateState());
    }


    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        // Recebe o novo estado ou vazio caso não haja transição.
        GoToNewState(state.FixedUpdateState());
    }


    /// <summary>
    /// Atribui o novo estado caso exista.
    /// </summary>
    void GoToNewState(EnemyState.NewState newState)
    {
        if (newState.state != null)
        {
            state.ExitState();
            oldState = state;
            state = newState.state;
            state.PrepareToEnterState();
            newState.state = oldState;
            state.EnterState(newState);
        }
    }


    /// <summary>
    /// Chama as respectivas funções de colisão quando o inimigo colide com algo.
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
}
