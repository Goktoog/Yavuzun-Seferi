using UnityEngine;
using TMPro;

public class areaNPCDialogue : MonoBehaviour
{
    [TextArea(2, 5)]
    public string[] speechLines;

    public GameObject dialoguePanel;
    public TMP_Text dialogueTextUI;

    private bool dialogueActive = false;

    private void Start()
    {
        // Ba�lang��ta paneli kapal� tut
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Oyuncu NPC'nin collider'�na girdi�inde diyalog a��lacak
        if (other.CompareTag("Player"))
        {
            ShowRandomDialogue();
        }
    }

    void Update()
    {
        // E�er diyalog aktifse ve kullan�c� ekrana t�klarsa, diyalog kapanacak
        if (dialogueActive)
        {
            // Mouse t�klamas� veya dokunmatik ekran
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                CloseDialogue();
            }
        }
    }

    void ShowRandomDialogue()
    {
        // E�er replik dizisi bo�sa ya da panel/metin UI ��eleri eksikse, i�lemi durdur
        if (speechLines.Length == 0 || dialoguePanel == null || dialogueTextUI == null)
            return;

        // Random replik se� ve paneli g�ster
        string chosenLine = speechLines[Random.Range(0, speechLines.Length)];
        dialoguePanel.SetActive(true);
        dialogueTextUI.text = chosenLine;
        dialogueActive = true;  // Diyalog aktif
    }

    void CloseDialogue()
    {
        dialoguePanel.SetActive(false); // Paneli kapat
        dialogueActive = false;  // Diyalog kapal�
    }
}
