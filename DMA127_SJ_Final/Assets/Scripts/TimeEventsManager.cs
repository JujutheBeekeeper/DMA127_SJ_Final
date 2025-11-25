//using UnityEngine;
//using UnityEngine.Events;
//using System.Collections.Generic;

//public class TimeEventManager : MonoBehaviour
//{
//    [System.Serializable]
//    public class TimedEvent
//    {
//        [Tooltip("Hour in 24-hour format (9 = 9 AM, 16 = 4 PM, etc.)")]
//        public int triggerHour;
//        public UnityEvent onTriggered;
//    }

//    [Header("Timed Events")]
//    public List<TimedEvent> events = new List<TimedEvent>();

//    private void OnEnable()
//    {
//        if (TimeManager.Instance != null)
//            TimeManager.Instance.OnHourChanged += HandleHourChanged;
//    }

//    private void OnDisable()
//    {
//        if (TimeManager.Instance != null)
//            TimeManager.Instance.OnHourChanged -= HandleHourChanged;
//    }

//    private void HandleHourChanged(int timeUnits, string clockTime)
//    {
//        int currentHour = TimeManager.Instance.GetCurrentHour(); // returns 9–21

//        foreach (var timedEvent in events)
//        {
//            if (timedEvent.triggerHour == currentHour)
//            {
//                timedEvent.onTriggered?.Invoke();
//                Debug.Log($"Triggered event at {clockTime}");
//            }
//        }
//    }
//}

