using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "CustomInputMananger", order = 51)]
public class CustomInputManager : ScriptableObject
{
    public KeyCode PickUp;
    public KeyCode Inventory;
    public KeyCode DropBomb;
    public KeyCode DropCalciumHydroxide;
    public KeyCode DropBlueLight;
    public KeyCode DropOrangeLight;
    public KeyCode DropPurpleLight;
    public KeyCode DropRedLight;
    public KeyCode DropYellowLight;
    public KeyCode Restart;
    public KeyCode Up;
    public KeyCode Left;
    public KeyCode Down;
    public KeyCode Right;
    public Dictionary<string, KeyCode> keyMappings;

    public void OnEnable()
    {
        keyMappings = new Dictionary<string, KeyCode>()
        {
            {"PickUp", PickUp},
            {"Inventory", Inventory},
            {"DropBomb", DropBomb},
            {"DropCalciumHydroxide", DropCalciumHydroxide},
            {"DropBlueLight", DropBlueLight},
            {"DropOrangeLight", DropOrangeLight},
            {"DropPurpleLight", DropPurpleLight},
            {"DropRedLight", DropRedLight},
            {"DropYellowLight", DropYellowLight},
            {"Restart", Restart},
            {"Up", Up},
            {"Left", Left},
            {"Down", Down},
            {"Right", Right}

        };
    }

    public bool GetKeyDown(string key)
    {
        return Input.GetKeyDown(keyMappings[key]);
    }

    public bool GetKey(string key)
    {
        return Input.GetKey(keyMappings[key]);
    }

    public float GetAxisRaw(string axis)
    {
        // Returns only 0, -1 or 1 exactly
        if (axis == "Horizontal")
        {
            if (GetKey("Left")) return -1f;
            else if (GetKey("Right")) return 1f;
            return 0f;
        }

        else if (axis == "Vertical")
        {
            if (GetKey("Up")) return 1f;
            else if (GetKey("Down")) return -1f;
            return 0f;
        }
        Debug.LogError("No such axis: " + axis);
        return 0f;
    }
}
