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
public class ForceCure : Skill
{
    [Tooltip("Quantidade de HP que pode ser curada.")]
    [SerializeField]
    private float curePower = 10;
    [Tooltip("Aumento do poder de cura no level up.")]
    [SerializeField]
    private float curePowerIncrement = 10;
    [Tooltip("Particulas.")]
    [SerializeField]
    private ParticleSystem particles;
    [Tooltip("Incremento de Hp maximo do jogador quando a ahabilidade aumenta.")]
    [SerializeField]
    private float maxHpIncrement;
    [SerializeField]
    [Tooltip("Experiencia adiquirida ao usar a habilidade.")]
    private float xpOfUse;


    /// <summary>
    /// 
    /// </summary>
    public override void Activate()
    {
        player.Cure(curePower);
        particles.Play();
        gameObject.SetActive(false);
        AddExperience(xpOfUse);
        player.ConsumeForce(forceConsume);
    }


    /// <summary>
    /// 
    /// </summary>
    public override void LevelUpActions()
    {
        curePower += curePowerIncrement;
        player.IncreaseMaxHp(maxHpIncrement);
    }
}
