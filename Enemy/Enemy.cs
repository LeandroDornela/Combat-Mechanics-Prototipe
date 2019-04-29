/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// Calsse para dados do inimigo como clase e experiencia fornecida. 
/// </summary>
public class Enemy : MonoBehaviour
{
    public enum EnemyClass
    {
        Mariner,
        Flame,
        Heavy
    }

    [SerializeField]
    [Tooltip("Classe do inimigo.")]
    private EnemyClass enemyClass = EnemyClass.Mariner;
    [SerializeField]
    [Tooltip("Experiencia dada ao ser derrotado.")]
    private float experience = 0;


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public EnemyClass GetClass()
    {
        return enemyClass;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetXP()
    {
        return experience;
    }
}
