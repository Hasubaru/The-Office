using UnityEngine;

public class OvertimeSystem : MonoBehaviour
{
    public static OvertimeSystem Instance;

    private bool overtimeAssignedToday = false;
    private bool isOvertimeNow = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        float hour = TimeSystem.Instance.CurrentHour();

        // Giao overtime một lần duy nhất sau 5PM
        if (!overtimeAssignedToday && hour >= 17f && hour < 18f && TimeSystem.Instance.IsWorkday())
        {
            AssignOvertime();
        }

        // Cập nhật trạng thái đang trong overtime
        isOvertimeNow = (hour >= 17f && hour <= 22f && overtimeAssignedToday && TimeSystem.Instance.IsWorkday());
    }

    private void AssignOvertime()
    {
        float chance = 0.3f; // 30% xác suất
        overtimeAssignedToday = Random.value < chance;

        if (overtimeAssignedToday)
        {
            Debug.Log("🔔 Sếp: Hôm nay ở lại làm thêm đến 10PM!");
            // Có thể gọi UIManager.Instance.ShowOvertimePopup() nếu bạn có UI
        }
        else
        {
            Debug.Log("✅ Hôm nay được về đúng giờ.");
        }
    }

    public bool IsOvertimeNow()
    {
        return isOvertimeNow;
    }

    public bool WasAssignedToday()
    {
        return overtimeAssignedToday;
    }

    public bool CanWorkNow()
    {
        float hour = TimeSystem.Instance.CurrentHour();
        return (hour >= 8f && hour < 17f) || isOvertimeNow;
    }

    public void ResetOvertime()
    {
        overtimeAssignedToday = false;
        isOvertimeNow = false;
    }
}
