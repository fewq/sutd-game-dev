using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public List<GameObject> itemSelected = new List<GameObject>();
    private static List<GameObject> itemList = new List<GameObject>();
    private bool item1;
    public LayerMask layerMask;
    private bool item2;

    
    public InventoryObject invObj;
    // Start is called before the first frame update
    public void changeItemPos(GameObject item){
        if(itemSelected.Count == 2){
            return;
        }
        if(itemList.Count == 0){
            item1 = false;
            item2 = false;
        }
        invObj.InventoryItem = item; //sets the item in the scriptable object to be used by clickmanager
        if(!item1){
            Debug.Log("item1");
            item.GetComponent<RectTransform>().anchoredPosition = GameObject.Find("CraftItem1").GetComponent<RectTransform>().anchoredPosition;

            itemSelected.Add(item);
            itemList.Add(item);
            item1 = true;
        }
        else if (!item2){
            
            item.GetComponent<RectTransform>().anchoredPosition = GameObject.Find("CraftItem2").GetComponent<RectTransform>().anchoredPosition;
            itemList.Add(item);
            itemSelected.Add(item);
            item2 = true;
            
        }

        if(item1 && item2){
            itemSelected.Clear();
        }
    }
        



    

    public static List<GameObject> ItemList{
        get{
            return itemList;
        }
    }



    // Update is called once per frame
    void Update()
    {
    }
}
