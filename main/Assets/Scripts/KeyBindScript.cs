using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyBindScript : MonoBehaviour
{

    private Dictionary<string, KeyCode> keyMappings = new Dictionary<string, KeyCode>();

    public TextMeshProUGUI up, left, down, right, pickup, inventory, drop;
    private GameObject currentKey;

    void Start()
    {
        keyMappings = CustomInputManager.keyMappings;
        UpdateControlTexts();
    }
    private void UpdateControlTexts()
    {
        up.SetText(keyMappings["Up"].ToString());
        down.SetText(keyMappings["Down"].ToString());
        left.SetText(keyMappings["Left"].ToString());
        right.SetText(keyMappings["Right"].ToString());
        pickup.SetText(keyMappings["PickUp"].ToString());
        inventory.SetText(keyMappings["Inventory"].ToString());
        drop.SetText(keyMappings["Drop"].ToString());
    }

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keyMappings[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                currentKey = null;
                // CustomInputManager.PickUp = e.keyCode;
                Debug.Log(e.keyCode);
            }
        }
    }
    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }

    public void SetKeyMap(string keyMap, KeyCode key)
    {
        if (!keyMappings.ContainsKey(keyMap))
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + keyMap);
        keyMappings[keyMap] = key;
    }

    public bool GetKeyDown(string key)
    {
        return Input.GetKeyDown(keyMappings[key]);
    }
}