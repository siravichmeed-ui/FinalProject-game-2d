using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public static int currentCoin = 0;

    [Header("Components")]
    public Animator animator;
    public Rigidbody2D rb;
    [SerializeField] private Health health;

    public float jumpForce = 5f;
    public bool isGrounded = true;

    private float movement;
    public float moveSpeed = 5f;
    private bool facingRight = true;

    

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
        {
            health.OnDied += Die;
        }
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

        coinText.text = currentCoin.ToString();

        movement = Input.GetAxis("Horizontal");
        if (movement < 0f && facingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;
        }
        else if (movement > 0f && facingRight == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            isGrounded = false;
            animator.SetBool("Jump", true);
        }

        if (Mathf.Abs(movement) > .1f)
            animator.SetFloat("Run", 1f);
        else
            animator.SetFloat("Run", 0f);

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * moveSpeed;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
        }
    }

    public void Attack()
    {
        Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);
        if (collInfo)
        {
            Enemy enemy = collInfo.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    public void TakeDamage(int damage)
    {
        if (health == null) return;
        health.TakeDamage(damage);
        if (health.CurrentHealth > 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("Hurt");   
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            currentCoin++;
            other.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Collected");
            Destroy(other.gameObject, 1f);
        }
    }

    void Die()
    {
        Debug.Log("Die");
        FindAnyObjectByType<GameManager>().isGameActive = false;
        Destroy(this.gameObject);
    }
}
