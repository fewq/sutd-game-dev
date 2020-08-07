using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyBindScript : MonoBehaviour
{
    public CustomInputManager customInputManager;

    public TextMeshProUGUI up, left, down, right, pickup, inventory, dropBomb, dropCaoh, restart, droptorchblue, droptorchred, droptorchorange, droptorchpurple, droptorchyellow;
    private GameObject currentKey;

    void Start()
    {
        DisplayControlTexts();
    }
    private void DisplayControlTexts()
    {
        up.SetText(customInputManager.GetString("Up"));
        down.SetText(customInputManager.GetString("Down"));
        left.SetText(customInputManager.GetString("Left"));
        right.SetText(customInputManager.GetString("Right"));
        pickup.SetText(customInputManager.GetString("PickUp"));
        inventory.SetText(customInputManager.GetString("Inventory"));
        dropBomb.SetText(customInputManager.GetString("DropBomb"));
        dropCaoh.SetText(customInputManager.GetString("DropCalciumHydroxide"));
        restart.SetText(customInputManager.GetString("Restart"));
        droptorchblue.SetText(customInputManager.GetString("DropBlueLight"));
        droptorchorange.SetText(customInputManager.GetString("DropOrangeLight"));
        droptorchpurple.SetText(customInputManager.GetString("DropPurpleLight"));
        droptorchred.SetText(customInputManager.GetString("DropRedLight"));
        droptorchyellow.SetText(customInputManager.GetString("DropYellowLight"));
    }

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = customInputManager.GetStringFromKeycode(e.keyCode);
                Debug.Log(currentKey.name);
                switch (currentKey.name)
                {
                    case "Up":
                        customInputManager.Up = e.keyCode;
                        customInputManager.keyMappings["Up"] = e.keyCode;
                        break;
                    case "Down":
                        customInputManager.Down = e.keyCode;
                        customInputManager.keyMappings["Down"] = e.keyCode;
                        break;
                    case "Left":
                        customInputManager.Left = e.keyCode;
                        customInputManager.keyMappings["Left"] = e.keyCode;
                        break;
                    case "Right":
                        customInputManager.Right = e.keyCode;
                        customInputManager.keyMappings["Right"] = e.keyCode;
                        break;
                    case "PickUp":
                        customInputManager.PickUp = e.keyCode;
                        customInputManager.keyMappings["PickUp"] = e.keyCode;
                        break;
                    case "Inventory":
                        customInputManager.Inventory = e.keyCode;
                        customInputManager.keyMappings["Inventory"] = e.keyCode;
                        break;
                    case "DropBomb":
                        customInputManager.DropBomb = e.keyCode;
                        customInputManager.keyMappings["DropBomb"] = e.keyCode;
                        break;
                    case "DropCalciumHydroxide":
                        customInputManager.DropCalciumHydroxide = e.keyCode;
                        customInputManager.keyMappings["DropCalciumHydroxide"] = e.keyCode;
                        break;
                    case "Restart":
                        customInputManager.Restart = e.keyCode;
                        customInputManager.keyMappings["Restart"] = e.keyCode;
                        break;
                    case "Blue":
                        customInputManager.DropBlueLight = e.keyCode;
                        customInputManager.keyMappings["DropBlueLight"] = e.keyCode;
                        break;
                    case "Yellow":
                        customInputManager.DropYellowLight = e.keyCode;
                        customInputManager.keyMappings["DropYellowLight"] = e.keyCode;
                        break;
                    case "Red":
                        customInputManager.DropRedLight = e.keyCode;
                        customInputManager.keyMappings["DropRedLight"] = e.keyCode;
                        break;
                    case "Orange":
                        customInputManager.DropOrangeLight = e.keyCode;
                        customInputManager.keyMappings["DropOrangeLight"] = e.keyCode;
                        break;
                    case "Purple":
                        customInputManager.DropPurpleLight = e.keyCode;
                        customInputManager.keyMappings["DropPurpleLight"] = e.keyCode;
                        break;
                    default:
                        Debug.Log("no such key in keymappings" + currentKey.name);
                        break;
                }
                currentKey = null;
            }
        }

    }
    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }
}