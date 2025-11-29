using UnityEngine;

public class Dragon : Enemy
{
    [Header("Fireball Attack")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireCooldown = 3f;
    public float fireRange = 8f;

    private float lastFireTime = 0f;

    protected override void HandleAI()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (player.position.x > transform.position.x && facingLeft)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingLeft = false;
        }
        else if (player.position.x < transform.position.x && !facingLeft)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingLeft = true;
        }

        if (distance > fireRange)
        {
            base.HandleAI();
            return;
        }

        if (Time.time - lastFireTime >= fireCooldown)
        {
            lastFireTime = Time.time;
            ShootFireball();
        }

        if (distance <= retrieveDistance)
        {
            animator.SetBool("Attack 1", true);
        }
        else
        {
            animator.SetBool("Attack 1", false);
        }
    }

    void ShootFireball()
    {
        if (fireballPrefab == null || firePoint == null) return;

        animator.SetTrigger("Cast");

        Vector2 dir = (player.position - firePoint.position).normalized;

        GameObject fb = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        fb.GetComponent<Fireball>().Init(dir);
    }

    public override void Hurt()
    {
        Debug.Log("Boss hurt!");
        animator.SetTrigger("Hurt");
    }

    public override void Die()
    {
        Debug.Log("Boss died!");
        animator.SetTrigger("Die");
        Destroy(gameObject, 2f);
    }
}
