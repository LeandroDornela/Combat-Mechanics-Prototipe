/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Classe para detecção de checkpoint.
/// </summary>
public class Checkpoint : MonoBehaviour
{
    [Tooltip("")]
    [SerializeField]
    private string playerTag = "Player";
    [Tooltip("")]
    [SerializeField]
    private int checkpointID = 0;
    [Tooltip("")]
    private CheckpointManager checkpointManager;


    /// <summary>
    /// 
    /// </summary>
    void Start ()
    {
        checkpointManager = GetComponentInParent<CheckpointManager>();
        if (checkpointManager == null) Debug.LogError("Não um checkpoint manager no parent.");
	}


    /// <summary>
    /// 
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == playerTag)
        {
            checkpointManager.Checkpoint(checkpointID);
            gameObject.SetActive(false);
        }
    }
}
