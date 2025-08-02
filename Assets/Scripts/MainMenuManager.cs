using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject settingsPanel;

    [Header("Music")]
    public AudioSource musicSource;
    public GameObject musicOnButton;
    public GameObject musicOffButton;

    [Header("Button SFX")]
    public GameObject sfxOnButton;
    public GameObject sfxOffButton;
    public AudioSource sfxSource;
    public AudioClip startSFX;
    public AudioClip settingsSFX;
    public AudioClip quitSFX;

    private bool sfxEnabled = true; // Varsayýlan açýk

    public void PlayGame()
    {
        PlaySound(startSFX);
        SceneManager.LoadScene("levelSelectScene");
    }

    public void QuitGame()
    {
        PlaySound(quitSFX);
        Application.Quit();
    }

    public void OpenSettings()
    {
        PlaySound(settingsSFX);
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        PlaySound(quitSFX);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void ToggleMusic()
    {
        if (musicSource.isPlaying)
        {
            PlaySound(quitSFX); // Müzik kapatýlýrken ses efekti çal
            musicSource.Pause();
            musicOnButton.SetActive(false);
            musicOffButton.SetActive(true);
        }
        else
        {
            PlaySound(startSFX); // Müzik açýlýrken ses efekti çal
            musicSource.Play();
            musicOnButton.SetActive(true);
            musicOffButton.SetActive(false);
        }
    }

    public void ToggleSFX()
    {
        sfxEnabled = !sfxEnabled;
     

        sfxOnButton.SetActive(sfxEnabled);
        sfxOffButton.SetActive(!sfxEnabled);

        if (sfxEnabled)
        {
            PlaySound(startSFX);
        }


    }

    void PlaySound(AudioClip clip)
    {
        if (sfxEnabled && sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
