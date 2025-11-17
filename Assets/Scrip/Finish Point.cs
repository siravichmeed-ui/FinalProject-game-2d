using TMPro;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public GameObject finishUI;              // Panel UI
    public TextMeshProUGUI textRequired;     // ข้อความใน UI

    private void Start()
    {
        if (finishUI != null)
            finishUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        // เอา Player มาจาก Collider
        Player player = collision.GetComponent<Player>();
        if (player == null) return;

        // อัปเดตข้อความใน UI
        if (textRequired != null)   
        {
            textRequired.text =
                $"ต้องการเหรียญ: {GameManager.Instance.coinRequired}\n" +
                $"ปัจจุบัน: {Player.currentCoin}";
        }

        // เปิด UI
        if (finishUI != null)
            finishUI.SetActive(true);

        // ลองเช็คว่าชนะได้ไหม
        GameManager.Instance.Finish(Player.currentCoin);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (finishUI != null)
            finishUI.SetActive(false);
    }
}
