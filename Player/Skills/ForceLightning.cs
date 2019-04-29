/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 10/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// 
/// </summary>
public class ForceLightning : Skill
{
    [Tooltip("Particulas.")]
    [SerializeField]
    private ParticleSystem particles;
    [Tooltip("Colisor da habilidade.")]
    [SerializeField]
    private GameObject col;
    [Tooltip("Tempo de ativãção do colisor.")]
    [SerializeField]
    private float activationTime = 0;
    [Tooltip("Tempo de desativação do colisor.")]
    [SerializeField]
    private float deactivationTime = 0;

    // Contador de tempo.
    private float timer = 0;


    /// <summary>
    /// 
    /// </summary>
    public override void Activate()
    {
        particles.Play();
        player.ConsumeForce(forceConsume);
    }


    /// <summary>
    /// 
    /// </summary>
    public override void LevelUpActions()
    {
        
    }


    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if(timer >= activationTime)
        {
            col.SetActive(true);
        }

        if(timer >= deactivationTime)
        {
            col.SetActive(false);
            gameObject.SetActive(false);
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
