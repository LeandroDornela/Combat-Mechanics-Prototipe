using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [Header("UI Elements")]

    // Icone da skill a ser exibida no menu.
    public GameObject icon;
    // Barra visual de xp.
    public Slider xpBar;
    // Indicador de level.
    public Text levelText;


    [SerializeField]
    private Skill skill;


    /// <summary>
    /// 
    /// </summary>
    public void ShowUIElements()
    {
        if (skill.learned)
        {
            icon.SetActive(true);
            levelText.gameObject.SetActive(true);
            levelText.text = skill.GetLevel().ToString();
            xpBar.gameObject.SetActive(true);
            xpBar.value = skill.GetExperience() / skill.GetNextLevelExperience();
        }
        else
        {
            HideUIElements();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public void HideUIElements()
    {
        icon.SetActive(false);
        levelText.gameObject.SetActive(false);
        xpBar.gameObject.SetActive(false);
    }
}
