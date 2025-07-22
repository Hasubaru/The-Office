using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    private bool isPlayerNear = false;
    private bool isWorking = false;
    private PlayerController player;

    [SerializeField] private float staminaCostPerSecond = 10f;

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (isWorking)
                StopWorking();
            else if (TimeSystem.Instance != null && TimeSystem.Instance.IsWorkTime())
                StartWorking();
        }

        if (isWorking)
        {
            if (player == null || GameManager.Instance == null || GameManager.Instance.timeSystem == null)
                return;

            player.ReduceStamina(staminaCostPerSecond * Time.deltaTime);

            if (player.currentStamina <= 0 || !GameManager.Instance.timeSystem.IsWorkTime())
            {
                StopWorking();
                return;
            }

            WorkSessionTracker.Instance?.AddWorkTime(Time.deltaTime);
        }
    }

    private void StartWorking()
    {
        if (player == null)
        {
            Debug.LogWarning("⛔ Không tìm thấy player khi bắt đầu làm việc");
            return;
        }

        isWorking = true;
        player.isWorking = true;

        GameManager.Instance.timeSystem.timeSpeed = 0.3f;

        // ✅ Chỉ gọi hàm public, không đụng biến nội bộ
        WorkProgress.Instance?.StartWork();

        Debug.Log("🖥️ Bắt đầu làm việc...");
    }

    private void StopWorking()
    {
        if (player == null)
            return;

        isWorking = false;
        player.isWorking = false;

        GameManager.Instance.timeSystem.timeSpeed = 0.009f;

        // ✅ Gọi hàm public
        WorkProgress.Instance?.StopWork();

        Debug.Log("🔚 Dừng làm việc.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            player = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (isWorking)
                StopWorking();
            player = null;
        }
    }
}
