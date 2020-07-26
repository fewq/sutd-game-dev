using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName="CustomInputMananger", order=51)]
public class CustomInputManager : ScriptableObject
{

    public static Dictionary<string, KeyCode> keyMappings = new Dictionary<string, KeyCode>
	{
		{"PickUp", KeyCode.E},
		{"Inventory", KeyCode.C},
		{"Drop", KeyCode.B},
		{"Up", KeyCode.W},
		{"Left", KeyCode.A},
		{"Down", KeyCode.S},
		{"Right", KeyCode.D},
	};
}
