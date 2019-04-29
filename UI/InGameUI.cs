/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Classe para controle da UI durante o jogo.
/// </summary>
public class InGameUI : MonoBehaviour
{
    [Tooltip("")]
    [SerializeField]
    private GameObject pauseMenu;
    [Tooltip("")]
    [SerializeField]
    private GameObject skillsMenu;
    [Tooltip("")]
    [SerializeField]
    private GameObject levelSelectMenu;
    [Tooltip("")]
    [SerializeField]
    private GameObject gameHUD;
    [Tooltip("")]
    [SerializeField]
    private Slider playerHP;
    [Tooltip("")]
    [SerializeField]
    private Slider playerForce;
    [Tooltip("")]
    [SerializeField]
    private Text notificationText;
    [Tooltip("")]
    [SerializeField]
    private Text objectiveText;
    [Tooltip("")]
    [SerializeField]
    private SkillIcon[] skills;
    [Tooltip("")]
    [SerializeField]
    private GameObject[] levelButtons;


    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if(notificationText.color.a > 0)
        {
            notificationText.color = new Color(notificationText.color.r, notificationText.color.g, notificationText.color.b, notificationText.color.a - Time.deltaTime/5);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="not"></param>
    public void DisplayNotification(string not)
    {
        notificationText.color = new Color(1, 1, 1, 1);
        notificationText.text = not;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="objective"></param>
    public void UpdateObjective(string objective)
    {
        objectiveText.text = objective;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void UpdateHP(float value)
    {
        playerHP.value = value;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void UpdateForce(float value)
    {
        playerForce.value = value;
    }


    /// <summary>
    /// 
    /// </summary>
    public void EnterPauseMenu()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    public void EnterSkillsMenu()
    {
        if(skillsMenu.activeSelf)
        {
            ExitSkillsMenu();
        }
        else
        {
            skillsMenu.SetActive(true);
            ShowSkills();
            Time.timeScale = 0;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    void ShowSkills()
    {
        for(int i = 0; i < skills.Length; i++)
        {
            skills[i].ShowUIElements();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public void ExitSkillsMenu()
    {
        skillsMenu.SetActive(false);
        Time.timeScale = 1;
    }


    /// <summary>
    /// 
    /// </summary>
    public void EnterLevelsMenu()
    {
        levelSelectMenu.SetActive(true);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Ir em levels[i+1] para pular o hub.
            if (GameData.levels[i+1].unlocked)
            {
                levelButtons[i].SetActive(true);
            }
            else
            {
                levelButtons[i].SetActive(false);
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="levelName"></param>
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
