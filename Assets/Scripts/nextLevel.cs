using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    [SerializeField] private int nextLevelIndex; // Bu, �rn. "Level2" i�in 2 olacak.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Mevcut kay�tl� level de�eri
            int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

            // Yeni level daha b�y�kse kaydet
            if (nextLevelIndex > currentLevel)
            {
                PlayerPrefs.SetInt("CurrentLevel", nextLevelIndex);
                PlayerPrefs.Save(); // Kal�c� hale getir
            }

            // Sonraki level sahnesine ge�
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
