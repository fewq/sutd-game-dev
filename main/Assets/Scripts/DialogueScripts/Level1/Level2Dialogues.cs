using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Dialogues : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponents<DialogueTrigger>()[0].TriggerDialogue(); //Intro Dialogue
    }

}
