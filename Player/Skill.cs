/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 10/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Class abstrata para as habilidades do jogador. Possui implemetada a aquisição de
/// experiencia e ganho de leveis.
/// </summary>
public abstract class Skill : MonoBehaviour
{
    // Tipos de crescimento de um level para o outro.
    public enum NextLevelMult
    {
        constant,
        linear,
        quadratic
    }

    [Header("Skill data")]

    // Crescimento de xp de level para o proximo.
    public NextLevelMult nextLevelMultiplicatorForm = NextLevelMult.constant;
    // Valor do crescimento.
    public float nextLevelMultValue = 100;
    // Experiencia para avançar no primeiro level.
    public float fistLevelXp = 100;

    // Contador de experiencia da habilidade.
    protected float experience = 0;
    // Contador de leveis da habilidade.
    protected int level = 0;
    // Experiencia nescessaria para o proximo level.
    protected float nextLevelXp = 0;
    // Referencia ao gameobject do player; 
    protected GameObject playerGameObjetc;
    // Referencia ao script Player do player;
    protected Player player;
    // Tempo até o jogador poder usar a proxima skill.
    public float cooldown = 0;
    // Quantidade de energia consumida pela habilidade.
    public float forceConsume = 0;
    // Verdadeiro quando o jogador ja aprendeu a habilidade.
    public bool learned = false;


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetLevel()
    {
        return level;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetExperience()
    {
        return experience;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetNextLevelExperience()
    {
        return nextLevelXp;
    }


    /// <summary>
    /// Inicialisa a habilidade. Executado apenas no inicio do jogo.
    /// </summary>
    /// <param name="playerGO"></param>
    public virtual void Initialize(GameObject playerGO)
    {
        nextLevelXp = fistLevelXp;
        playerGameObjetc = playerGO;
        player = playerGameObjetc.GetComponent<Player>();
    }


    /// <summary>
    /// Adiciona experiencia a habilidade e avança um level caso
    /// tenha obtido o suficiente no total.
    /// </summary>
    /// <param name="xp"></param>
    public virtual void AddExperience(float xp)
    {
        this.experience += xp;

        if(this.experience >= nextLevelXp)
        {
            this.level++;
            player.LevelUp();

            if(nextLevelMultiplicatorForm == NextLevelMult.constant)
            {
                nextLevelXp += nextLevelMultValue;
            }
            else if(nextLevelMultiplicatorForm == NextLevelMult.linear)
            {
                nextLevelXp += nextLevelXp * nextLevelMultValue;
            }
            else
            {
                nextLevelXp += Mathf.Pow(nextLevelXp, nextLevelMultValue);
            }

            LevelUpActions();
        }
    }


    /// <summary>
    /// Ações ao usar a habilidade.
    /// </summary>
    public abstract void Activate();


    /// <summary>
    /// Ações ao avançar um level na habilidade.
    /// </summary>
    public abstract void LevelUpActions();
}
