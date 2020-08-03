using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "CustomInputMananger", order = 51)]
public class CustomInputManager : ScriptableObject
{
    public KeyCode PickUp;
    public KeyCode Inventory;
    public KeyCode Drop;
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
            {"Drop", Drop},
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
}
