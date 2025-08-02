using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Sound Effects")]
    [SerializeField] private AudioClip swordSound;
    [SerializeField] private AudioClip slapSound;
    private AudioSource audioSource;



    [Header("Basic Attack Settings")]
    [SerializeField] private float attackDamage = 20f;
    [SerializeField] private float attackRange = 1f;

    [Header("Slap Settings")]
    [SerializeField] private float slapDamage = 100f;
    [SerializeField] private float slapRange = 1.5f;
    [SerializeField] private float slapCooldown = 3f;
    private bool canSlap = true;

    [Header("Call Help")]
    [SerializeField] private GameObject archer;
    [SerializeField] private GameObject warrior;
    [SerializeField] private float callCooldown = 10f;
    private bool canCall = true;


    [Header("General Settings")]
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyLayer;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    public void Attack()
    {
        animator.SetTrigger("Attack");

        if (swordSound != null && audioSource != null)
            audioSource.PlayOneShot(swordSound);


        // Sald�r� noktas�nda d��man var m� kontrol et
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.SendMessage("TakeDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void Slap()
    {
        if (!canSlap) return;

        canSlap = false;
        Invoke(nameof(ResetSlap), slapCooldown);


        animator.SetTrigger("Slap");


        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, slapRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.SendMessage("TakeDamage", slapDamage, SendMessageOptions.DontRequireReceiver);
        }

        Debug.Log("Osmanl� Tokad� at�ld�!");
    }

    public void PlaySlapSound()
    {
        if (slapSound != null && audioSource != null)
            audioSource.PlayOneShot(slapSound);
    }


    private void ResetSlap()
    {
        canSlap = true;
        Debug.Log("Osmanl� Tokad� tekrar kullan�labilir.");
    }

    public void CallHelp()
    {
        if (!canCall) return;

        canCall = false;
        Invoke(nameof(ResetCall), callCooldown);

        Vector3 spawnPos1 = transform.position + new Vector3(1f, 1f, 0f);
        Vector3 spawnPos2 = transform.position + new Vector3(2.5f, 0.2f, 0f);

        Instantiate(archer, spawnPos1, archer.transform.rotation);
        Instantiate(warrior, spawnPos2, warrior.transform.rotation);

        Debug.Log("2 Yeni�eri �a�r�ld�!");
    }

    private void ResetCall()
    {
        canCall = true;
        Debug.Log("Yeni�eri �a�r�s� tekrar kullan�labilir.");
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, slapRange);
    }
}
