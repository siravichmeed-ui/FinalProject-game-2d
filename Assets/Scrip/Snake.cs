using UnityEngine;

public class Snake : Enemy
{
    [Header("Snake Settings")]
    public float snakeMoveSpeed = 1.5f;
    public float snakeChaseSpeed = 3f;
    public int snakeDamage = 3;

    protected override void Awake()
    {
        base.Awake();

        moveSpeed = snakeMoveSpeed;
        chaseSpeed = snakeChaseSpeed;
        damage = snakeDamage;
    }

    public override void Die()
    {
        Debug.Log("Snake died.");

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        base.Die();
    }

    public override void Hurt()
    {
        Debug.Log("Snake hurt!");

        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }
    }
}
