using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneEndSummary : MonoBehaviour
{
    public TextMeshProUGUI summaryText;

    public void ShowSummary()
    {
        summaryText.text = QuestManager.Instance.GetCompletedQuestsSummary();
    }
}
