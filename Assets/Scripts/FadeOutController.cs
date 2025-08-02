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
        fadeImageObj.SetActive(true); // Ölünce sadece image açýlýr
        StartCoroutine(FadeRoutine());
    }

    private System.Collections.IEnumerator FadeRoutine()
    {
        float timer = 0f;

        // Fade-in (ekran kararsýn)
        while (timer < fadeDuration)
        {
            float alpha = timer / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1);

        // Fade tamamlandýktan sonra sahne yüklenir
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
