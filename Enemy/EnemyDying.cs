/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Estado para o inimigo morrendo.
/// </summary>
public class EnemyDying : EnemyState
{
    [SerializeField]
    [Tooltip("String com trigger da anbimação de morter do inimigo.")]
    private string animDieString;
    [SerializeField]
    [Tooltip("Total de animações. É usado para sortear a  animação.")]
    private int totalAnims;
    [SerializeField]
    [Tooltip("Colisor do inimigo.")]
    private GameObject col;
    [SerializeField]
    private GameObject particle;

    // Hash da animação no animator.
    private int animDieHash;
    // Animator.
    private Animator anim;
    // Rigidbody.
    private Rigidbody rb;
    private NavMeshAgent agent;
    

    /// <summary>
    /// 
    /// </summary>
	public override void StartState()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        animDieHash = Animator.StringToHash(animDieString);
        agent = GetComponent<NavMeshAgent>();
    }


    /// <summary>
    /// Sorteia uma animação e desabilita o colisor.
    /// </summary>
    public override void EnterState(NewState lastState)
    {
        int animID = Random.Range(0, totalAnims);
        anim.SetInteger(animDieHash, animID);
        col.SetActive(false);
        agent.enabled = false;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        GameObject clone = Instantiate(particle, transform.position, Quaternion.LookRotation(-transform.forward));
    }
}
