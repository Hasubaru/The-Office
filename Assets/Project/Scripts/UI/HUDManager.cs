using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [Header("Hiển thị các thông số")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI levelText;

    public PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        // [HIỆN TẠI] Hiển thị thời gian & ngày
        if (TimeSystem.Instance != null)
        {
            int hour = Mathf.FloorToInt(TimeSystem.Instance.CurrentHour());
            int minute = Mathf.FloorToInt((TimeSystem.Instance.CurrentHour() - hour) * 60);
            timeText.text = $"🕒 {hour:00}:{minute:00} - Day {TimeSystem.Instance.CurrentDay()}";
        }

        


        // [TƯƠNG LAI] Hiển thị tiền - tạm dùng workEfficiency thay thế
        moneyText.text = $"💵 Salary base: {PlayerStats.Instance.GetWorkEfficiency() * 10f}";

        // [HIỆN TẠI] Cấp độ và EXP %
        float expPercent = PlayerStats.Instance.GetExperiencePercent() * 100f;
        levelText.text = $"🧠 Level {PlayerStats.Instance.GetLevel()} ({expPercent:F1}%)";
    }
}
