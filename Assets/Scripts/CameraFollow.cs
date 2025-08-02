using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("Takip edilecek oyuncu karakteri.")]
    [SerializeField] private Transform player;

    [Tooltip("Kamera takip hýzý.")]
    [SerializeField] private float smoothSpeed = 0.125f;

    [Tooltip("Kamera takip offseti.")]
    [SerializeField] private Vector3 offset;

    private playerHealth playerHealthScript;


    private void Start()
    {
        if (player != null)
            playerHealthScript = player.GetComponent<playerHealth>();
    }

    private void Update()
    {
        if (player == null || playerHealthScript == null)
        {
            Debug.LogWarning("Player veya playerHealth eksik.");
            return;
        }

        // Oyuncu ölü ise takip etme
        if (playerHealthScript.isDead)
            return;

        // Takip pozisyonunu hesapla
        Vector3 desiredPosition = new Vector3(
            player.position.x + offset.x,
            player.position.y + offset.y,
            transform.position.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
