using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Tooltip("Drag all quest assets here")]
    public List<QuestData> quests;

    private HashSet<string> completedQuests = new HashSet<string>();

    //track quest objects
    private Dictionary<string, QuestObject> questObjects = new Dictionary<string, QuestObject>();
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Register quest objects at runtime
    public void RegisterQuestObject(string questId, QuestObject obj)
    {
        if (!questObjects.ContainsKey(questId))
        {
            questObjects[questId] = obj;
        }
    }

    public QuestObject GetQuestObject(string questId)
    {
        questObjects.TryGetValue(questId, out var obj);
        return obj;
    }


    public bool CanStartQuest(QuestData quest)
    {
        if (completedQuests.Contains(quest.id)) return false;

        if (quest.prerequisiteQuest != null &&
            !completedQuests.Contains(quest.prerequisiteQuest.id))
        {
            return false;
        }

        // Check affordability
        return CoinManager.Instance.HasCoins(quest.coinCost) &&
               TimeManager.Instance.HasTime(quest.timeCost);
    }

    public void StartQuest(QuestData quest, Animator questAnimator)
    {
        if (CanStartQuest(quest))
        {
            // Deduct costs
            CoinManager.Instance.SpendCoins(quest.coinCost);
            TimeManager.Instance.SpendTime(quest.timeCost);

            // Apply reward if > 0
            if (quest.coinReward > 0)
                CoinManager.Instance.AddCoins(quest.coinReward);

            completedQuests.Add(quest.id);
            quest.isCompleted = true;

            questAnimator?.SetTrigger("StartQuest");
            Debug.Log($"Quest {quest.questName} completed! Reward: {quest.coinReward} coins");
        }
        else
        {
            Debug.Log("Quest cannot be started.");
        }
    }


    //ADDING STUFF
    public QuestData GetQuestById(string questId)
    {
        return quests.Find(q => q.id == questId);
    }
    //

    public string GetCompletedQuestsSummary()
    {
        return "You go to sleep. You have a hard time falling asleep and wake up multiple times during the night. You wake up in the morning, You're tired, and the pain is worse. \r\n\r\nYou feel gross..." + string.Join(", ", completedQuests);
    }
}
