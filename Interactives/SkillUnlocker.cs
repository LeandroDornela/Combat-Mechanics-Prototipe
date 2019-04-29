/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Desbloqueia uma skill quando o jogador interage na area.
/// </summary>
public class SkillUnlocker : Interactive
{
    [SerializeField]
    [Tooltip("String da habilidade a ser liberada. (sem espaços. ex.: ForceCure)")]
    private string skill;
    [SerializeField]
    [Tooltip("Level qque será liberado quando aprender a habilidade. (Level 0 é o Hub, logo cemeçe do 1)")]
    private int levelToUnlock;
    
    private GameManager gameManager;


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (GameData.levels[levelToUnlock - 1].objectiveComplete)
        {
            gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public override void Interact(GameObject player)
    {
        player.GetComponent<ForceCombat>().UnlockSkill(skill);
        GameData.UnlockSkill(skill);
        GameData.UnlockLevel(levelToUnlock);
    }
}
