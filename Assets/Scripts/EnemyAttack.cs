using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    public int attackDamage = 10;
    public Transform attackPoint;
    public LayerMask playerLayer;

    private Animator animator;
    private float lastAttackTime;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (hit != null && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack(hit.gameObject);
            lastAttackTime = Time.time;
        }
    }

    void Attack(GameObject player)
    {
        animator?.SetTrigger("IsAttacking");

        // Damage uygulama (Player scriptinde "TakeDamage" metodu varsa)
        player.SendMessage("TakeDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
