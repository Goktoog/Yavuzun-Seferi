using UnityEngine;
using System.Collections; // Coroutine için gerekli

public class playerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private FadeManager fadeManager; // FadeManager referansı
    public bool isDead { get; private set; } = false;

    private SpriteRenderer[] spriteRenderers;
    private Color originalColor = Color.white;

    [Header("Optional Hit Effect")]
    [SerializeField] private GameObject hitEffectPrefab; 

    void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Kırmızıya çalma efekti başlat
        StartCoroutine(DamageFlash());

        SpawnHitEffect();


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator DamageFlash()
    {
        foreach (var sr in spriteRenderers)
        {
            sr.color = Color.red;
        }

        yield return new WaitForSeconds(0.2f);

        foreach (var sr in spriteRenderers)
        {
            sr.color = originalColor;
        }
    }

    void SpawnHitEffect()
    {
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    public void Die()
    {
        Debug.Log("Player has died.");
        isDead = true;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 1.5f;
        }

        fadeManager.FadeAndRespawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone") && !isDead)
        {
            Die();
        }
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        TakeDamage(25f);
    //    }
    //}
}
