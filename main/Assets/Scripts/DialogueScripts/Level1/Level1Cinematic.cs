using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Cinematic : MonoBehaviour
{
    public DialogueState dialogueState;
    private bool CaoNotEncounteredYet;
    // Start is called before the first frame update
    void Start()
    {
        if (!dialogueState.SpokeOnLevel1)
        {
            this.GetComponents<DialogueTrigger>()[0].TriggerDialogue(); //Intro Dialogue
            dialogueState.SpokeOnLevel1 = true;
            CaoNotEncounteredYet = true;
        }
    }

    public void onCaOFound()
    {
        if (CaoNotEncounteredYet)
        {
            this.GetComponents<DialogueTrigger>()[1].TriggerDialogue();
        }
        CaoNotEncounteredYet = false;
    }
}
