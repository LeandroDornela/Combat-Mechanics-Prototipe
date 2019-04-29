/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 10/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Classe para os status do jogador, vida, energia, level total
/// </summary>
public class Player : MonoBehaviour
{
    [Tooltip("")]
    [SerializeField]
    private float maxLife = 100;
    [Tooltip("")]
    [SerializeField]
    private float maxForce = 30;
    [Tooltip("")]
    [SerializeField]
    private float defaltLife = 100;
    [Tooltip("")]
    [SerializeField]
    private float defaltforce = 30;
    [Tooltip("")]
    [SerializeField]
    private float forceRegeneration = 0;
    [Tooltip("")]
    public InGameUI inGameUI = null;

    // Vida atual.
    private float life = 0;
    // Força atual.
    private float force = 0;
    // Level atual.
    private int level = 0;
    // Gerenciador de estados.
    private PlayerStateManager stateManager;
    // Gerenciador de strings input.
    private PlayerInput playerInput;


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetForce()
    {
        return force;
    }

    
    /// <summary>
    /// 
    /// </summary>
	void Start ()
    {
        if (inGameUI == null) Debug.Log("No UI.");
        this.life = defaltLife;
        this.force = defaltforce;

        stateManager = GetComponent<PlayerStateManager>();
        playerInput = GetComponent<PlayerInput>();
	}
	

	/// <summary>
    /// 
    /// </summary>
	void Update ()
    {
        RegenerateForce();

        if(Input.GetButtonDown(playerInput.select))
        {
            inGameUI.EnterSkillsMenu();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        
    }


    /// <summary>
    /// Armazena informações iniciais do jogador.
    /// </summary>
    public void SetPlayer()
    {

    }


    /// <summary>
    /// Restaura a vida e os estados iniciais do jogador.
    /// Mantem toda experiencia adiquirida.
    /// </summary>
    public void ResetPlayer(Vector3 position)
    {
        transform.position = new Vector3(position.x, transform.position.y, position.z);
        transform.rotation = Quaternion.identity;
        stateManager.ResetState();
        life = maxLife;
        force = maxForce;
        inGameUI.UpdateHP(life / maxLife);
        inGameUI.UpdateForce(force / maxForce);
    }


    /// <summary>
    /// Regenera a força com o valor dado;
    /// </summary>
    public void RegenerateForce()
    {
        if(force < maxForce)
        {
            force += forceRegeneration*Time.deltaTime;
            inGameUI.UpdateForce(force/maxForce);
        }
    }


    /// <summary>
    /// Adiciona um level ao jogador. Um level é adiquirido a cada level de habilidade.
    /// </summary>
    public void LevelUp()
    {
        this.level++;
    }


    /// <summary>
    /// Remove uma quantidade dada de vida do jogador e retorna verdadeiro caso a vida
    /// chegue a 0 ou falso caso contrario.
    /// </summary>
    public bool HpDamage(float damage)
    {
        life -= damage;

        if (life <= 0)
        {
            life = 0;
            inGameUI.UpdateHP(0);
            return true;
        }
        inGameUI.UpdateHP(life / maxLife);

        return false;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="hp"></param>
    public void Cure(float hp)
    {
        life += hp;

        if(life > maxLife)
        {
            life = maxLife;
        }

        inGameUI.UpdateHP(life / maxLife);
    }


    /// <summary>
    /// Aumenta a quantidade de hp maximo.
    /// </summary>
    /// <param name="value"></param>
    public void IncreaseMaxHp(float value)
    {
        maxLife += value;
        life = maxLife;
    }


    /// <summary>
    /// Consome a quantidade de energia informada.
    /// </summary>
    /// <param name="value"></param>
    public void ConsumeForce(float value)
    {
        force -= value;
        inGameUI.UpdateForce(force/maxForce);
    }
}
