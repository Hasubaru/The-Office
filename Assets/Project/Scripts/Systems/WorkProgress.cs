using UnityEngine;

public class WorkProgress : MonoBehaviour
{
    public static WorkProgress Instance;

    [Header("Progress làm việc")]
    public float currentProgress = 0f; // phần trăm task hiện tại
    public int tasksCompletedToday = 0; // số lượng task hoàn thành trong ngày

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Gọi khi người chơi làm việc, tăng phần trăm tiến độ task hiện tại
    /// </summary>
    public void AddProgress(float amount)
    {
        currentProgress += amount;

        if (currentProgress >= 100f)
        {
            currentProgress = 0f;
            tasksCompletedToday++;
            Debug.Log($"✅ Đã hoàn thành 1 task. Tổng cộng hôm nay: {tasksCompletedToday}");
        }
    }

    /// <summary>
    /// Bắt đầu làm việc (nếu cần xử lý riêng)
    /// </summary>
    public void StartWork()
    {
        Debug.Log("▶️ Tiến hành làm việc.");
    }

    /// <summary>
    /// Dừng làm việc
    /// </summary>
    public void StopWork()
    {
        Debug.Log("⏹️ Đã dừng công việc.");
    }

    public int GetTasksCompletedToday()
    {
        return tasksCompletedToday;
    }

    public void ResetDailyProgress()
    {
        currentProgress = 0f;
        tasksCompletedToday = 0;
    }

    public float GetCurrentProgress()
    {
        return currentProgress;
    }
}
