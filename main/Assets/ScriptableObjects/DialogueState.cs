using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue State", order = 52)]
public class DialogueState : ScriptableObject
{
    public bool SpokeOnLevel1;

    public void Reset()
    {
        SpokeOnLevel1 = false;
    }
}