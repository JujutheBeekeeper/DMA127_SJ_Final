using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public DialogueEvents dialogueEvents;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Found more than one GameEventsManager in the scene!");
            Destroy(gameObject); // optional safeguard
            return;
        }

        instance = this;
        dialogueEvents = new DialogueEvents();
    }



}
