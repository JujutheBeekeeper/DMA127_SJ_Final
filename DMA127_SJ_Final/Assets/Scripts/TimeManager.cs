using System;
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

    public event Action<int> OnHourChanged;
    private int lastHour;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            time = startingTime;
            lastHour = GetCurrentHour();
            UpdateUI();
        }
        else
        {
            Destroy(gameObject);
        }
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
            CheckHourEvent();
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
        CheckHourEvent();
    }

    public int GetCurrentHour()
    {
        int hour = 9 + (12 - time); // your mapping logic
        return hour; // returns 9–21 (9 AM to 9 PM)
    }

    public bool HasTime(int amount) => time >= amount;

    private void UpdateUI()
    {
        if (timeText != null)
            timeText.text = ConvertToClock(time);
    }

    private string ConvertToClock(int units)
    {
        int hour = GetCurrentHour();
        string suffix = (hour >= 12) ? "PM" : "AM";
        int displayHour = (hour > 12) ? hour - 12 : hour;
        return $"{displayHour}:00 {suffix}";
    }

    private void CheckHourEvent()
    {
        int currentHour = GetCurrentHour();

        // Fire event when hour changes
        if (currentHour != lastHour)
        {
            lastHour = currentHour;
            OnHourChanged?.Invoke(currentHour);
        }

    }
}