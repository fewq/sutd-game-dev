using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemHover : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private InventoryManager inventoryManager;

    private CollectItem collectItem;

    public Text itemDescription;
    
    private void Awake() {
        inventoryManager =  GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        itemDescription = GameObject.Find("ItemDescription").GetComponent<Text>();
        collectItem = GameObject.Find("Player").GetComponent<CollectItem>();
    }


    // Start is called before the first frame update
    public void OnPointerExit(PointerEventData pointer){
        itemDescription.text = "";
    }

    public void OnPointerEnter(PointerEventData pointer){
        
        int index = System.Convert.ToInt32((transform.name.Substring(transform.name.Length - 1)));
        string tag = GameObject.Find("Item"+index.ToString()).tag;
        if(gameObject.name == "CraftItem1" || gameObject.name == "CraftItem2" ){
            itemDescription.text = "Crafting Slot";
        }
        else if(gameObject.name.Contains("BarSlot")){
            string crafttag = transform.GetChild(0).tag;
            itemDescription.text = crafttag;
        }
        else if(collectItem.inventoryDict[tag] > 0){  
            itemDescription.text = "Selected Item: " + GameObject.Find("Item"+index.ToString()).tag;
        }
        else{
            itemDescription.text = "No item available in slot";
        }
        
    }
}
