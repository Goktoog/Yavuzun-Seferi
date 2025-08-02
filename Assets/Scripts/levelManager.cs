using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Transform playerTransform;

    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

        // T�m butonlar� kontrol et
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;

            // Buton aktif mi de�il mi?
            if (levelIndex <= currentLevel)
            {
                levelButtons[i].interactable = true;

                // Listener ekle (�nce temizle)
                int sceneIndex = levelIndex; // local de�i�ken �art
                levelButtons[i].onClick.RemoveAllListeners();
                levelButtons[i].onClick.AddListener(() => LoadLevel(sceneIndex));
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }

        // Player ikonunu aktif seviyeye yerle�tir
        if (currentLevel - 1 < levelButtons.Length)
        {
            Vector3 iconPos = levelButtons[currentLevel - 1].transform.position;
            playerTransform.position = iconPos;
        }
    }

    public void LoadLevel(int levelIndex)
    {
        // Gerekirse bu sahneleri Build Settings'e eklemeyi unutma
        string sceneName = "Level" + levelIndex;
        SceneManager.LoadScene(sceneName);
    }
}
