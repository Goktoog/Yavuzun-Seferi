using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 2f;
    public float chaseRange = 5f;
    public Transform player;
    private Animator animator;
    [SerializeField] private Transform visionPoint;

    private bool isFacingRight = true;
    private int currentPointIndex = 0;
    private bool chasingPlayer = false;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(visionPoint.position, player.position);

        if (distanceToPlayer < chaseRange)
        {
            chasingPlayer = true;
        }
        else if (distanceToPlayer > chaseRange + 2f)
        {
            chasingPlayer = false;
        }

        if (chasingPlayer)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
        animator.SetFloat("Speed", moveSpeed);
    }

    void Patrol()
    {
        Transform target = patrolPoints[currentPointIndex];

        // Flip yönü kontrolü
        FlipDirection(target.position.x);

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    void ChasePlayer()
    {
        // Flip yönü kontrolü
        FlipDirection(player.position.x);

        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void FlipDirection(float targetX)
    {
        if (transform.position.x < targetX && !isFacingRight)
        {
            Flip();
        }
        else if (transform.position.x > targetX && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmos()
    {
        if (visionPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(visionPoint.position, chaseRange);
        }
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            Gizmos.color = Color.green;
            foreach (Transform point in patrolPoints)
            {
                Gizmos.DrawSphere(point.position, 0.1f);
            }
        }
    }
}
