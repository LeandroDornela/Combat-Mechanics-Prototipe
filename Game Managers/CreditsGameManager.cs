/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/

 
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 
/// </summary>
public class CreditsGameManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public void BackToHub()
    {
        SceneManager.LoadScene("Hub");
    }
}
