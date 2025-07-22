using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeUI : MonoBehaviour
{
    [Header("Text hiển thị kỹ năng")]
    public TextMeshProUGUI workSpeedText;
    public TextMeshProUGUI staminaRegenText;
    public TextMeshProUGUI communicationText;
    public TextMeshProUGUI skillPointsText;

    [Header("Các nút nâng kỹ năng")]
    public Button upgradeWorkSpeedButton;
    public Button upgradeStaminaRegenButton;
    public Button upgradeCommunicationButton;

    private void Start()
    {
        UpdateUI();

        upgradeWorkSpeedButton.onClick.AddListener(UpgradeWorkSpeed);
        upgradeStaminaRegenButton.onClick.AddListener(UpgradeStaminaRegen);
        upgradeCommunicationButton.onClick.AddListener(UpgradeCommunication);
    }

    private void UpdateUI()
    {
        workSpeedText.text =   PlayerStats.Instance.skill_WorkSpeed.ToString();
        staminaRegenText.text =  PlayerStats.Instance.skill_StaminaRegen.ToString();
        communicationText.text =  PlayerStats.Instance.skill_Communication.ToString();
        skillPointsText.text = "Skill Points: " + PlayerStats.Instance.skillPoints.ToString();
    }

    private void UpgradeWorkSpeed()
    {
        if (PlayerStats.Instance.skillPoints > 0)
        {
            PlayerStats.Instance.skill_WorkSpeed += 1;
            PlayerStats.Instance.SpendSkillPoint();
            UpdateUI();
        }
    }

    private void UpgradeStaminaRegen()
    {
        if (PlayerStats.Instance.skillPoints > 0)
        {
            PlayerStats.Instance.skill_StaminaRegen += 1;
            PlayerStats.Instance.SpendSkillPoint();
            UpdateUI();
        }
    }

    private void UpgradeCommunication()
    {
        if (PlayerStats.Instance.skillPoints > 0)
        {
            PlayerStats.Instance.skill_Communication += 1;
            PlayerStats.Instance.SpendSkillPoint();
            UpdateUI();
        }
    }
}
