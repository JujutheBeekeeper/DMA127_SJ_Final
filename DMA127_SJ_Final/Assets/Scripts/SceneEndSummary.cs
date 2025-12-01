using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneEndSummary : MonoBehaviour
{
    public TextMeshProUGUI summaryText;
    public GameObject summaryPanel;

    private void Start()
    {
        if (TimeManager.Instance != null)
            TimeManager.Instance.OnHourChanged += HandleHourChanged;
        else
            Debug.LogWarning("TimeManager not found in scene!");
    }

    private void OnDisable()
    {
        if (TimeManager.Instance != null)
            TimeManager.Instance.OnHourChanged -= HandleHourChanged;
    }


    private void HandleHourChanged(int hour)
    {
        if (hour >= 10) // 9 PM or later
        {
            ShowSummary();
        }
    }

    public void ShowSummary()
    {
        // Update the text
        summaryText.text = QuestManager.Instance.GetCompletedQuestsSummary();

        // Make sure the UI element is visible
        summaryPanel.gameObject.SetActive(true);

        // Show cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Debug.Log("Summary shown and text object activated.");
    }
}
