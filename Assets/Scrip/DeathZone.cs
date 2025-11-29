using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damage = 10000;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null)
            player = other.GetComponentInParent<Player>();

        if (player != null)
        {
            player.TakeDamage(damage);
            Debug.Log("DamageZone: ทำดาเมจให้ Player = " + damage);
        }
    }
}
