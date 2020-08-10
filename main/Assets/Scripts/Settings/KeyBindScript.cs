using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyBindScript : MonoBehaviour
{
    public CustomInputManager customInputManager;

    public TextMeshProUGUI up, left, down, right, inventory, dropBomb, dropCaoh, restart, droptorchblue, droptorchred, droptorchorange, droptorchpurple, droptorchyellow;
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
                if (customInputManager.usedKeyCodes.ContainsKey(e.keyCode))
                {
                    // var existingkeys = "";
                    // foreach (KeyValuePair<KeyCode, bool> key in customInputManager.usedKeyCodes)
                    // {
                    //     existingkeys = existingkeys + " " + key.Key;
                    // }
                    // Debug.Log("Existing keys " + existingkeys);
                    // Debug.Log("This key is already being used");
                    return;
                }
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = customInputManager.GetStringFromKeycode(e.keyCode);
                Debug.Log(currentKey.name);
                switch (currentKey.name)
                {
                    case "Up":
                        customInputManager.Up = e.keyCode;
                        customInputManager.updateExistingKeyBinding(customInputManager.keyMappings["Up"], e.keyCode);
                        customInputManager.keyMappings["Up"] = e.keyCode;
                        break;
                    case "Down":
                        customInputManager.Down = e.keyCode;
                        customInputManager.updateExistingKeyBinding(customInputManager.keyMappings["Down"], e.keyCode);
                        customInputManager.keyMappings["Down"] = e.keyCode;
                        break;
                    case "Left":
                        customInputManager.Left = e.keyCode;
                        customInputManager.updateExistingKeyBinding(customInputManager.keyMappings["Left"], e.keyCode);
                        customInputManager.keyMappings["Left"] = e.keyCode;
                        break;
                    case "Right":
                        customInputManager.Right = e.keyCode;
                        customInputManager.updateExistingKeyBinding(customInputManager.keyMappings["Right"], e.keyCode);

                        customInputManager.keyMappings["Right"] = e.keyCode;
                        break;
                    case "Inventory":
                        customInputManager.updateExistingKeyBinding(customInputManager.keyMappings["Inventory"], e.keyCode);
                        customInputManager.Inventory = e.keyCode;
                        customInputManager.keyMappings["Inventory"] = e.keyCode;
                        break;
                    case "DropBomb":
                        customInputManager.updateExistingKeyBinding(customInputManager.keyMappings["DropBomb"], e.keyCode);
                        customInputManager.DropBomb = e.keyCode;
                        customInputManager.keyMappings["DropBomb"] = e.keyCode;
                        break;
                    case "DropCalciumHydroxide":
                        customInputManager.updateExistingKeyBinding(customInputManager.keyMappings["DropCalciumHydroxide"], e.keyCode);
                        customInputManager.DropCalciumHydroxide = e.keyCode;
                        customInputManager.keyMappings["DropCalciumHydroxide"] = e.keyCode;
                        break;
                    case "Restart":
                        customInputManager.updateExistingKeyBinding(customInputManager.keyMappings["Restart"], e.keyCode);
                        customInputManager.Restart = e.keyCode;
                        customInputManager.keyMappings["Restart"] = e.keyCode;
                        break;
                    case "DropBlueLight":
                        customInputManager.updateExistingKeyBinding(customInputManager.keyMappings["DropBlueLight"], e.keyCode);
                        customInputManager.DropBlueLight = e.keyCode;
                        customInputManager.keyMappings["DropBlueLight"] = e.keyCode;
                        break;
                    case "DropYellowLight":
                        customInputManager.updateExistingKeyBinding(customInputManager.keyMappings["DropYellowLight"], e.keyCode);
                        customInputManager.DropYellowLight = e.keyCode;
                        customInputManager.keyMappings["DropYellowLight"] = e.keyCode;
                        break;
                    case "DropRedLight":
                        customInputManager.updateExistingKeyBinding(customInputManager.keyMappings["DropRedLight"], e.keyCode);
                        customInputManager.DropRedLight = e.keyCode;
                        customInputManager.keyMappings["DropRedLight"] = e.keyCode;
                        break;
                    case "DropOrangeLight":
                        customInputManager.updateExistingKeyBinding(customInputManager.keyMappings["DropOrangeLight"], e.keyCode);
                        customInputManager.DropOrangeLight = e.keyCode;
                        customInputManager.keyMappings["DropOrangeLight"] = e.keyCode;
                        break;
                    case "DropPurpleLight":
                        customInputManager.updateExistingKeyBinding(customInputManager.keyMappings["DropPurpleLight"], e.keyCode);
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