using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemHover : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    public Text itemDescription;
    // Start is called before the first frame update
    public void OnPointerExit(PointerEventData pointer){

    }

    public void OnPointerEnter(PointerEventData pointer){
        itemDescription = GameObject.Find("ItemDescription").GetComponent<Text>();

        if(gameObject.name == "CraftItem1" || gameObject.name == "CraftItem2" ){
            itemDescription.text = "Crafting Slot";
        }
        else if(transform.GetChild(0).GetComponent<Image>().enabled){  
            var index = System.Convert.ToInt32((transform.name.Substring(transform.name.Length - 1)));
            itemDescription.text = "Selected Item: " + GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>().inventoryItems[index-1].name;
        }
        else{
            itemDescription.text = "No item available in slot";
        }
        
    }
}
