using UnityEngine;

public class Snake : Enemy
{
    [Header("Snake Settings")]
    public float snakeMoveSpeed = 1.5f;   // ความเร็วเดินปกติ
    public float snakeChaseSpeed = 3f;    // ความเร็วตอนไล่ผู้เล่น
    public int snakeDamage = 3;           // ดาเมจงู

    protected override void Awake()
    {
        base.Awake();

        // ตั้งค่าเฉพาะของงู (จะไปใช้ใน HandleAI ของ Enemy)
        moveSpeed = snakeMoveSpeed;
        chaseSpeed = snakeChaseSpeed;
        damage = snakeDamage;
    }

    // ถ้าอยากมีอนิเมชัน Die ของงูเอง
    public override void Die()
    {
        Debug.Log("Snake died.");

        if (animator != null)
        {
            animator.SetTrigger("Die");   // ต้องมี Trigger ชื่อ Die ใน Animator ของงู
        }

        base.Die();   // ให้ของแม่จัดการ Destroy
    }

    // ถ้าใน Enemy มีฟังก์ชัน Hurt() แบบ virtual อยู่แล้ว
    // และอยากให้ Snake มีอนิเมชันเจ็บด้วย ให้ใช้แบบนี้ได้
    public override void Hurt()
    {
        Debug.Log("Snake hurt!");

        if (animator != null)
        {
            animator.SetTrigger("Hurt");  // ต้องมี Trigger Hurt ใน Animator ของงู
        }

        // ถ้าไม่อยากทำอะไรเพิ่ม ไม่ต้องเรียก base.Hurt() ก็ได้
        // base.Hurt();
    }
}
