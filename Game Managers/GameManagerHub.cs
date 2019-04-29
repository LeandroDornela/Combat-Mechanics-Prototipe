/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Game Manager do Hub
/// </summary>
public class GameManagerHub : GameManager
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    /// <summary>
    /// Carrega o level informado.
    /// </summary>
    public override void LoadLevel(string lvl)
    {
        SceneManager.LoadScene(lvl);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    public override void GameOver(Player player)
    {
        
    }
}
