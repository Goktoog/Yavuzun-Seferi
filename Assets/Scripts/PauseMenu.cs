using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    [Header("Music")]
    public GameObject musicOnButton;
    public GameObject musicOffButton;
    public AudioSource musicSource;

    [Header("SFX")]
    public GameObject sfxOnButton;
    public GameObject sfxOffButton;
    public AudioSource sfxSource;
    public AudioClip resumeSFX;
    public AudioClip pauseSFX;
    public AudioClip menuSFX;

    private bool sfxEnabled;

    void Start()
    {
        // Müzik ayarýný uygula
        bool isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        musicSource.mute = !isMusicOn;
        musicOnButton.SetActive(isMusicOn);
        musicOffButton.SetActive(!isMusicOn);

        // SFX ayarýný uygula
        sfxEnabled = PlayerPrefs.GetInt("SFXOn", 1) == 1;
        sfxOnButton.SetActive(sfxEnabled);
        sfxOffButton.SetActive(!sfxEnabled);
    }

    public void Pause()
    {
        PlaySFX(pauseSFX);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        PlaySFX(resumeSFX);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ToggleMusic()
    {
        bool isCurrentlyOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        bool newState = !isCurrentlyOn;

        PlayerPrefs.SetInt("MusicOn", newState ? 1 : 0);
        PlayerPrefs.Save();

        musicSource.mute = !newState;
        musicOnButton.SetActive(newState);
        musicOffButton.SetActive(!newState);
    }

    public void ToggleSFX()
    {
        sfxEnabled = !sfxEnabled;
        PlayerPrefs.SetInt("SFXOn", sfxEnabled ? 1 : 0);
        PlayerPrefs.Save();

        sfxOnButton.SetActive(sfxEnabled);
        sfxOffButton.SetActive(!sfxEnabled);
    }

    public void GoToMainMenu()
    {
        PlaySFX(menuSFX);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void PlaySFX(AudioClip clip)
    {
        if (sfxEnabled && sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
