using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;


public class DialogueManager : MonoBehaviour
{
    [Header("ink Story")]
    [SerializeField] private TextAsset inkJson;

    private Story story;
    private int currentChoiceIndex = -1;

    private bool dialoguePlaying = false;

    public bool IsDialoguePlaying => dialoguePlaying;

    private InkExternalFunctions inkExternalFunctions;

    private void Awake()
    {
        story = new Story(inkJson.text);

        InkExternalFunctions inkFunctions = new InkExternalFunctions();
        inkFunctions.Bind(story);

    }

    public void ContinueDialogue()
    {
        ContinueOrExitStory();
    }

    private void Start()
    {

        if (GameEventsManager.instance?.dialogueEvents != null)
        {
            GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
            //here he also subscribes to the input to enable or disable, we'll see about what that means later
            GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex += UpdateChoiceIndex;
            
            GameEventsManager.instance.dialogueEvents.onRequestContinueDialogue += ContinueDialogue;
        }
        else
        {
            Debug.LogError("Dialogue events not initialized!");
        }
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
        //here he also subscribes to the input to enable or disable, we'll see about what that means later
        GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex -= UpdateChoiceIndex;
    }

    // here he puts that if dialogue is active and you press the submit button then it will continue the story, instead let's try on mouse down

    private void UpdateChoiceIndex(int choiceIndex)
    {
        this.currentChoiceIndex = choiceIndex;
    }

    private void OnMouseDown()
    {
        // Only register input if dialogue is currently playing
        if (!dialoguePlaying)
        {
            return;
        }

        // If there are choices, don't consume the click — let the UI handle it
        if (story.currentChoices.Count > 0)
        {
            return;
        }

        // Otherwise, continue the story
        ContinueOrExitStory();
    }




    private void EnterDialogue(string knotName)
    {
        //THIS IS ME ADDING SHIT
        Cursor.visible = true; // show cursor for UI interaction
        Cursor.lockState = CursorLockMode.None;

        if (dialoguePlaying)
        {
            return;
        }

        dialoguePlaying = true;

        GameEventsManager.instance.dialogueEvents.DialogueStarted();

        Debug.Log("Entering dialogue for knot name: " + knotName);

        // jump to knot
        if (!knotName.Equals(""))
        {
            story.ChoosePathString(knotName);
        }
        else
        {
            Debug.LogWarning("Knot name was the empty string when entering dialogue");
        }

        //kick off the story
        ContinueOrExitStory();
    }

    private void ContinueOrExitStory()
    {
        // make a choice, if applicable
        if (story.currentChoices.Count > 0 && currentChoiceIndex != -1)
        {
            story.ChooseChoiceIndex(currentChoiceIndex);
            // reset choice index for next time
            currentChoiceIndex = -1;
        }

        if (story.canContinue)
        {
            string dialogueLine = story.Continue();

            // handle the case where there's a empty line of dialogue
            // by continuing until we get a line with content
            while (IsLineBlank(dialogueLine) && story.canContinue)
            {
                dialogueLine = story.Continue();
            }
            //handle the case where the last line of dialogue is blank
            // (empty choice, external function, etc)
            if (IsLineBlank(dialogueLine) && !story.canContinue)
            {
                ExitDialogue();
            }
            else
            {
                GameEventsManager.instance.dialogueEvents.DisplayDialogue(dialogueLine, story.currentChoices);
            }


        }
        else if (story.currentChoices.Count == 0)
        {
            StartCoroutine(ExitDialogue());
        }
    }

    private IEnumerator ExitDialogue()
    {
        //THIS IS ME ADDING SHIT
        Cursor.visible = false; // hide cursor for gameplay
        Cursor.lockState = CursorLockMode.Locked;

        yield return null;

        Debug.Log("Exiting Dialogue");

        dialoguePlaying = false;

        GameEventsManager.instance.dialogueEvents.DialogueFinished();

        //reset story state
        story.ResetState();

    }

    private bool IsLineBlank(string dialogueLine)
    {
        return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("/n");
    }

    //====================================================================


}