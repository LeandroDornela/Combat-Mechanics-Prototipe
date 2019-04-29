/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Camera simples para seguir o jogador.
/// </summary>
public class ActionRPGCamera : MonoBehaviour
{
    public enum NumberOfPlayers
    {
        SinglePlayer,
        Multiplayer
    }

    [Tooltip("Quantidade de players, muda a referencia de moviemnto da camera")]
    public NumberOfPlayers numberOfPlayers = NumberOfPlayers.SinglePlayer;
    [Tooltip("Objeto que a camera irá seguir.")]
    public Transform holder;

    // Posição da camera em relação ao jogador no inicio.
    private Vector3 defalPosition = Vector3.zero;

	// Use this for initialization
	void Start ()
    {
        defalPosition = holder.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(numberOfPlayers == NumberOfPlayers.SinglePlayer)
        {
            transform.position = holder.transform.position - defalPosition;
        }
	}
}
