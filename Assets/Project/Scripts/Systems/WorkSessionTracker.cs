using UnityEngine;

/// <summary>
/// Ghi lại tổng thời gian người chơi đã làm việc trong ngày.
/// </summary>
public class WorkSessionTracker : MonoBehaviour
{
    public static WorkSessionTracker Instance;

    private float totalWorkTimeToday = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Gọi mỗi khung hình khi đang làm việc để cộng dồn thời gian.
    /// </summary>
    public void AddWorkTime(float timeDelta)
    {
        totalWorkTimeToday += timeDelta;
    }

    /// <summary>
    /// Lấy tổng thời gian làm việc hôm nay.
    /// </summary>
    public float GetWorkTime()
    {
        return totalWorkTimeToday;
    }

    /// <summary>
    /// Reset lại mỗi ngày.
    /// </summary>
    public void ResetDaily()
    {
        totalWorkTimeToday = 0f;
    }
}
