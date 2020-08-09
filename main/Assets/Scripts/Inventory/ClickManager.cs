using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private static Dictionary<GameObject, string> itemSelected = new Dictionary<GameObject, string>(); //manages the locks for the crafting slots.
    private static List<GameObject> itemList = new List<GameObject>(); //stores picked up items to be used for crafting
    private bool item1;
    private bool item2;

    
    public InventoryObject invObj;
    // Start is called before the first frame update
    public void changeItemPos(GameObject item){
        if(itemSelected.Count == 2){
            return;
        }
        if(itemList.Count == 0){ //reset the slot locks
            item1 = false;
            item2 = false;
            // itemSelected.Clear();
        }
        //create like a locking system such that only the empty slots get filled with the item
        invObj.InventoryItem = item; //sets the item in the scriptable object to be used by clickmanager
        if(!item1){
            item.GetComponent<RectTransform>().anchoredPosition = GameObject.Find("CraftItem1").GetComponent<RectTransform>().anchoredPosition;

            itemSelected[item] = "item1";
            Debug.Log(item.name);
            itemList.Add(item);
            item1 = true;
        }
        else if (!item2){
            item.GetComponent<RectTransform>().anchoredPosition = GameObject.Find("CraftItem2").GetComponent<RectTransform>().anchoredPosition;
            itemList.Add(item);
            itemSelected[item] = "item2";
            item2 = true;
            
        }
    }

    public void ItemBoolSet(int index){
        if(index == 1){
            item1 = false;
        }
        if(index == 2){
            item2 = false;
        }
    }


    public static List<GameObject> ItemList{
        get{
            return itemList;
        }
    }

    public static Dictionary<GameObject, string> ItemSelected{
        get{
            return itemSelected;
        }
    }
}
