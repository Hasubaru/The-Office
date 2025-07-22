using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TimeSystem timeSystem;

    public int currentDay = 1; // Day 1
    public string[] weekdays = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

    public string CurrentWeekday => weekdays[(currentDay - 1) % 7];

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AdvanceDay()
    {
        currentDay++;
        timeSystem.ResetTime();
        Debug.Log("New Day: " + CurrentWeekday);
    }
}
