using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneEndSummary : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI summaryText;   // Text element
    [SerializeField] private GameObject summaryPanel;       // Panel container (set in Inspector)

    [Header("Player Controller")]
    [SerializeField] private SimpleFirstPersonController playerController;

    [SerializeField] private int summaryHourThreshold = 10;

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
        if (hour >= summaryHourThreshold) // 9 PM or later
        {
            StartCoroutine(DelayedShowSummary()); 
        }
    }

    private IEnumerator DelayedShowSummary()
    {
        yield return new WaitForSeconds(5f);
        ShowSummary();
    }


    public void ShowSummary()
    {
        if (summaryText != null)
            summaryText.text = QuestManager.Instance.GetCompletedQuestsSummary();

        if (summaryPanel != null)
            summaryPanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (playerController != null)
            playerController.enabled = false;

        // Start listening for any input
        StartCoroutine(WaitForAnyInput());
    }

    private IEnumerator WaitForAnyInput()
    {
        // Wait half a second before listening
        yield return new WaitForSeconds(0.5f);

        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        SceneController.instance.BackToMenuAnim();
    }


}
