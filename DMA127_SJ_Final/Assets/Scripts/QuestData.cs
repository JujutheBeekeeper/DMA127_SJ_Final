using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    // Unique identifier, auto-set to the asset name
    [field: SerializeField] public string id { get; private set; }

    [Header("Quest Info")]
    public string questName;
    [TextArea] public string description;

    [Header("Time Availability")]
    public bool alwaysAvailable = true;       // default: quest can be started anytime
    [Range(9, 21)] public int availableHourStart = 0;
    [Range(9, 21)] public int availableHourEnd = 23;  

    [Header("Requirements")]
    public int coinCost;
    public int timeCost;

    public int coinReward; //if any

    public QuestData prerequisiteQuest; // drag another QuestData here

    [Header("Repeatability")]
    public bool isRepeatable = false;


    [HideInInspector] public bool isCompleted; // runtime only

    // ensure the id is always the name of the Scriptable Object asset
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
