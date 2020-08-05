using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Cinematic : MonoBehaviour
{
    private bool CaoNotEncounteredYet;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponents<DialogueTrigger>()[0].TriggerDialogue(); //Intro Dialogue
        CaoNotEncounteredYet = true;
    }

    public void onCaOFound(){
        if(CaoNotEncounteredYet){
            this.GetComponents<DialogueTrigger>()[1].TriggerDialogue();
        }
        CaoNotEncounteredYet = false;
    }
}
