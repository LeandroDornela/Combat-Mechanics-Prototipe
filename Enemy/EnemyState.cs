/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Classe mãe para os estados dos inimigos. Cuidado ao utilizar os metodos de
/// update e start nas classes derivadas.
/// </summary>
public class EnemyState : MonoBehaviour
{
    // Struct para retornar e repassar um novo estado e um input.
    public struct NewState
    {
        public EnemyState state;

        public NewState(EnemyState st)
        {
            state = st;
        }

        public void SetNewState(EnemyState st)
        {
            state = st;
        }

        public void Reset()
        {
            state = null;
        }
    }


    // Novo estado.
    protected NewState newState = new NewState(null);
    // Administrador de estados.
    protected EnemyStateManager stateManager;
    // Script do inimigo.
    protected Enemy enemy;


    void Start()
    {
        stateManager = GetComponent<EnemyStateManager>();
        enemy = GetComponent<Enemy>();
        StartState();
    }


    /// <summary>
    /// Metodo de inicialisação para as classes derivadas.
    /// </summary>
    public virtual void StartState()
    {

    }


    /// <summary>
    /// Executa ações obrigatorias antes deentrar no estado.
    /// </summary>
    public void PrepareToEnterState()
    {
        newState.Reset();
    }


    /// <summary>
    /// Ações tomadas ao entrar no estado.
    /// </summary>
    public virtual void EnterState(NewState lastState)
    {

    }


    /// <summary>
    /// Executa o update e retorna null para continuar no mesmo estado ou retorna o novo estado.
    /// </summary>
    public virtual NewState UpdateState()
    {
        return newState;
    }


    /// <summary>
    /// Executa o fixed update e retorna null para continuar no mesmo estado ou retorna o novo estado.
    /// </summary>
    public virtual NewState FixedUpdateState()
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
    /// Funções para tratamento de colisão camadas pelo adiministrados de estados.
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
