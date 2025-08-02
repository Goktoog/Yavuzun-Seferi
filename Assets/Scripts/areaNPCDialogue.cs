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
        // Baþlangýçta paneli kapalý tut
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Oyuncu NPC'nin collider'ýna girdiðinde diyalog açýlacak
        if (other.CompareTag("Player"))
        {
            ShowRandomDialogue();
        }
    }

    void Update()
    {
        // Eðer diyalog aktifse ve kullanýcý ekrana týklarsa, diyalog kapanacak
        if (dialogueActive)
        {
            // Mouse týklamasý veya dokunmatik ekran
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                CloseDialogue();
            }
        }
    }

    void ShowRandomDialogue()
    {
        // Eðer replik dizisi boþsa ya da panel/metin UI öðeleri eksikse, iþlemi durdur
        if (speechLines.Length == 0 || dialoguePanel == null || dialogueTextUI == null)
            return;

        // Random replik seç ve paneli göster
        string chosenLine = speechLines[Random.Range(0, speechLines.Length)];
        dialoguePanel.SetActive(true);
        dialogueTextUI.text = chosenLine;
        dialogueActive = true;  // Diyalog aktif
    }

    void CloseDialogue()
    {
        dialoguePanel.SetActive(false); // Paneli kapat
        dialogueActive = false;  // Diyalog kapalý
    }
}
