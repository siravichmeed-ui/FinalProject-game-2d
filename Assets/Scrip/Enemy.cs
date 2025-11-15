using System.Xml.Serialization;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Health health;

    public bool facingLeft = true;
    public float moveSpeed = 2f;
    public Transform checkPoint;
    public float distance = 1f;
    public LayerMask layerMask;
    public bool inRange = false;
    public Transform player;
    public float attackRange = 10f;
    public float retrieveDistance = 2.5f;
    public float chaseSpeed = 4f;
    public Animator animator;

    public Transform attackPoint;
    public float attackRadius = 1f;
    public LayerMask attackLayer;

    void Awake()
    {
        if (health == null)
            health = GetComponent<Health>();
    }

    void Start()
    {
        if (health != null)
            health.OnDied += Die;
    }

    void OnDestroy()
    {
        if (health != null)
            health.OnDied -= Die;
    }

    void Update()
    {
        if (FindAnyObjectByType<GameManager>().isGameActive == false)
            return;

        // ถ้าอยากกัน null
        if (player == null) return;

        if (Vector2.Distance(transform.position, player.position) <= attackRange)
            inRange = true;
        else
            inRange = false;

        if (inRange)
        {
            if (player.position.x > transform.position.x && facingLeft == true)
            {
                transform.eulerAngles = new Vector3(0f, -180f, 0f);
                facingLeft = false;
            }
            else if (player.position.x < transform.position.x && facingLeft == false)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                facingLeft = true;
            }

            if (Vector2.Distance(transform.position, player.position) > retrieveDistance)
            {
                animator.SetBool("Attack 1", false);
                transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
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
            else if (hit == false && facingLeft == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                facingLeft = true;
            }
        }
    }

    public void Attack()
    {
        Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);
        if (collInfo)
        {
            Player player = collInfo.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(5);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (health == null) return;
        health.TakeDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        if (checkPoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(checkPoint.position, Vector2.down * distance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    void Die()
    {
        Debug.Log(this.transform.name + " Died.");
        Destroy(this.gameObject);
    }
}
