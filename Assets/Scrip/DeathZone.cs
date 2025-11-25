using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damage = 10000;   // ดาเมจที่อยากให้โดน

    private void OnTriggerEnter2D(Collider2D other)
    {
        // หา Player จากตัวที่ชน (รองรับกรณี collider อยู่ในลูกของ Player)
        Player player = other.GetComponent<Player>();
        if (player == null)
            player = other.GetComponentInParent<Player>();

        if (player != null)
        {
            // เรียกฟังก์ชัน TakeDamage ของ Player
            player.TakeDamage(damage);
            Debug.Log("DamageZone: ทำดาเมจให้ Player = " + damage);
        }
    }
}
