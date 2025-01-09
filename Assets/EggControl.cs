using UnityEngine;

public class EggControl : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        bool hitFarmer = false; // 初始化命中狀態

        // 通知 GameManager 更新總發射次數，並傳遞是否命中 Farmer 的資訊
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Farmer"))
        {
            if (collision.gameObject.CompareTag("Farmer"))
            {
                GameManager.instance.AddScore(1);
                hitFarmer = true;
            }
            GameManager.instance.AddShot(hitFarmer); // 先更新數據
            Destroy(gameObject); // 再銷毀物體
        }

    }
}
