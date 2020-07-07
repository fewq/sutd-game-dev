using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Item Inventory", menuName = "Player Inventory", order = 51)]
public class ItemInventory : ScriptableObject
{
    [SerializeField]
    private List<GameObject> item1;
    [SerializeField]
    private List<GameObject> item2;

    public List<GameObject> Item1{
        get{
            return item1;
        }
    
    }

    public List<GameObject> Item2{
        get{
            return item2;
        }
    }
}
