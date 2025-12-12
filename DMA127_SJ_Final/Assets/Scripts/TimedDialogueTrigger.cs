using System.Collections;
using UnityEngine;

public class TimedDialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [SerializeField] private string knotName;               // The Ink knot to trigger
    [SerializeField] private int triggerHour = 18;          // Hour threshold (e.g. 18 = 6 PM)
    [SerializeField] private float delaySeconds = 0f;       // Optional delay before triggering

    private void Start()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnHourChanged += HandleHourChanged;
        }
        else
        {
            Debug.LogWarning("TimeManager not found in scene!");
        }
    }

    private void OnDisable()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnHourChanged -= HandleHourChanged;
        }
    }

    private void HandleHourChanged(int hour)
    {
        if (hour >= triggerHour)
        {
            Debug.Log("Hour has been reached");
            if (delaySeconds > 0f)
                StartCoroutine(DelayedTriggerDialogue());
            else
                TriggerDialogue();
        }
    }

    private IEnumerator DelayedTriggerDialogue()
    {
        yield return new WaitForSeconds(delaySeconds);
        TriggerDialogue();
    }

    private void TriggerDialogue()
    {
        if (GameEventsManager.instance?.dialogueEvents != null)
        {
            Debug.Log("TimedDialogueTrigger: Entering dialogue knot " + knotName);
            GameEventsManager.instance.dialogueEvents.EnterDialogue(knotName);
        }
        else
        {
            Debug.LogError("DialogueEvents not initialized!");
        }
    }
}
