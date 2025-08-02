using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [TextArea(2, 5)]
    public string[] speechLines;

    public GameObject dialoguePanel;
    public TMP_Text dialogueTextUI;
    private bool dialogueActive = false;


    public void ShowRandomDialogue()
    {
        if (speechLines.Length == 0 || dialoguePanel == null || dialogueTextUI == null)
            return;

        string chosenLine = speechLines[Random.Range(0, speechLines.Length)];
        dialoguePanel.SetActive(true);
        dialogueTextUI.text = chosenLine;
        dialogueActive = true;
    }

    void Update()
    {
        if (!dialogueActive) return;

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            CloseDialogue();
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            CloseDialogue();
        }
#endif
    }

    void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowRandomDialogue();
        }
    }
}