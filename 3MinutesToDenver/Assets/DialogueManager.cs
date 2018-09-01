using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEntry
{
    public string speaker;
    public string dialogue;
    public float duration;

    public DialogueEntry(string speakerName, string dialogue, float duration)
    {
        this.speaker = speakerName;
        this.dialogue = dialogue;
        this.duration = duration;
    }

}
public class DialogueManager : MonoBehaviour {

    public GameObject dialogueBox;
    public Text dialogueText;

    public float timeBetweenDialogues = 0.3f;

    public Queue<DialogueEntry> dialogueEntryQueue;

    Coroutine dialogueRoutine;

    public void Awake()
    {
        dialogueEntryQueue = new Queue<DialogueEntry>();
    }

    public void QueueDialogue(string speaker, string dialogue, float duration)
    {
        DialogueEntry newEntry = new DialogueEntry(speaker, dialogue, duration);
        dialogueEntryQueue.Enqueue(newEntry);

        if(dialogueRoutine == null)
        {
            dialogueRoutine = StartCoroutine(DialogueRoutine());
        }
    }

    IEnumerator DialogueRoutine()
    {
        DialogueEntry nextDialogue = dialogueEntryQueue.Dequeue();

        dialogueText.text = "<i>*" + nextDialogue.speaker + "*</i> " + nextDialogue.dialogue;
        dialogueBox.SetActive(true);

        yield return new WaitForSeconds(nextDialogue.duration);

        dialogueBox.SetActive(false);
        yield return new WaitForSeconds(timeBetweenDialogues);

        if(dialogueEntryQueue.Count > 0)
        {
            dialogueRoutine = StartCoroutine(DialogueRoutine());
        }else
        {
            dialogueRoutine = null;
        }

    }
}
