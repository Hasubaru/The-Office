using UnityEngine;
using UnityEngine.UI;

public class WorkUIManager : MonoBehaviour
{
    public static WorkUIManager Instance;

    [Header("Thanh tiến độ làm việc")]
    [SerializeField] private Slider progressSlider;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (progressSlider != null)
            progressSlider.value = 0f;
    }

    private void Update()
    {
        if (WorkProgress.Instance == null || progressSlider == null)
            return;

        // Cập nhật UI theo phần trăm công việc
        progressSlider.value = WorkProgress.Instance.GetCurrentProgress() / 100f;
    }

    public void ResetProgressUI()
    {
        if (progressSlider != null)
            progressSlider.value = 0f;
    }
}
