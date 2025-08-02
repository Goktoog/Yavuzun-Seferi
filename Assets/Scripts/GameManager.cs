using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PauseMenu pauseMenu;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PauseGame() => pauseMenu.Pause();
    public void ResumeGame() => pauseMenu.Resume();
    public void ToggleSound() => pauseMenu.ToggleMusic();
}
