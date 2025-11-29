using System.Xml.Serialization;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected Health health;
    [SerializeField] protected Animator animator;

    [Header("Target")]
    public Transform player;

    [Header("Move Settings")]
    public bool facingLeft = true;
    public float moveSpeed = 2f;
    public float chaseSpeed = 4f;
    public Transform checkPoint;
    public float distance = 1f;
    public LayerMask layerMask;

    [Header("Attack Settings")]
    public int damage = 5;
    public float attackRange = 10f;
    public float retrieveDistance = 2.5f;

    [Header("Melee Hitbox")]
    public Transform attackPoint;
    public float attackRadius = 1f;
    public LayerMask attackLayer;

    protected bool inRange = false;

    protected virtual void Awake()
    {
        if (health == null)
            health = GetComponent<Health>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        if (health != null)
            health.OnDied += Die;
    }

    protected virtual void OnDestroy()
    {
        if (health != null)
            health.OnDied -= Die;
    }

    protected virtual void Update()
    {
        if (FindAnyObjectByType<GameManager>().isGameActive == false)
            return;

        if (player == null) return;

        HandleAI();
    }

    protected virtual void HandleAI()
    {
        inRange = Vector2.Distance(transform.position, player.position) <= attackRange;

        if (inRange)
        {
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

            if (Vector2.Distance(transform.position, player.position) > retrieveDistance)
            {
                animator.SetBool("Attack 1", false);
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    player.position,
                    chaseSpeed * Time.deltaTime
                );
            }
            else
            {
                animator.SetBool("Attack 1", true);
            }
        }
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);

            RaycastHit2D hit = Physics2D.Raycast(checkPoint.position, Vector2.down, distance, layerMask);
            if (hit == false && facingLeft)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                facingLeft = false;
            }
            else if (hit == false && !facingLeft)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                facingLeft = true;
            }
        }
    }

    public virtual void Attack()
    {
        if (attackPoint == null) return;

        Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);
        if (collInfo)
        {
            Player p = collInfo.gameObject.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamage(damage);
            }
        }
    }

    public virtual void TakeDamage(int dmg)
    {
        if (health == null) return;
        health.TakeDamage(dmg);
        if (health.CurrentHealth > 0)
        {
            Hurt();
        }
    }

    public abstract void Hurt();

    protected virtual void OnDrawGizmosSelected()
    {
        if (checkPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(checkPoint.position, Vector2.down * distance);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }

    public virtual void Die()
    {
        Debug.Log($"{name} died.");
        Destroy(gameObject);
    }
}
