using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damage = 10;
    public float speed = 6f;
    public float lifeTime = 3f;

    private Vector2 dir;

    public void Init(Vector2 direction)
    {
        dir = direction.normalized;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null)
        {
            p.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
