using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [Header("Time Settings")]
    public int startingTime = 12;
    [HideInInspector] public int time;

    [Header("UI (TextMeshPro)")]
    public TextMeshProUGUI timeText; 


    private void Awake()
    {
        if (Instance == null) Instance = this;
        time = startingTime;
        UpdateUI();
    }

    /// <summary>
    /// Try to spend time. Returns true if successful.
    /// </summary>
    public bool SpendTime(int amount)
    {
        if (time >= amount)
        {
            time -= amount;
            UpdateUI();
            //TriggerHourEvent();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Add coins (e.g., rewards).
    /// </summary>
    public void AddTime(int amount)
    {
        time += amount;
        UpdateUI();
        //TriggerHourEvent();
    }

    public int GetCurrentHour()
    {
        int hour = 9 + (12 - time); // your mapping logic
        return hour; // returns 9–21 (9 AM to 9 PM)
    }

    public bool HasTime(int amount)
    {
        return time >= amount;
    }


    /// <summary>
    /// Update time display in UI.
    /// </summary>
    private void UpdateUI()
    {
        if (timeText != null)
        {
            timeText.text = ConvertToClock(time);
        }
    }

    private string ConvertToClock(int units)
    {
        // Each unit = 1 hour, starting at 9 AM when units = 12
        int hour = 9 + (12 - units); // move forward in hours
        string suffix = (hour >= 12) ? "PM" : "AM";

        // Convert to 12-hour format
        int displayHour = (hour > 12) ? hour - 12 : hour;

        return $"{displayHour}:00 {suffix}";
    }

}