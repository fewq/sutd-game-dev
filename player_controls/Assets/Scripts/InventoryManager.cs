using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance{get; private set;}
    private List<Transform> itemPlaceholders = new List<Transform>();

    private List<Image> slotPlaceholders = new List<Image>();
    private List<Image> inventoryImages = new List<Image>();

    //list of items that have been added to the inventory
    //Do you need pickup list or just the items in the inventory?
    private List<GameObject> inventoryItems = new List<GameObject>();
    //Should have an event that tells when an item has been added and then update the inventory.
    private GameObject player;

    public InventoryObject inventoryObject;

    private bool itemAdded;
    Transform inventory;
    // Start is called before the first frame update
    void Awake()
    {
        for(int i=1; i<9; i++){
            itemPlaceholders.Add(GameObject.FindGameObjectWithTag("Item"+i.ToString()).transform);
            var image = GameObject.FindGameObjectWithTag("Slot"+i.ToString()).transform.GetChild(0).GetComponent<Image>();
            image.enabled = false;
            slotPlaceholders.Add(image);
        }
        
        player = GameObject.FindGameObjectWithTag("Player");

        foreach(Transform slot in itemPlaceholders){
            Image slotimage = slot.GetComponent<Image>();
            slotimage.enabled = false;
            inventoryImages.Add(slotimage);
        }
        

    }
    
    void SetInventory(){
        for(int j=0; j<inventoryImages.Count; j++){
            if(!inventoryImages[j].IsActive()){
                inventoryImages[j].enabled = true;
                slotPlaceholders[j].enabled = true;
                //Get the most recent object from the list. This is the actual gameobject item, not placeholder image.
                //Set the new placeholder image 
                var toAdd = inventoryObject.PickupList[inventoryObject.PickupList.Count - 1];
                inventoryImages[j].sprite = toAdd.transform.GetComponent<SpriteRenderer>().sprite;
                slotPlaceholders[j].sprite = toAdd.transform.GetComponent<SpriteRenderer>().sprite;

                //I need something that can remove items from the list when it hits 0 count and add when crafted.
                inventoryItems.Add(toAdd.gameObject);
                inventoryObject.PickupList.RemoveAt(inventoryObject.PickupList.Count - 1);
                break;
            }
        }
    }

    public List<GameObject> getInventory{
        get{
            return inventoryItems;
        }
    }



    // Update is called once per frame
    void Update()
    {
        itemAdded = player.GetComponent<CollectItem>().itemAdded;
        if(itemAdded){
            SetInventory();
        }
        player.GetComponent<CollectItem>().itemAdded = false;
    }
}
