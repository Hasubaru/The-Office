using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    private bool hasGivenSalaryToday = false;

    public static TimeSystem Instance;

    [Header("Cài đặt thời gian")]
    [Tooltip("Thời gian hiện tại theo giờ (ví dụ: 8.5 là 8 giờ 30 phút)")]
    public float currentHour = 8f; // Bắt đầu lúc 8AM
    public int currentDay = 1;
    [Tooltip("Tốc độ thời gian. 1.0f = 1 giây thực = 1 giờ trong game. 0.1f = 1 giây thực = 0.1 giờ trong game (6 phút).")]
    public float timeSpeed = 0.008f; // Tốc độ thời gian

    [Header("Hiển thị UI (nếu có)")]
    public TextMeshProUGUI clockText;
    public TextMeshProUGUI dayText;

    // Delegate và Event để các hệ thống khác lắng nghe sự kiện chuyển ngày
    public delegate void OnDayAdvanced(int newDay);
    public static event OnDayAdvanced onDayAdvanced;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Đảm bảo TimeSystem không bị hủy khi tải cảnh mới
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Tăng thời gian theo từng khung hình.
        // Time.deltaTime là thời gian hoàn thành khung hình trước đó (tính bằng giây).
        // timeSpeed quy định 1 giây thực bằng bao nhiêu giờ trong game.
        currentHour += Time.deltaTime * timeSpeed;

        // Xử lý khi thời gian vượt quá 24 giờ (chuyển sang ngày mới)
        if (currentHour >= 24f)
        {
            hasGivenSalaryToday = false;
            WorkSessionTracker.Instance?.ResetDaily();
            currentHour -= 24f; // Trừ đi 24 để giữ giá trị trong khoảng 0-23.999...
            currentDay++; // Tăng ngày

            // Thông báo cho các hệ thống khác biết ngày đã được chuyển
            onDayAdvanced?.Invoke(currentDay);

            // Nếu có GameManager và bạn muốn gọi hàm của nó khi chuyển ngày
            /*if (GameManager.Instance != null)
             {
                 GameManager.Instance.AdvanceDay(); 
             }*/
        }

        UpdateClockUI();
        if (currentHour >= 22f && !hasGivenSalaryToday)
        {
            if (SalarySystem.Instance != null && WorkSessionTracker.Instance != null)
            {
                float workedTime = WorkSessionTracker.Instance.GetWorkTime();
                SalarySystem.Instance.CalculateSalaryAndEXP(workedTime);
            }

            hasGivenSalaryToday = true;
        }
    }

    /// <summary>
    /// Hàm này được dùng để tua nhanh thời gian (ví dụ: khi ngủ, hoặc sự kiện đặc biệt).
    /// KHÔNG nên gọi trong Update().
    /// </summary>
    /// <param name="hoursToAdvance">Số giờ muốn tua nhanh.</param>
    public void AdvanceTime(float hoursToAdvance)
    {
        currentHour += hoursToAdvance;
        while (currentHour >= 24f)
        {
            currentHour -= 24f;
            currentDay++;
            onDayAdvanced?.Invoke(currentDay);
            // if (GameManager.Instance != null) GameManager.Instance.AdvanceDay();
        }
        UpdateClockUI(); // Cập nhật UI ngay sau khi tua nhanh
    }

    private void UpdateClockUI()
    {
        if (clockText != null)
        {
            int hour = Mathf.FloorToInt(currentHour);
            int minute = Mathf.FloorToInt((currentHour - hour) * 60);
            clockText.text = $"{hour:00}:{minute:00}";
        }

        if (dayText != null)
        {
            dayText.text = $"Day {currentDay} ({GetDayOfWeek()})";
        }
    }

    public void ResetTime()
    {
        currentHour = 8f; // Reset về 8AM, bạn đặt là 6 nhưng khởi tạo là 8. Nên thống nhất.
        currentDay = 1;
        UpdateClockUI();
    }


    // Các hàm cho hệ thống khác dùng
    public float CurrentHour() => currentHour;
    public int CurrentDay() => currentDay;

    // Lấy số thứ tự của ngày trong tuần (1 = Monday, 7 = Sunday)
    public int GetWeekdayNumber()
    {
        // currentDay - 1 để bắt đầu từ 0 cho phép tính modulo
        // + 1 để đưa về 1-7
        return ((currentDay - 1) % 7) + 1;
    }

    // Lấy tên ngày trong tuần
    public string GetDayOfWeek()
    {
        string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        // Trừ 1 vì mảng bắt đầu từ index 0
        return days[GetWeekdayNumber() - 1];
    }

    public bool IsWeekend()
    {
        int day = GetWeekdayNumber();
        return (day == 6 || day == 7); // Saturday (6) hoặc Sunday (7)
    }

    public bool IsWorkday()
    {
        return !IsWeekend();
    }

    public bool IsInWorkingHours()
    {
        return currentHour >= 8f && currentHour < 17f; // Từ 8:00 đến trước 17:00
    }

    public bool IsInOvertimeHours()
    {
        return currentHour >= 17f && currentHour <= 22f; // Từ 17:00 đến 22:00
    }

    // Hàm kiểm tra thời gian rảnh rỗi (cần điều chỉnh logic)
    public bool IsWorkTime()
    {
        return IsInWorkingHours() || IsInOvertimeHours();
    }


}