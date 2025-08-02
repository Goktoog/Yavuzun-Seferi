using UnityEngine;

public class AllyAI : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 2f;
    public float attackRange = 1f;
    public float jumpForce = 5f;
    public float attackDamage = 10f;
    public float health = 20f;
    public LayerMask enemyLayer;
    public Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public Transform frontCheck;
    [SerializeField] private float attackCooldown = 3f;
    private float nextAttackTime = 0f;

    private Rigidbody2D rb;
    private Animator animator;
    private float nextHealthTick;
    private float direction = 1f;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isFacingRight = true; // Yön durumu takibi

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        nextHealthTick = Time.time + 1f;
    }

    void Update()
    {
        // Her saniye can azalt
        if (Time.time >= nextHealthTick)
        {
            health -= 1f;
            nextHealthTick = Time.time + 1f;

            if (health <= 0)
            {
                Die();
                return;
            }
        }

        // En yakın düşmanı bul
        Collider2D target = FindClosestEnemy();

        if (target != null)
        {
            float dist = Vector2.Distance(transform.position, target.transform.position);

            // Düşmana yönel
            direction = Mathf.Sign(target.transform.position.x - transform.position.x);

            if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
                Flip();

            if (dist > attackRange)
            {
                Move();
            }
            else if (Time.time >= nextAttackTime)
            {
                Attack(target.gameObject);
                nextAttackTime = Time.time + attackCooldown;
            }

        }
        else
        {
            // Düşman yoksa sağa yürü
            direction = 1f;
            if (!isFacingRight) Flip();
            Move();
        }
    }

    void Move()
    {
        CheckGround();
        CheckFront();

        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        animator?.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
    }

    void CheckGround()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
    }

    void CheckFront()
    {
        Vector2 rayDirection = new Vector2(direction, -0.3f).normalized; // Hafif aşağı doğru
        RaycastHit2D hit = Physics2D.Raycast(frontCheck.position, rayDirection, 0.65f, groundLayer);

        if (hit.collider != null)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else
            {
                direction *= -1;
                Flip();
            }
        }
    }

    void Attack(GameObject enemy)
    {
        animator?.SetTrigger("IsAttacking");
        enemy.SendMessage("TakeDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
    }

    void Die()
    {
        animator?.SetTrigger("Die");

        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (var col in colliders)
        {
            col.enabled = false;
        }

        this.enabled = false;
        Destroy(gameObject, 2f);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    Collider2D FindClosestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 5f, enemyLayer);
        Collider2D closest = null;
        float minDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = hit;
            }
        }

        return closest;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f); // Düşman arama alanı
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * 0.1f); // Yerde kontrol çizgisi
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(frontCheck.position, frontCheck.position + new Vector3(direction, -0.3f, 0) * 0.65f); // Ön kontrol çizgisi
    }
}
