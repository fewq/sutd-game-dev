using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "InventoryObject", order = 1)]
public class InventoryObject : ScriptableObject
{

    private GameObject inventoryItem;

    private List<GameObject> pickupList;

    private Dictionary<string, int> inventory;

    //Do we really want to put these two together? Maybe separating into 2 objects would be better
    public GameObject InventoryItem{
        get{
            return inventoryItem;
        }
        set{
            inventoryItem = value;
        }
    }

    public List<GameObject> PickupList{
        get{
            return pickupList;
        }
        set{
            pickupList = value;
        }
    }
    
}
