using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinCount = 0;
    public TextMeshProUGUI coinText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateCoinUI();
    }

    // Update is called once per frame
    void UpdateCoinUI()
    {
        coinText.text = "Coins: " + coinCount;
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateCoinUI();
    }
}
