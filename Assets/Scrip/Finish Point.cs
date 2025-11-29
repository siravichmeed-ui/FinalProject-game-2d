using TMPro;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [Header("UI When Not Enough Coins")]
    public GameObject notEnoughUI;
    public TextMeshProUGUI textNotEnough;

    [Header("UI When Win")]
    public GameObject winUI;

    private void Start()
    {
        if (notEnoughUI != null) notEnoughUI.SetActive(false);
        if (winUI != null) winUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null)
            player = collision.GetComponentInParent<Player>();
        if (player == null) return;

        int required = GameManager.Instance.coinRequired;
        int current = Player.currentCoin;

        if (current < required)
        {
            Debug.Log("ยังไม่ถึงเหรียญพอ แสดง UI แบบยังไม่ถึง");

            if (textNotEnough != null)
                textNotEnough.text = $"NEED {required} Coin\nYou Have {current} Coin";

            if (notEnoughUI != null)
                notEnoughUI.SetActive(true);

            return;
        }

        Debug.Log("เหรียญครบ! แสดง Win UI");

        if (winUI != null)
            winUI.SetActive(true);

        GameManager.Instance.Finish(current);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null)
            player = collision.GetComponentInParent<Player>();
        if (player == null) return;

        if (notEnoughUI != null) notEnoughUI.SetActive(false);
        if (winUI != null) winUI.SetActive(false);
    }
}
