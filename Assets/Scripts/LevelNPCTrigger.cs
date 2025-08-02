using UnityEngine;
using TMPro;

public class LevelNPCTrigger : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform spawnPoint;
    private GameObject spawnedNPC;
    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasSpawned && other.CompareTag("Player"))
        {
            spawnedNPC = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
            hasSpawned = true;

            NPCDialogue npcDialogue = spawnedNPC.GetComponent<NPCDialogue>();
            if (npcDialogue != null)
            {
                npcDialogue.dialoguePanel = GameObject.Find("DialoguePanel");
                npcDialogue.dialogueTextUI = GameObject.Find("DialogueText").GetComponent<TMP_Text>();

                npcDialogue.ShowRandomDialogue();

            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (spawnedNPC != null && other.CompareTag("Player"))
        {
            Destroy(spawnedNPC);
            hasSpawned = false;
        }
    }
}
