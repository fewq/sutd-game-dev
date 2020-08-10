using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CustomInputMananger", order = 51)]
public class CustomInputManager : ScriptableObject
{
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
    public float Volume;
    public Dictionary<string, KeyCode> keyMappings;
    public Dictionary<KeyCode, bool> usedKeyCodes;

    public void OnEnable()
    {
        keyMappings = new Dictionary<string, KeyCode>()
        {
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

        usedKeyCodes = new Dictionary<KeyCode, bool>(){};
        foreach(KeyValuePair<string, KeyCode> keyMapping in keyMappings)
        {
            usedKeyCodes[keyMapping.Value] = true;
        }
    }


    public void updateExistingKeyBinding(KeyCode existing, KeyCode newKey)
    {
        usedKeyCodes.Remove(existing);   
        usedKeyCodes.Add(newKey, true);
    }

    public bool GetKeyDown(string key)
    {
        // Debug.Log("GetKeyDown: " + key + keyMappings[key].ToString());
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

    public string GetStringFromKeycode(KeyCode keyCode)
    {
        string str = keyCode.ToString();
        return stringMapping(str);
    }

    public string GetString(string key)
    {
        string str = keyMappings[key].ToString();
        return stringMapping(str);
    }

    private string stringMapping(string str)
    {
        if (str.Length < 6) return str; // shortest switch case below has at least 6 chars 
        switch (str)
        {
            case "Alpha0":
                return "0";
            case "Alpha1":
                return "1";
            case "Alpha2":
                return "2";
            case "Alpha3":
                return "3";
            case "Alpha4":
                return "4";
            case "Alpha5":
                return "5";
            case "Alpha6":
                return "6";
            case "Alpha7":
                return "7";
            case "Alpha8":
                return "8";
            case "Alpha9":
                return "9";
            case "LeftArrow":
                return '\u2190'.ToString();
            case "RightArrow":
                return '\u2192'.ToString();
            case "UpArrow":
                return '\u2191'.ToString();
            case "DownArrow":
                return '\u2193'.ToString();
            default:
                return str;
        }
    }
}
