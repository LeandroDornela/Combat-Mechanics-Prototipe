/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Possui as funções para IA do inimigo.
/// </summary>
[RequireComponent(typeof(Enemy))]
public class EnemyStormAI : MonoBehaviour
{
    public enum SeekState
    {
        Forgeting,
        Chasing,
        Patrolling
    }

    [Tooltip("Tempo para o inimigo se esquecer do jogador.")]
    [SerializeField]
    private float timeToForgetTarget = 5;
    [Tooltip("Tag do objeto que deve ser perseguido e destruido.")]
    [SerializeField]
    private string seekAndDestroy = "Player";
    [Tooltip("Distancia minima do alvo")]
    [SerializeField]
    private float stopDistance = 5;
    [SerializeField]
    [Tooltip("Velocidade de giro do inimigo.")]
    private float rotationSpeed;

    private Transform target;
    //private Transform mobileTarget;
    private SeekState seekState = SeekState.Patrolling;
    private float forgetTimer = 0;
    private Rigidbody rb;
    private NavMeshAgent agent;


    public void SetTarget(Transform tar)
    {
        target = tar;
    }

    public Transform GetTarget()
    {
        return target;
    }

    public SeekState GetState()
    {
        return seekState;
    }

    
    /// <summary>
    /// 
    /// </summary>
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
	}
	
	
    /// <summary>
    /// 
    /// </summary>
	void Update ()
    {
		if(seekState == SeekState.Forgeting)
        {
            if(forgetTimer >= timeToForgetTarget)
            {
                seekState = SeekState.Patrolling;
                agent.stoppingDistance = 0;
            }
            else
            {
                forgetTimer += Time.deltaTime;
            }
        }
        else if(seekState == SeekState.Chasing)
        {
            agent.stoppingDistance = stopDistance;
        }
	}


    /// <summary>
    /// Se move na direção do alvo e retorna verdadeiro quando o atinge.
    /// </summary>
    /// <returns></returns>
    public bool ChaseTarget()
    {
        if(IsOnTargetPosition(0))
        {
            return true;
        }

        agent.SetDestination(target.position);

        return false;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos"></param>
    public void GoToTarget(Transform pos)
    {
        target = pos;
        agent.SetDestination(target.position);
    }


    /// <summary>
    /// 
    /// </summary>
    public bool IsOnTargetPosition(float error)
    {
        if ((transform.position - target.position).magnitude <= agent.stoppingDistance + error)
        {
            return true;
        }

        return false;
    }


    /// <summary>
    /// 
    /// </summary>
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == seekAndDestroy)
        {
            seekState = SeekState.Chasing;
            target = col.gameObject.transform;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == seekAndDestroy)
        {
            seekState = SeekState.Forgeting;
            forgetTimer = 0;
        }
    }
}
