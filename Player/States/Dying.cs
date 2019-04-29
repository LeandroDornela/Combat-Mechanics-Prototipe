/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/

using UnityEngine;


/// <summary>
/// Estado do jogador morrendo. Nenhuma interação é permitida neste estado.
/// </summary>
public class Dying : PlayerState
{
    [Tooltip("Trigger da animação de morte do jogador.")]
    [SerializeField]
    private string animDieTrigger;
    
    // Hash da animação.
    private int animDieHash;
    // Animator.
    private Animator anim;
    // Rigidbody.
    private Rigidbody rb;


    /// <summary>
    /// 
    /// </summary>
    protected override void StartState()
    {
        animDieHash = Animator.StringToHash(animDieTrigger);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// 
    /// </summary>
    public override void EnterState(NewState lastState, PlayerInput playerInput)
    {
        anim.SetTrigger(animDieHash);
        rb.velocity = Vector3.zero;
        GameObject.Find("GameManager").GetComponent<GameManager>().GameOver(this.gameObject.GetComponent<Player>());
    }
}
