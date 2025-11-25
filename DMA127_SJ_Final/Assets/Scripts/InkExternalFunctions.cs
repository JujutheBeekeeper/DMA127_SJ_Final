using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions
{
    public void Bind(Story story)
    {
        story.BindExternalFunction("StartQuest", (string questId) => StartQuest(questId));

    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("StartQuest");
    }

    private void StartQuest(string questId)
    {
        QuestData quest = QuestManager.Instance.GetQuestById(questId);
        QuestObject questObj = QuestManager.Instance.GetQuestObject(questId);

        if (quest != null)
        {
            QuestManager.Instance.StartQuest(quest, questObj?.animator);
        }
        else
        {
            Debug.LogWarning($"Quest with id {questId} not found!");
        }
    }



}
