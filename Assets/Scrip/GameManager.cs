using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;   // เอาไว้เรียกจากสคริปต์อื่น

    public bool isGameActive = true;

    [Header("Win Condition")]
    public int coinRequired = 10;     // ตั้งใน Inspector ได้เลย ว่าต้องการกี่เหรียญ

    private void Awake()
    {
        Instance = this;
    }

    public void Finish(int currentCoin)
    {
        if (!isGameActive) return;

        // ถ้าเหรียญยังไม่ถึง → ยังไม่ชนะ
        if (currentCoin < coinRequired)
        {
            Debug.Log($"ยังชนะไม่ได้ เหรียญ {currentCoin}/{coinRequired}");
            return;
        }

        // ถ้าเหรียญครบแล้ว → ชนะ
        isGameActive = false;
        Debug.Log("YOU WIN!");

        // จะโหลดฉาก หรือเปิด Win UI ก็ใส่เพิ่มตรงนี้ได้
        // SceneManager.LoadScene("WinScene");
    }
}
