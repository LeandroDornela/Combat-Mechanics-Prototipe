/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 10/2018
 * Modificação:
 * ********************************************************************/

using UnityEngine;


/// <summary>
/// Classe de combate para movimentação do player.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Moving : Combat
{
    [Tooltip("Aceleração do player.")]
    public float aceleration = 10;
    [Tooltip("desaceleração do jogador.")]
    public float slowdown = 15;
    [Tooltip("Velocidade maxima correndo.")]
    public float maxRunSpeed = 8;
    [Tooltip("Velocidade maxima andando.")]
    public float maxWalkSpeed = 2;
    [Tooltip("Velocidade de rotação.")]
    public float rotaionSpeed = 5;
    [Tooltip("Trigger da animação de movimento")]
    public string animationTrigger;

    // Rigidbody do player.
    private Rigidbody rb;
    // Animator do player.
    private Animator anim;
    // Hash do trigger da animação de movimento.
    private int animationTriggerHash = 0;
    // Vetor da direção de movimento.
    private Vector3 dir = Vector3.zero;
    // Objeto interativo cujo o jogador esta proximo.
    private Interactive interactiveObj;
    // Velociade atual do player.
    private float speed = 0;
    // Velocidade maxima atual.
    private float maxPresentSpeed = 0;
    // Verdadeiro quando o jogador está parando.
    private bool stopping = false;


    /// <summary>
    /// 
    /// </summary>
    protected override void StartState()
    {
        this.rb = this.GetComponent<Rigidbody>();
        this.anim = this.GetComponent<Animator>();
        this.animationTriggerHash = Animator.StringToHash(animationTrigger);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="lastState"></param>
    /// <param name="playerInput"></param>
    public override void EnterState(NewState lastState, PlayerInput playerInput)
    {
        this.anim.SetTrigger(this.animationTriggerHash);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerInput"></param>
    /// <returns></returns>
    public override NewState FixedUpdateState(PlayerInput playerInput)
    {
        this.Move(playerInput);

        return base.UpdateState(playerInput);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerInput"></param>
    /// <returns></returns>
    public override NewState UpdateState(PlayerInput playerInput)
    {
        return InputHandle(playerInput);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerInput"></param>
    /// <returns></returns>
    public override NewState InputHandle(PlayerInput playerInput)
    {
        if (Input.GetButtonDown(playerInput.up))
        {
            if(interactiveObj != null)
            {
                interactiveObj.Interact(gameObject);
            }
            else
            {
                Debug.Log("No interactive object.");
            }
        }

        return base.InputHandle(playerInput);
    }


    /// <summary>
    /// 
    /// </summary>
    public override void ExitState()
    {
        newState.Reset();
    }


    /// <summary>
    /// Modifica a velocidade e rotaciona o jogador na direção do movimento.
    /// </summary>
    /// <param name="playerInput"></param>
    void Move(PlayerInput playerInput)
    {
        if (Input.GetAxis(playerInput.horizontal_L) != 0 || Input.GetAxis(playerInput.vertical_L) != 0)
        {
            // Verifica se o player deve estar correndo.
            if (Input.GetAxis(playerInput.bumper_L) > 0)
            {
                speed = Mathf.Lerp(rb.velocity.magnitude, maxRunSpeed, aceleration*Time.deltaTime);
            }
            else
            {
                speed = Mathf.Lerp(rb.velocity.magnitude, maxWalkSpeed, slowdown*Time.deltaTime);
            }

            // Velocidade do rb.
            dir = new Vector3(Input.GetAxis(playerInput.horizontal_L), 0, Input.GetAxis(playerInput.vertical_L));
            rb.velocity = dir.normalized * speed;

            // Direção do transform.
            dir = Vector3.RotateTowards(this.transform.forward, new Vector3(this.rb.velocity.x, 0, this.rb.velocity.z).normalized, rotaionSpeed * Time.deltaTime, 0f);
            if (dir != Vector3.zero) this.rb.rotation = Quaternion.LookRotation(dir);
        }
        else
        {
            speed = Mathf.Lerp(rb.velocity.magnitude, 0, slowdown * Time.deltaTime);
            rb.velocity = rb.velocity.normalized * speed;
        }


        Debug.DrawRay(transform.position, dir, Color.red, 1);
        
        this.anim.SetFloat("Speed", this.rb.velocity.magnitude / maxRunSpeed);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="col"></param>
    public override void TriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Interactive")
        {
            interactiveObj = col.gameObject.GetComponent<Interactive>();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="col"></param>
    public override void TriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Interactive")
        {
            interactiveObj = null;
        }
    }
}
