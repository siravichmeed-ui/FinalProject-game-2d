using System.Collections;
using UnityEngine;

public class Skeleton : Enemy
{
    [Header("Skeleton FX")]
    public float hurtDuration = 0.25f;
    private bool isHurting = false;

    public override void Hurt()
    {
        if (isHurting) return;

        Debug.Log("Skeleton hurt!");

        isHurting = true;

        // ถ้ามี Animation Hurt
        if (animator != null)
            animator.SetTrigger("Hurt");

        // หยุดขยับชั่วคราว จะได้เห็น Hurt ชัดเจน
        StartCoroutine(HurtRoutine());
    }

    private IEnumerator HurtRoutine()
    {
        float oldMove = moveSpeed;
        float oldChase = chaseSpeed;

        moveSpeed = 0;
        chaseSpeed = 0;

        yield return new WaitForSeconds(hurtDuration);

        moveSpeed = oldMove;
        chaseSpeed = oldChase;

        isHurting = false;
    }

    public override void Die()
    {
        Debug.Log("Skeleton died.");

        if (animator != null)
            animator.SetTrigger("Die");

        base.Die();
    }
}
