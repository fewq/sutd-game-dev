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
            switch(crafttag){
                case("Bomb"):
                    itemDescription.text = crafttag + ": Blow up things in a 3x3 area";
                    break;
                case("CalciumHydroxide"):
                    itemDescription.text = crafttag + ": Strong base that neutralises acid";
                    break;
                default:
                    itemDescription.text = crafttag + ": Provides a good distraction";
                    break;
            }
            // itemDescription.text = crafttag;
        }
        else{
            itemDescription.text = "Selected Item: " + GameObject.Find("Item"+index.ToString()).tag;
        }
        
    }
}
