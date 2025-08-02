using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public GameObject fadeImageObj; // sadece Image objesi
    public Image fadeImage;         // Image component'i
    public float fadeDuration = 1f;

    public void FadeAndRespawn()
    {
        fadeImageObj.SetActive(true); // �l�nce sadece image a��l�r
        StartCoroutine(FadeRoutine());
    }

    private System.Collections.IEnumerator FadeRoutine()
    {
        float timer = 0f;

        // Fade-in (ekran karars�n)
        while (timer < fadeDuration)
        {
            float alpha = timer / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1);

        // Fade tamamland�ktan sonra sahne y�klenir
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
