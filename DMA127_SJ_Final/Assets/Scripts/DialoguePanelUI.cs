using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

public class DialoguePanelUI : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private GameObject contentParent;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private DialogueChoiceButton[] choiceButtons;

    private void Awake()
    {
        contentParent.SetActive(false);
        ResetPanel();
    }

    private void Start()
    {
        if (GameEventsManager.instance?.dialogueEvents != null)
        {
            GameEventsManager.instance.dialogueEvents.onDialogueStarted += DialogueStarted;
            GameEventsManager.instance.dialogueEvents.onDialogueFinished += DialogueFinished;
            GameEventsManager.instance.dialogueEvents.onDisplayDialogue += DisplayDialogue;
        }
        else
        {
            Debug.LogError("Dialogue events not initialized!");
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance?.dialogueEvents != null)
        {
            GameEventsManager.instance.dialogueEvents.onDialogueStarted -= DialogueStarted;
            GameEventsManager.instance.dialogueEvents.onDialogueFinished -= DialogueFinished;
            GameEventsManager.instance.dialogueEvents.onDisplayDialogue -= DisplayDialogue;
        }
    }


    private void DialogueStarted()
    {
        contentParent.SetActive(true);
    }

    private void DialogueFinished()
    {
        contentParent.SetActive(false);

        //reset anything for the next time
        ResetPanel();
    }

    private void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
    {
        dialogueText.text = dialogueLine;

        //defensive check - if there are more choices coming in than we can support, Log an error
        if (dialogueChoices.Count > choiceButtons.Length)
        {
            Debug.LogError("More dialogue ("
                + dialogueChoices.Count + ") came through than are supported ("
                + choiceButtons.Length + ").");
        }

        //start with all of the choice buttons hidden
        foreach (DialogueChoiceButton choiceButton in choiceButtons)
        {
            choiceButton.gameObject.SetActive(false);
        }

        //enable and set info for buttons depending on ink choice unformation
        int choiceButtonIndex = dialogueChoices.Count - 1;
        for (int inkChoiceIndex = 0; inkChoiceIndex < dialogueChoices.Count; inkChoiceIndex++)
        {
            Choice dialogueChoice = dialogueChoices[inkChoiceIndex];
            DialogueChoiceButton choiceButton = choiceButtons[choiceButtonIndex];

            choiceButton.gameObject.SetActive(true);
            choiceButton.SetChoiceText(dialogueChoice.text);
            choiceButton.SetChoiceIndex(inkChoiceIndex);

            //if (inkChoiceIndex == 0)
            //{
            //    choiceButton.SelectButton();
            //    GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(0);
            //}

            var nav = choiceButton.GetComponent<Button>().navigation;
            nav.mode = Navigation.Mode.None;
            choiceButton.GetComponent<Button>().navigation = nav;

            choiceButtonIndex--;
        }


    }

    private void ResetPanel()
    {
        dialogueText.text = "";
    }
}
