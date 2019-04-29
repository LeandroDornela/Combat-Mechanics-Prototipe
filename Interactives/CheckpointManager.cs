/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Classe para gerência dos checkpoints.
/// </summary>
public class CheckpointManager : MonoBehaviour
{
    [Tooltip("")]
    [SerializeField]
    private GameObject[] checkpoints;
    [Tooltip("")]
    [SerializeField]
    private InGameUI inGameUI;
    
    private int lastCheckpoint = 0;


    /// <summary>
    /// Retorna a posição do ultimo checkpoint.
    /// </summary>
    /// <returns></returns>
    public Vector3 LastCheckpointPosition()
    {
        return checkpoints[lastCheckpoint].transform.position;
    }


    /// <summary>
    /// Atualisza o ultimo checkpoint.
    /// </summary>
    public void Checkpoint(int id)
    {
        lastCheckpoint = id;
        inGameUI.DisplayNotification("Checkpoint.");
    }


    /// <summary>
    /// Recarega a cena.
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
