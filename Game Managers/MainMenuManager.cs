/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Game manager do menu principal.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string hubLevel = "Hub";


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    /// <summary>
    /// 
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene(hubLevel);
    }
}
