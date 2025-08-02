using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    [SerializeField] private int nextLevelIndex; // Bu, örn. "Level2" için 2 olacak.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Mevcut kayýtlý level deðeri
            int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

            // Yeni level daha büyükse kaydet
            if (nextLevelIndex > currentLevel)
            {
                PlayerPrefs.SetInt("CurrentLevel", nextLevelIndex);
                PlayerPrefs.Save(); // Kalýcý hale getir
            }

            // Sonraki level sahnesine geç
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
