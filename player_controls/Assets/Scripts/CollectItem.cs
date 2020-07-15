using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Dictionary<string, int> inventoryDict;

    // dictionary to store crafting recepies
    public Dictionary<string, (string, string)> recipeDict;

    // boolean for inventory state
    public bool inventoryState = false;

    // inventory canvas
    public Canvas inventoryCanvas;

    // canvas text
    public Text inventoryText;


    // Start is called before the first frame update
    void Start()
    {
        playerCollider = gameObject.GetComponent<BoxCollider2D>();

        // create dicitonary to store items with sample numbers
        inventoryDict = new Dictionary<string, int>(){
            {"Fire", 5},
            {"Water", 1},
            {"CupricChloride", 1},
            {"LithiumChloride", 1},
            {"CalciumChloride", 1},
            {"PotassiumChloride", 1},
            {"SodiumChloride", 1},
            {"Caesium", 1},
            {"CalciumOxide", 1},
            {"Bomb", 1},
            {"BlueLight", 1},
            {"RedLight", 1},
            {"OrangeLight", 1},
            {"PurpleLight", 1},
            {"YellowLight", 1},
            {"CalciumHydroxide", 1}
        };
        // create crafting recepie dict
        recipeDict = new Dictionary<string, (string, string)>{
            {"Bomb", ("Water", "Caesium")},
            {"BlueLight", ("Fire", "CupricChloride")},
            {"RedLight", ("Fire", "LithiumChloride")},
            {"OrangeLight", ("Fire", "CalciumChloride")},
            {"PurpleLight", ("Fire", "PotassiumChloride")},
            {"YellowLight", ("Fire", "SodiumChloride")},
            {"CalciumHydroxide", ("Water", "CalciumOxide")}
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
                inventoryDict[pickupItem.name] += 1;
                Debug.Log("inventoryDict entry -> " + pickupItem.name + ": " + inventoryDict[pickupItem.name]);
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
",
inventoryDict["Fire"],
inventoryDict["Water"],
inventoryDict["CupricChloride"],
inventoryDict["LithiumChloride"],
inventoryDict["CalciumChloride"],
inventoryDict["PotassiumChloride"],
inventoryDict["SodiumChloride"],
inventoryDict["Caesium"],
inventoryDict["CalciumOxide"],
inventoryDict["Bomb"],
inventoryDict["BlueLight"],
inventoryDict["RedLight"],
inventoryDict["OrangeLight"],
inventoryDict["PurpleLight"],
inventoryDict["YellowLight"],
inventoryDict["CalciumHydroxide"]
);
        }
        else
        {
            inventoryCanvas.GetComponent<Canvas>().enabled = false;
        }
    }

    void craft(string craftedItem)
    {
        string rawItem1 = recipeDict[craftedItem].Item1;
        string rawItem2 = recipeDict[craftedItem].Item2;
        if (inventoryDict[rawItem1] > 0 && inventoryDict[rawItem2] > 0)
        {
            inventoryDict[rawItem1]--;
            inventoryDict[rawItem2]--;
            inventoryDict[craftedItem]++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PickObject();
        ToggleInventory();
    }
}
