using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue State", order = 52)]
public class DialogueState : ScriptableObject
{
    public enum Levels { One, Two, Three, Four, Five };
    public bool DialoguePlayedOnLevel1;
    public bool DialoguePlayedOnLevel2;
    public bool DialoguePlayedOnLevel3;
    public bool DialoguePlayedOnLevel4;
    public bool DialoguePlayedOnLevel5;

    public void SetDialoguePlayed(Levels level)
    {
        switch (level)
        {
            case Levels.One:
                DialoguePlayedOnLevel1 = true;
                break;
            case Levels.Two:
                DialoguePlayedOnLevel2 = true;
                break;
            case Levels.Three:
                DialoguePlayedOnLevel3 = true;
                break;
            case Levels.Four:
                DialoguePlayedOnLevel4 = true;
                break;
            case Levels.Five:
                DialoguePlayedOnLevel5 = true;
                break;
            default:
                break;
        }
    }

    public bool GetDialoguePlayed(Levels level)
    {
        switch (level)
        {
            case Levels.One:
                return DialoguePlayedOnLevel1;
            case Levels.Two:
                return DialoguePlayedOnLevel2;
            case Levels.Three:
                return DialoguePlayedOnLevel3;
            case Levels.Four:
                return DialoguePlayedOnLevel4;
            case Levels.Five:
                return DialoguePlayedOnLevel5;
            default:
                Debug.LogError("Invalid argument " + level);
                return false;
        }
    }

    // seems like must have functions without arguments to be used with OnValueChanged toggle
    public void ResetLevel1()
    {
        DialoguePlayedOnLevel1 = false;
    }

    public void ResetLevel2()
    {
        DialoguePlayedOnLevel2 = false;
    }
    public void ResetLevel3()
    {
        DialoguePlayedOnLevel3 = false;
    }
    public void ResetLevel4()
    {
        DialoguePlayedOnLevel4 = false;
    }
    public void ResetLevel5()
    {
        DialoguePlayedOnLevel5 = false;
    }
    public void ResetDialoguePlayedOnLevel(Levels level)
    {
        switch (level)
        {
            case Levels.One:
                ResetLevel1();
                break;
            case Levels.Two:
                ResetLevel2();
                break;
            case Levels.Three:
                ResetLevel3();
                break;
            case Levels.Four:
                ResetLevel4();
                break;
            case Levels.Five:
                ResetLevel5();
                break;
            default:
                break;
        }
    }
}