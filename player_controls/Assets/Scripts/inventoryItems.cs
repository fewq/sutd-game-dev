using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Inventory", menuName="InventoryDict", order=2)]
public class inventoryItems : ScriptableObject
{

    private Dictionary<string, int> invItems;


    public Dictionary<string, int> InvItems{
        get{
            return invItems;
        }
        set{
            invItems = value;
        }
        
    }

}
