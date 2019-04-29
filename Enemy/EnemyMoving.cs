/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Estado para o inimigo se movendo.
/// </summary>
public class EnemyMoving : EnemyState
{
    [SerializeField]
    [Tooltip("Velocidade de movimento andando.")]
    private float walkSpeed = 10;
    [SerializeField]
    [Tooltip("Velocidade de movimento correndo.")]
    private float runSpeed = 20;
    [SerializeField]
    [Tooltip("Posições de patrulha.")]
    private Transform[] patrolPositions;
    [Tooltip("")]
    [SerializeField]
    private string animMovingTrigger;
    

    private NavMeshAgent agent;
    private int activePatrolPos = 0;
    private Animator anim;
    private EnemyStormAI enemyAI;
    private int animMovingHash = 0;
    

    
    /// <summary>
    /// 
    /// </summary>
    public override void StartState()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyStormAI>();
        if(patrolPositions.Length > 0) enemyAI.SetTarget(patrolPositions[activePatrolPos]);
        animMovingHash = Animator.StringToHash(animMovingTrigger);
        agent.speed = walkSpeed;
	}


    /// <summary>
    /// 
    /// </summary>
    /// <param name="lastState"></param>
    public override void EnterState(NewState lastState)
    {
        anim.SetTrigger(animMovingHash);
        agent.enabled = true;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override NewState UpdateState()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude/runSpeed);

        if (enemyAI.GetState() == EnemyStormAI.SeekState.Chasing || enemyAI.GetState() == EnemyStormAI.SeekState.Forgeting)
        {
            agent.speed = runSpeed;

            if (enemyAI.ChaseTarget())
            {
                newState.SetNewState(stateManager.EnemyShooting());
                return newState;
            }
        }
        else if(enemyAI.GetState() == EnemyStormAI.SeekState.Patrolling)
        {
            agent.speed = walkSpeed;
            if(patrolPositions.Length > 0) enemyAI.GoToTarget(patrolPositions[activePatrolPos]);
        }

        return base.UpdateState();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override NewState FixedUpdateState()
    {
        return base.FixedUpdateState();
    }


    /// <summary>
    /// 
    /// </summary>
    public override void ExitState()
    {
        base.ExitState();
    }


    /// <summary>
    /// 
    /// </summary>
    public override void TriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Patrol_position")
        {
            activePatrolPos++;
            if(activePatrolPos == patrolPositions.Length)
            {
                activePatrolPos = 0;
            }

            enemyAI.SetTarget(patrolPositions[activePatrolPos]);
        }
        else if(col.gameObject.tag == "Player_saber")
        {
            newState.SetNewState(stateManager.EnemyDying());
        }
        else if (col.gameObject.tag == "Force_lightning")
        {
            col.gameObject.GetComponentInParent<ForceLightning>().AddExperience(enemy.GetXP());
            newState.SetNewState(stateManager.EnemyDying());
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public override void TriggerExit(Collider col)
    {
        
    }


    /// <summary>
    /// 
    /// </summary>
    public override void CollisionEnter(Collision col)
    {
        
    }
}
