using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DialogueChoiceButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI choiceText;

    private int choiceIndex = -1;


    private void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();

        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        Debug.Log("Button clicked! Choice index: " + choiceIndex);

        // Update choice index
        GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(choiceIndex);

        // Immediately continue the dialogue
        GameEventsManager.instance.dialogueEvents.RequestContinueDialogue();

        GameEventsManager.instance.dialogueEvents.DialogueFinished();
    }


    //private void Awake()
    //{
    //    if (button != null)
    //    {
    //        button.onClick.AddListener(() =>
    //        {
    //            Debug.Log("Button clicked! Choice index: " + choiceIndex);
    //            GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(choiceIndex);
    //        });
    //    }
    //}



    public void SetChoiceText(string choiceTextString)
    {
        choiceText.text = choiceTextString;
    }

    public void SetChoiceIndex(int choiceIndex)
    {
        this.choiceIndex = choiceIndex;
    }


    //public void SelectButton()
    //{
    //    button.Select();
    //}

    //public void OnSelect(BaseEventData eventData)
    //{
    //    GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(choiceIndex);
    //}
}