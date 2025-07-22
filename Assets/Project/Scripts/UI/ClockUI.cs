using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    public TextMeshProUGUI clockText;
    public TextMeshProUGUI dayText;

    private void Update()
    {
        if (TimeSystem.Instance == null) return;

        float hour = TimeSystem.Instance.CurrentHour();
        int intHour = Mathf.FloorToInt(hour);
        int intMinute = Mathf.FloorToInt((hour - intHour) * 60);

        if (clockText != null)
            clockText.text = $"{intHour:00}:{intMinute:00}";

        if (dayText != null)
            dayText.text = $"Day {TimeSystem.Instance.CurrentDay()} ({TimeSystem.Instance.GetDayOfWeek()})";
    }
}
