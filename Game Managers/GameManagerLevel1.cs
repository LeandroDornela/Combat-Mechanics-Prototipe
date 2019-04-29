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
public class GameManagerLevel1 : GameManager
{
    private bool gameOver = false;
    public float timeToRestart = 5;
    private float timer = 0;
    

    void Start()
    {
        checkpointManager = GameObject.Find("Checkpoint Manager").GetComponent<CheckpointManager>();
    }


    void Update()
    {
        if(gameOver)
        {
            if(timer >= timeToRestart)
            {
                gameOver = false;
                player.ResetPlayer(checkpointManager.LastCheckpointPosition());
                timer = 0;
            }
            timer += Time.deltaTime;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="_player"></param>
    public override void GameOver(Player _player)
    {
        gameOver = true;
        player = _player;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="level"></param>
    public override void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
