/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Armazena os dado do jogo, como niveis e habilidades liberados.
/// </summary>
public class GameData : MonoBehaviour
{
    public struct Level
    {
        public string name;
        public bool unlocked;
        public bool objectiveComplete;

        public Level(string _name)
        {
            name = _name;
            unlocked = false;
            objectiveComplete = false;
        }
    }

    public static Level[] levels;
    public static List<string> unlockedSkills = new List<string>();

    public string[] levelStrings;


    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        SetLevels(levelStrings);
    }
    

    /// <summary>
    /// 
    /// </summary>
    void SetLevels(string[] names)
    {
        GameData.levels = new Level[names.Length];

        for (int i = 0; i < GameData.levels.Length; i++)
        {
            GameData.levels[i] = new Level(names[i]);
        }
    }
    

    /// <summary>
    /// 
    /// </summary>
    public static void UnlockLevel(int id)
    {
        if(id < levels.Length)
        {
            levels[id].unlocked = true;
            levels[id - 1].objectiveComplete = true;
        }
        else
        {
            Debug.Log("Level inesistente.");
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public static void UnlockSkill(string key)
    {
        unlockedSkills.Add(key);
    }


    /// <summary>
    /// 
    /// </summary>
    public static bool IsSKillUnlocked(string skill)
    {
        if(unlockedSkills.Contains(skill))
        {
            return true;
        }

        return false;
    }
}
