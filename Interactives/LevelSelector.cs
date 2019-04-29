/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Abre o menu com os leveis acessiveis.
/// </summary>
public class LevelSelector : Interactive
{
    [SerializeField]
    private InGameUI inGameUI;

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    public override void Interact(GameObject obj)
    {
        inGameUI.EnterLevelsMenu();
    }
}
