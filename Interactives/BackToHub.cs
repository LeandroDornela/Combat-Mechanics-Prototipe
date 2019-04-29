/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Leva o jogador de volta ao hub ou qualquer outra cena.
/// </summary>
public class BackToHub : Interactive
{
    [SerializeField]
    [Tooltip("Level que será carregado.")]
    private string destination = "Hub";

    private GameManager gameManager;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    public override void Interact(GameObject obj)
    {
        gameManager.LoadLevel(destination);
    }
}
