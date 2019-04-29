/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Classe abstrata de game manager.
/// </summary>
public abstract class GameManager : MonoBehaviour
{
    public abstract void LoadLevel(string level);

    protected Player player;
    protected CheckpointManager checkpointManager;

    public abstract void GameOver(Player player);
}
