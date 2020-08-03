using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance{get; private set;}

    private List<Image> slotPlaceholders = new List<Image>();
    private List<Image> inventoryImages = new List<Image>();


    //list of items that have been added to the inventory
    //Do you need pickup list or just the items in the inventory?
    private List<GameObject> inventoryItems = new List<GameObject>();
    //Should have an event that tells when an item has been added and then update the inventory.
    private GameObject player;

    public InventoryObject inventoryObject;

    private bool itemAdded;

    private GameObject toAdd;

    private int itemIndex;

    private List<string> itemList = new List<string> {"Fire", "Water", "CupricChloride", "LithiumChloride", "SodiumChloride", "CalciumChloride", "PotassiumChloride", "CalciumOxide", "Caesium"};

    public Dictionary<string, int> inventoryDict;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(string item in itemList){
            var image = GameObject.FindGameObjectWithTag(item).GetComponent<Image>();
            
            image.enabled = false;
            
            slotPlaceholders.Add(image);
        }
        
        player = GameObject.FindGameObjectWithTag("Player");
        inventoryDict = player.GetComponent<CollectItem>().inventoryDict;

        

    }
    
    void SetInventory(){
        toAdd = inventoryObject.PickupList[inventoryObject.PickupList.Count - 1]; //name refers to the actual item name
        GameObject.FindGameObjectWithTag(toAdd.name).GetComponent<Image>().enabled = true;
        inventoryItems.Add(toAdd.gameObject);
        inventoryObject.PickupList.RemoveAt(inventoryObject.PickupList.Count - 1);
        print("Inv Mgr" + inventoryItems.Count.ToString());
    }


    public List<GameObject> getInventory{
        get
        {

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
