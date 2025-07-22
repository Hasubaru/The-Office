using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    // [HIỆN TẠI] Các chỉ số kỹ năng đang sử dụng
    public int skill_WorkSpeed = 1;
    public int skill_StaminaRegen = 1;
    public int skill_Communication = 1;

    // [HIỆN TẠI] Hệ thống cấp độ
    private float experience = 0f;
    private int level = 1;
    private float expToNextLevel = 100f;
    // [HIỆN TẠI] Nếu đã dùng UI nâng kỹ năng
    public int skillPoints = 10;
    public int money = 0;

    public void SpendSkillPoint()
    {
        if (skillPoints > 0)
            skillPoints--;
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // [HIỆN TẠI] Gọi từ hệ thống tính lương hoặc làm việc
    public float GetWorkEfficiency()
    {
        return skill_WorkSpeed;
    }

    // [HIỆN TẠI] Nhận kinh nghiệm và lên cấp
    public void GainExp(float amount)
    {
        experience += amount;
        Debug.Log($"🧠 Nhận {amount} EXP. Tổng: {experience}/{expToNextLevel}");

        if (experience >= expToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        // [HIỆN TẠI] Đã sử dụng UI nâng kỹ năng
        skillPoints += 5;
        level++;
        experience -= expToNextLevel;
        expToNextLevel *= 1.2f;

        Debug.Log($"🎉 Lên cấp {level}!");

        // [TƯƠNG LAI] Nếu bạn có hệ thống nâng skill:
        // skillPoints += 5;
    }

    // [TƯƠNG LAI] Gọi từ UI hoặc lưu dữ liệu
    public int GetLevel()
    {
        return level;
    }

    public float GetExperiencePercent()
    {
        return experience / expToNextLevel;
    }
}
