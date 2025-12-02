using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneEndSummary : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI summaryText;   // Text element
    [SerializeField] private GameObject summaryPanel;       // Panel container (set in Inspector)

    [Header("Player Controller")]
    [SerializeField] private SimpleFirstPersonController playerController; // drag your player here

    private void Start()
    {
        // Make sure the panel starts hidden
        if (summaryPanel != null)
            summaryPanel.SetActive(false);

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
        if (summaryText != null)
            summaryText.text = QuestManager.Instance.GetCompletedQuestsSummary();

        // Activate the panel
        if (summaryPanel != null)
            summaryPanel.SetActive(true);

        // Show and unlock cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Disable player input so clicks don’t leak into the 3D world
        if (playerController != null)
            playerController.enabled = false;
    }
}
