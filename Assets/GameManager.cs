using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 單例模式
    public TextMeshProUGUI scoreText; // 用於顯示分數的 UI 文本
    public TextMeshProUGUI accuracyText; // 用於顯示命中率的 UI 文本
    private int score = 0; // 初始化分數
    private int totalShots = 0; // 總發射次數
    private int farmerHits = 0; // 命中 Farmer 次數

    private void Awake()
    {
        // 確保只有一個 GameManager 存在
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        // 增加分數
        score += amount;
        UpdateUI();
    }

    public void AddShot(bool hitFarmer)
    {
        // 增加總發射次數
        totalShots++;

        // 如果命中 Farmer，增加命中次數
        if (hitFarmer)
        {
            farmerHits++;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        // 更新分數 UI
        scoreText.text = "Score: " + score;

        // 計算並更新命中率 UI
        float accuracy = (totalShots > 0) ? ((float)farmerHits / totalShots) * 100 : 0;
        accuracyText.text = "Accuracy: " + accuracy.ToString("F2") + "%";
    }
}
