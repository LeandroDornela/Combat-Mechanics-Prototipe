/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 10/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Estado de combate com o sabre.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class SaberCombat : Combat
{
    // Tipos de golpe.
    enum Strike
    {
        shortStrike,
        longStrike,
        none
    }
    

    [SerializeField]
    [Tooltip("Tempo antes do final do clip quando a animação desacelera.")]
    private float timeToSlowMotion;
    [SerializeField]
    [Tooltip("Velocidade da animação atual em slow motion.")]
    private float slowMotionValue;
    [SerializeField]
    [Tooltip("Tempo apos entrar em slow motion que será iniciada a proxima animação caso exista.")]
    private float timeForNextAnimFromSlow;
    [SerializeField]
    [Tooltip("Trigger para as animações de golpe longo.")]
    private string longStrkAnimTrigger;
    [SerializeField]
    [Tooltip("Trigger para as animações de golpe curto.")]
    private string shortStrkAnimTrigger;
    [Tooltip("Velocidade de rotação.")]
    private float rotaionSpeed = 5;
    [Tooltip("desaceleração do jogador.")]
    [SerializeField]
    private float slowdown = 10;
    [SerializeField]
    [Tooltip("Projeção de movimento, dano, null, null")]
    private Vector4[] longStrikesData;
    [SerializeField]
    [Tooltip("Projeção de movimento, dano, null, null")]
    private Vector4[] shortStrikesData;
    [Tooltip("Colisor do sabre. É desligado quando não está em combate.")]
    [SerializeField]
    private GameObject saberCollider;
    [SerializeField]
    private GameObject saberDefence;

    // Animator do player.
    private Animator anim;
    // Rigidbody do player.
    private Rigidbody rb;
    // Hash para trigger das animações de golpe longo.
    private int longStrkAnimTriggerHash = 0;
    // Hash para trigger das animações de golpe curto.
    private int shortStrkAnimTriggerHash = 0;
    // Contador de tempo do golpe atual.
    private float timer = 0;
    // Duração da animação do golpe atual.
    private float animationClipLength = 0;
    // Vetor da direção de movimento.
    private Vector3 dir = Vector3.zero;
    // Verdadeiro se o jogador pressionaol o botão para o proximo golpe.
    private NewState nextStrike = new NewState(null, null, false);
    // Golpe atual.
    private int presentStrikeID = 0;
    // Ultimo tipo de golpe dado.
    private Strike lastStrike = Strike.none;
    // Dados do ataque atual.
    private Vector4 presentStateData;


    /// <summary>
    /// 
    /// </summary>
    protected override void StartState()
    {
        this.anim = this.GetComponent<Animator>();
        this.rb = this.GetComponent<Rigidbody>();
        this.longStrkAnimTriggerHash = Animator.StringToHash(longStrkAnimTrigger);
        this.shortStrkAnimTriggerHash = Animator.StringToHash(shortStrkAnimTrigger);
    }


    /// <summary>
    /// O estado de combate pode ir devolta para o estado de combate, assim como um automato que volta pra ele mesmo.
    /// </summary>
    public override void EnterState(NewState lastState, PlayerInput playerInput)
    {
        // Zera o contador de golpes se não estava dando um golpe.
        if (lastState.state.GetType() != typeof(SaberCombat))
        {
            lastStrike = Strike.none;
            presentStrikeID = 0;
        }

        // Prepara pra o golpe curto.
        if (lastState.input == playerInput.trigger_R)
        {
            // Incrementa o contador dde golpes caso o ultimo golpe seja curto
            if(lastStrike == Strike.shortStrike)
            {
                presentStrikeID++;
                if(presentStrikeID >= shortStrikesData.Length) presentStrikeID = 0;
            }
            else
            {
                // Zera o contador de golpes e seta para golpe atual curto.
                presentStrikeID = 0;
                lastStrike = Strike.shortStrike;
            }

            presentStateData = shortStrikesData[presentStrikeID];

            this.anim.SetTrigger(this.shortStrkAnimTriggerHash);
        }
        // Prepara para o golpe longo.
        else if(lastState.input == playerInput.trigger_L)
        {
            // Incrementa o contador dde golpes caso o ultimo golpe seja longo
            if (lastStrike == Strike.longStrike)
            {
                presentStrikeID++;
                if (presentStrikeID >= longStrikesData.Length) presentStrikeID = 0;
            }
            else
            {
                // Zera o contador de golpes e seta para golpe atual longo.
                presentStrikeID = 0;
                lastStrike = Strike.longStrike;
            }

            presentStateData = longStrikesData[presentStrikeID];

            this.anim.SetTrigger(this.longStrkAnimTriggerHash);
        }

        // Projeta o jogador de acordo com o golpe.
        rb.velocity = transform.forward*presentStateData[0];
        // Ativa a colisão do sabre.
        saberCollider.SetActive(true);
        saberDefence.SetActive(true);
        // Reseta a velocidade de animação.
        anim.speed = 1;
        // Reseta o contador de tempo.
        timer = 0;
    }


    /// <summary>
    /// 
    /// </summary>
    public override NewState UpdateState(PlayerInput playerInput)
    {
        anim.speed = 1;

        // TODO: Tentar colocar isso no inicio, para isso tem q esperar a animação terminar a transição.
        animationClipLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length / anim.GetCurrentAnimatorStateInfo(0).speed;

        // Verifica se a animação já terminou, se verdadeiro volta para o estado inicial.
        if (timer >= animationClipLength)
        {
            newState.SetNewState(stateManager.Moving(), null, false);
            return newState;
        }


        EnterInSlowMotion();


        #region Retorno por interrupção de animação
        // Verificar interrupção por algum evento.
        if (newState.state != null && newState.instant)
        {
            return newState;
        }

        // Verificar interrupção por input.
        InputHandle(playerInput);
        if (newState.state != null && newState.instant)
        {
            return newState;
        }
        #endregion

        #region Retorno por tempo minimo de animação
        // Verificar evento não prioritario se o input verificado não é de interrupção.
        if (newState.state != null && nextStrike.state == null)
        {
            // Next state fica protegido a partir do momento em que recebe um valor, enquando newstate muda.
            nextStrike = newState;
        }

        // Se não é interrupção e ja foi atribuido ao nextState caso necessario, limpa o novo estado que pode ter um
        // golpe que não deve ser retornado.
        newState.Reset();

        // Se ha um nextStrike retorna no tempo certo.
        if (nextStrike.state != null && timer >= (animationClipLength - timeToSlowMotion + timeForNextAnimFromSlow))
        {
            anim.speed = 1;
            return nextStrike;
        }
        #endregion

        // Senão, incrementa o tempo e retorna o newState resetado.
        timer += Time.deltaTime;
        return newState;
    }


    /// <summary>
    /// 
    /// </summary>
    public override NewState FixedUpdateState(PlayerInput playerInput)
    {
        RotateRb(playerInput);

        float newMag = rb.velocity.magnitude - slowdown * Time.deltaTime;

        if (newMag <= 0)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = rb.velocity.normalized * (rb.velocity.magnitude - slowdown * Time.deltaTime);
        }

        return base.FixedUpdateState(playerInput);
    }


    /// <summary>
    /// 
    /// </summary>
    public override void ExitState()
    {
        anim.speed = 1;
        nextStrike = new NewState(null, null, false);
        saberCollider.SetActive(false);
        saberDefence.SetActive(false);
    }


    /// <summary>
    /// 
    /// </summary>
    public override NewState InputHandle(PlayerInput playerInput)
    {
        return base.InputHandle(playerInput);
    }

    
    /// <summary>
    /// Reduz a velocidade de animação no tempo dado.
    /// </summary>
    void EnterInSlowMotion()
    {
        if (timer >= animationClipLength - timeToSlowMotion)
        {
            anim.speed = 1f / slowMotionValue;
        }
    }


    /// <summary>
    /// Rotaciona o jogador na direção do direcional de movimento.
    /// </summary>
    void RotateRb(PlayerInput playerInput)
    {
        if (Input.GetAxis(playerInput.horizontal_L) != 0 || Input.GetAxis(playerInput.vertical_L) != 0 && rb.velocity.magnitude > 0)
        {
            dir = new Vector3(Input.GetAxis(playerInput.horizontal_L), 0, Input.GetAxis(playerInput.vertical_L));
            dir = Vector3.RotateTowards(transform.forward, dir, rotaionSpeed * Time.deltaTime, 0f);
            this.rb.velocity = dir * rb.velocity.magnitude;
            this.rb.rotation = Quaternion.LookRotation(dir);

            if (rb.velocity.magnitude != 0)
            {
                dir = Vector3.RotateTowards(this.transform.forward, new Vector3(this.rb.velocity.x, 0, this.rb.velocity.z).normalized, rotaionSpeed * Time.deltaTime, 0f);
            }

            /*
            dir = new Vector3(Input.GetAxis(playerInput.horizontal_L), 0, Input.GetAxis(playerInput.vertical_L));
            dir = Vector3.RotateTowards(new Vector3(this.rb.velocity.x, 0, this.rb.velocity.z).normalized, dir, rotaionSpeed * Time.deltaTime, 0f);
            rb.velocity = rb.velocity.magnitude * dir;
            if (dir != Vector3.zero) this.rb.rotation = Quaternion.LookRotation(dir);
            */
        }
        
        Debug.DrawRay(transform.position, dir, Color.red, 1);
    }
}
