using UnityEngine;

public class QuestObject : MonoBehaviour, IInteractable
{
    [Header("Quest Settings")]
    public QuestData questData;
    public Animator animator;

    [Header("Dialogue (optional)")]
    [SerializeField] private string dialogueKnotName;

    public AudioSource audioSource;

    public void PlaySound()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    //------------------------------------- NEW
    private void Start()
    {
        // Register this quest object with the manager
        if (questData != null)
        {
            QuestManager.Instance.RegisterQuestObject(questData.id, this);
        }
    }
    //------------------------------------------------



    /// <summary>
    /// Called by the player controller when this object is interacted with.
    /// </summary>
    public void Interact()
    {
        // Time restriction check
        if (!questData.alwaysAvailable)
        {
            int currentHour = TimeManager.Instance.GetCurrentHour();
            if (currentHour < questData.availableHourStart || currentHour > questData.availableHourEnd)
            {
                Debug.Log($"Quest {questData.questName} is not available at {currentHour}:00.");
                return; // block quest start
            }
        }



        // If we have a dialogue knot name defined, start dialogue
        if (!string.IsNullOrEmpty(dialogueKnotName))
        {
            Debug.Log("there is dialogue");
            GameEventsManager.instance.dialogueEvents.EnterDialogue(dialogueKnotName);
        }
        // Otherwise, start or finish quest immediately
        else
        {
            QuestManager.Instance.StartQuest(questData, animator);
        }
    }

    public bool IsAvailable()
    {
        if (questData.alwaysAvailable) return true;

        int currentHour = TimeManager.Instance.GetCurrentHour();
        return currentHour >= questData.availableHourStart && currentHour <= questData.availableHourEnd;
    }


}
