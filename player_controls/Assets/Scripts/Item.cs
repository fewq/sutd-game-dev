using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "ItemData", order = 1)]
public class Item : ScriptableObject
{

    private string itemName;

    public string ItemName{
        get{
            return itemName;
        }
        set{
            itemName = value;
        }
    }
    
}
