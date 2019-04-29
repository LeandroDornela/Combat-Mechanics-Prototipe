/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 10/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Classe abstrata para os estados do jogador. Cuidado ao utilizar os metodos de
/// update e start nas classes derivadas.
/// </summary>
public class PlayerState : MonoBehaviour
{
    // Struct para retornar e repassar um novo estado e um input.
    public struct NewState
    {
        // Novo estado.
        public PlayerState state;
        // Input que saiu do ultimo estado.
        public string input;
        // Verdadeiro se o novo estado não deve esperar a animação atual terminar.
        public bool instant;

        public NewState(PlayerState st, string ip, bool inst)
        {
            state = st;
            input = ip;
            instant = inst;
        }

        public void  SetNewState(PlayerState st, string ip, bool inst)
        {
            state = st;
            input = ip;
            instant = inst;
        }

        public void Reset()
        {
            state = null;
            input = null;
            instant = false;
        }
    }


    [Tooltip("Verdadeiro se for o estado inicial.")]
    [SerializeField]
    private bool initialState = false;
    // Novo estado.
    protected NewState newState = new NewState(null, null, false);
    // Script geral do player.
    protected Player player;
    // Gerenciador de estados.
    protected PlayerStateManager stateManager;


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        player = GetComponent<Player>();
        stateManager = GetComponent<PlayerStateManager>();

        // Inicia os estados.
        StartState();
        // Desabilita todos os scripts dos estados exeto o estado inicial.
        if(!initialState) enabled = false;
    }


    /// <summary>
    /// Metodo Start que deve ser usado nas classes extendidas.
    /// </summary>
    protected virtual void StartState()
    {

    }


    /// <summary>
    /// Faz tarefas criticas antes de entrar no estado.
    /// </summary>
    public void PrepareToEnterState()
    {
        newState.Reset();
    }

    
    /// <summary>
    /// Ações tomadas ao entrar no estado.
    /// </summary>
    public virtual void EnterState(NewState lastState, PlayerInput playerInput)
    {

    }


    /// <summary>
    /// Executa o update e retorna null para continuar no mesmo estado ou retorna o novo estado.
    /// </summary>
    /// <param name="playerInput"></param>
    /// <returns></returns>
    public virtual NewState UpdateState(PlayerInput playerInput)
    {
        return newState;
    }


    /// <summary>
    /// Executa o fixed update e retorna null para continuar no mesmo estado ou retorna o novo estado.
    /// </summary>
    /// <param name="playerInput"></param>
    /// <returns></returns>
    public virtual NewState FixedUpdateState(PlayerInput playerInput)
    {
        return newState;
    }


    /// <summary>
    /// Executa as ações para sair do estado.
    /// </summary>
    public virtual void ExitState()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerInput"></param>
    public virtual NewState InputHandle(PlayerInput playerInput)
    {
        return newState;
    }


    /// <summary>
    /// Classe virtual para aplicar dano ao jogador.
    /// </summary>
    public virtual bool TakeDamage(float damage)
    {
        return false;
    }


    /// <summary>
    /// Use estes metodos para verificar colisão.
    /// </summary>
    public virtual void TriggerEnter(Collider col)
    {

    }


    public virtual void TriggerExit(Collider col)
    {

    }


    public virtual void CollisionEnter(Collision col)
    {

    }
}
