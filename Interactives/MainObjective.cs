/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Local de interação para completar o objetivo do level.
/// </summary>
public class MainObjective : Interactive
{
    [SerializeField]
    private int levelToUnlock;
    private GameManager gameManager;
    private InGameUI inGameUI;
    private Collider col;


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        inGameUI = GameObject.Find("InGameUI").GetComponent<InGameUI>();
        col = GetComponent<Collider>();

        if (GameData.levels[levelToUnlock - 1].objectiveComplete)
        {
            gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    public override void Interact(GameObject obj)
    {
        inGameUI.DisplayNotification("Objetivo principal completo.");
        GameData.UnlockLevel(levelToUnlock);
        col.enabled = false;
    }
}
