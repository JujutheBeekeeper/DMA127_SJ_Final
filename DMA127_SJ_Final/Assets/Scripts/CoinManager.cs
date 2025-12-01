using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [Header("Coin Settings")]
    public int startingCoins = 100;   // Set in Inspector
    [HideInInspector] public int coins;

    [Header("UI (TextMeshPro)")]
    public TextMeshProUGUI coinText; // Drag your TMP Text here

    private void Awake()
    {
        if (Instance == null) Instance = this;
        coins = startingCoins;
        UpdateUI();
    }

    /// <summary>
    /// Try to spend coins. Returns true if successful.
    /// </summary>
    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    public bool HasCoins(int amount)
    {
        return coins >= amount;
    }


    /// <summary>
    /// Add coins (e.g., rewards).
    /// </summary>
    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    /// <summary>
    /// Update coin display in UI.
    /// </summary>
    private void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = $"{coins}";
        }
    }
}
