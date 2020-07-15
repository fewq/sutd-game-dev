using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class CollectItem : MonoBehaviour
{
    private GameObject item;

    private BoxCollider2D playerCollider ;

    public List<GameObject> ItemList = new List<GameObject>();
=======
//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class CollectItem : MonoBehaviour
{
    // variable to hold GameObject to pick up
    private GameObject pickupItem;

    private BoxCollider2D playerCollider;

    // List to store the GameObejct that have been picked up
    public List<GameObject> ItemList = new List<GameObject>();

    // dictionary to store the GameObjects
    public Dictionary<string, int> itemDict;

    // boolean for inventory state
    public bool inventoryState = false;

    // inventory canvas
    public Canvas inventoryCanvas;

    // canvas text
    public Text inventoryText;


>>>>>>> upstream/master
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
<<<<<<< HEAD
    }

    private void OnTriggerExit2D(Collider2D other) {
        
        if(item == null){
            item = other.gameObject;
        }
        else{
            item = null;
            item = other.gameObject;
        }
    }

    void PickObject(){
        if(item != null){
            
            if(Input.GetKey(KeyCode.E)){
                Debug.Log(item);
                ItemList.Add(item);
                item.SetActive(false);
                item = null;
            }
            
        }
        
    }
=======

        // create empty dicitonary to store items
        itemDict = new Dictionary<string, int>(){
            {"Fire", 0},
            {"Water", 0},
            {"CupricChloride", 0},
            {"LithiumChloride", 0},
            {"CalciumChloride", 0},
            {"PotassiumChloride", 0},
            {"SodiumChloride", 0},
            {"Caesium", 0},
            {"CalciumOxide", 0},
            {"Bomb", 0},
            {"BlueLight", 0},
            {"RedLight", 0},
            {"OrangeLight", 0},
            {"PurpleLight", 0},
            {"YellowLight", 0},
            {"CalciumHydroxide", 0}
        };
        // get inventoryText Reference
        inventoryText = GameObject.Find("InventoryText").GetComponent<Text>();
        // get inventoryCanvas reference
        inventoryCanvas = GameObject.Find("InventoryCanvas").GetComponent<Canvas>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // might need to check that this has an "item" tag in the future with "other.tag" since there will be acid rivers
        if (pickupItem == null)
        {
            pickupItem = other.gameObject;
        }
        // irrelevant else statement
        // else
        // {
        //     pickupItem = null;
        // pickupItem = other.gameObject;
        // }
    }

    // this method removes the pickupItem as a target once the player steps out of the collider
    private void OnTriggerExit2D(Collider2D other)
    {
        pickupItem = null;
    }

    void PickObject()
    {
        if (pickupItem != null)
        {
            // reason for pressing E to pick up: in the future, we would need to place items, and the player may accidentally pick it up if they walked arount it
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("picked up " + pickupItem.name);
                // add to ItemList
                ItemList.Add(pickupItem);
                // also add to dictionary for now
                itemDict[pickupItem.name] += 1;
                Debug.Log("itemDict entry -> " + pickupItem.name + ": " + itemDict[pickupItem.name]);
                pickupItem.SetActive(false);
                pickupItem = null;
            }
        }
    }

    void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryState = !inventoryState;
        }
        if (inventoryState)
        {
            inventoryCanvas.GetComponent<Canvas>().enabled = true;
            inventoryText.text = string.Format(@"Inventory: 
Fire: {0}
Water: {1}
CupricChloride: {2} 
LithiumCloride: {3}
CalciumChloride: {4}
PotassiumChloride: {5}
SodiumCloride: {6}
Caesium: {7}
CalciumOxide: {8}
Bomb: {9}
BlueLight: {10}
RedLight: {11}
OrangeLight: {12}
PurpleLight: {13}
YellowLight: {14}
CalciumHydroxide: {15}
", itemDict["Fire"],
itemDict["Water"],
itemDict["CupricChloride"],
itemDict["LithiumChloride"],
itemDict["CalciumChloride"],
itemDict["PotassiumChloride"],
itemDict["SodiumChloride"],
itemDict["Caesium"],
itemDict["CalciumOxide"],
itemDict["Bomb"],
itemDict["BlueLight"],
itemDict["RedLight"],
itemDict["OrangeLight"],
itemDict["PurpleLight"],
itemDict["YellowLight"],
itemDict["CalciumHydroxide"]
);
        }
        else
        {
            inventoryCanvas.GetComponent<Canvas>().enabled = false;
        }
    }

>>>>>>> upstream/master
    // Update is called once per frame
    void Update()
    {
        PickObject();
<<<<<<< HEAD
=======
        ToggleInventory();
>>>>>>> upstream/master
    }
}
