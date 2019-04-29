/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 10/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Estado para o jogador recebendo dano.
/// </summary>
public class Damage : PlayerState
{
    [Tooltip("Trigger da animação.")]
    [SerializeField]
    private string animDamageTrigger;

    private int animDamageHash = 0;
    private float animationClipLength = 0;
    private float timer = 0;
    private Animator anim;
    private Rigidbody rb;
    

    /// <summary>
    /// 
    /// </summary>
    protected override void StartState()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        animDamageHash = Animator.StringToHash(animDamageTrigger);
    }


    /// <summary>
    /// 
    /// </summary>
    public override void EnterState(NewState lastState, PlayerInput playerInput)
    {
        rb.velocity = Vector3.zero;
        anim.SetTrigger(animDamageHash);
    }


    /// <summary>
    /// 
    /// </summary>
    public override NewState UpdateState(PlayerInput playerInput)
    {
        animationClipLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length / anim.GetCurrentAnimatorStateInfo(0).speed;

        if (timer >= animationClipLength)
        {
            newState.SetNewState(stateManager.Moving(), null, false);
            timer = 0;
            return newState;
        }

        timer += Time.deltaTime;

        return newState;
    }


    /// <summary>
    /// Aplica dano ao jogador a chamando a função de dano e indo para o estrado correto.
    /// </summary>
    public override bool TakeDamage(float damage)
    {
        if (player.HpDamage(damage))
        {
            newState.SetNewState(stateManager.Dying(), null, true);
        }
        else
        {
            newState.SetNewState(stateManager.Damage(), null, true);
        }

        return false;
    }
}
