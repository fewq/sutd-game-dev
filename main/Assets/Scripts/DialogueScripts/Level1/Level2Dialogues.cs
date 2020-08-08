using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Dialogues : MonoBehaviour
{
    public DialogueState dialogueState;

    // Start is called before the first frame update
    void Start()
    {
        if (!dialogueState.SpokeOnLevel2)
        {
            this.GetComponents<DialogueTrigger>()[0].TriggerDialogue(); //Intro Dialogue
            dialogueState.SpokeOnLevel2 = true;
        }
    }
}
