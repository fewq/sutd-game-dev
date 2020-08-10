using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueTrigger : MonoBehaviour
{
    public DialogueState dialogueState;
    public DialogueState.Levels level;
    public Dialogue dialogue;
    private bool dialoguePlayed = false;

    public void TriggerDialogue(){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    void Start(){
        if (level != DialogueState.Levels.One && !dialogueState.GetDialoguePlayed(level))
        {
            TriggerDialogue();
            dialogueState.SetDialoguePlayed(level);
        }
    }
}
