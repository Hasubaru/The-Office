using UnityEngine;

public class SalarySystem : MonoBehaviour
{
    public static SalarySystem Instance;

    [Header("Thiết lập mức lương & EXP")]
    public float baseSalaryPerUnit = 10f;
    public float expPerHour = 50f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// [HIỆN TẠI] Hàm tính lương & EXP dựa trên thời gian làm việc (gọi lúc 22:00)
    /// </summary>
    /// <param name="workedHours">Thời gian làm việc hôm nay</param>
    public void CalculateSalaryAndEXP(float workedHours)
    {
        float workEfficiency = PlayerStats.Instance.GetWorkEfficiency(); // dựa vào kỹ năng
        float salary = workedHours * workEfficiency * baseSalaryPerUnit;

        float bonus = 0f;
        if (OvertimeSystem.Instance != null && OvertimeSystem.Instance.WasAssignedToday())
        {
            bonus = 50f;
        }

        float totalSalary = salary + bonus;

        // Thêm tiền vào player
        PlayerStats.Instance.money += Mathf.RoundToInt(totalSalary);

        // Cộng EXP (hiện tại là 50% lương)
        float expGained = totalSalary * 0.5f;
        PlayerStats.Instance.GainExp(expGained);

        Debug.Log($"💰 Tính lương từ giờ làm: {workedHours:F2}h x Hiệu suất {workEfficiency} = {salary:F2}");
        Debug.Log($"💸 Tổng lương: {totalSalary} (Base: {salary}, Bonus OT: {bonus})");
        Debug.Log($"🧠 EXP nhận được: {expGained}");
    }

    /// <summary>
    /// [TÙY CHỌN - TƯƠNG LAI] Tính lương dựa theo số task hoàn thành từ WorkProgress
    /// </summary>
    public void CalculateFromTasks()
    {
        if (WorkProgress.Instance == null)
        {
            Debug.LogWarning("⚠️ Không tìm thấy WorkProgress để tính lương theo task.");
            return;
        }

        int taskCount = WorkProgress.Instance.GetTasksCompletedToday();
        float salary = taskCount * baseSalaryPerUnit;

        float bonus = 0f;
        if (OvertimeSystem.Instance != null && OvertimeSystem.Instance.WasAssignedToday())
        {
            bonus = 50f;
        }

        float totalSalary = salary + bonus;
        PlayerStats.Instance.money += Mathf.RoundToInt(totalSalary);
        float expGained = totalSalary * 0.5f;
        PlayerStats.Instance.GainExp(expGained);

        Debug.Log($"✅ Đã hoàn thành {taskCount} task. Tổng lương: {totalSalary}, EXP: {expGained}");

        // Reset task sau khi tính xong
        WorkProgress.Instance.ResetDailyProgress();
    }
}

    // [GIỮ LẠI] Có thể gọi tính task hoặc theo giờ tùy chế độ bạn muốn dùng
//}
