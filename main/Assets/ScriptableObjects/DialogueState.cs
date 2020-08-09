using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue State", order = 52)]
public class DialogueState : ScriptableObject
{
    public bool SpokeOnLevel1;
    public bool SpokeOnLevel2;

    public void ResetLevel1()
    {
        SpokeOnLevel1 = false;
    }

    public void ResetLevel2()
    {
        SpokeOnLevel2 = false;
    }
}