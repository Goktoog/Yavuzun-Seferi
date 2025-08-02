using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Burada dusmanin cani sifirin altina duserse olmesini sagliyoruz.
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took damage: " + damage + ", Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died");

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0.5f;
            rb.linearVelocity = new Vector2(0, 5f); // Yukari firlat
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Gorsel dpnme efekti
        StartCoroutine(RotateAndDestroy());
    }

    private System.Collections.IEnumerator RotateAndDestroy()
    {
        float timer = 0f;
        float duration = 1f;

        while (timer < duration)
        {
            transform.Rotate(0, 0, 10 * Time.deltaTime); // G�rsel olarak d�nd�r
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }


}
