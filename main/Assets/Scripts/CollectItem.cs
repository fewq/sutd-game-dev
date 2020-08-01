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
    public List<GameObject> PickupList = new List<GameObject>();

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

    public bool itemAdded = false;

    public InventoryObject inventoryObject;

    public GameObject itemPrefab;


    // Start is called before the first frame update
    void Start()
    {
        playerCollider = gameObject.GetComponent<BoxCollider2D>();

        // create dicitonary to store items with sample numbers
        inventoryDict = new Dictionary<string, int>(){
            {"Fire", 5},
            {"Water", 2},
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
        inventoryCanvas = GameObject.Find("Inventory").GetComponent<Canvas>();
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
                // add to PickupList
                PickupList.Add(pickupItem);
                // also add to dictionary for now
                inventoryDict[pickupItem.name] += 1;
                Debug.Log("inventoryDict entry -> " + pickupItem.name + ": " + inventoryDict[pickupItem.name]);
                // deactivate item
                pickupItem.SetActive(false);
                // destroy item instead
                // Destroy(pickupItem);
                pickupItem = null;
                itemAdded = true;
            }

            //update the scriptable object with the lists.
            inventoryObject.Inventory = inventoryDict;
            inventoryObject.PickupList = PickupList;
        }
    }

    void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            inventoryState = !inventoryState;
            print("InventoryState: " + inventoryState);
        }
        if (inventoryState)
        {
            inventoryCanvas.GetComponent<Canvas>().enabled = true;
            inventoryText.text = $@"Inventory: 
            Fire: {inventoryDict["Fire"]}
            Water: {inventoryDict["Water"]}
            CupricChloride: {inventoryDict["CupricChloride"]} 
            LithiumCloride: {inventoryDict["LithiumChloride"]}
            CalciumChloride: {inventoryDict["CalciumChloride"]}
            PotassiumChloride: {inventoryDict["PotassiumChloride"]}
            SodiumCloride: {inventoryDict["SodiumChloride"]}
            Caesium: {inventoryDict["Caesium"]}
            CalciumOxide: {inventoryDict["CalciumOxide"]}
            Bomb: {inventoryDict["Bomb"]} (Craft: T; Place: F)
            BlueLight: {inventoryDict["BlueLight"]} (Craft: Y; Place: G)
            RedLight: {inventoryDict["RedLight"]} (Craft: U; Place: H)
            OrangeLight: {inventoryDict["OrangeLight"]} (Craft: I; Place: J)
            PurpleLight: {inventoryDict["PurpleLight"]} (Craft: O; Place: K)
            YellowLight: {inventoryDict["YellowLight"]} (Craft: P; Place: L)
            CalciumHydroxide: {inventoryDict["CalciumHydroxide"]} (Craft: [; Place: ;)";
        }
        else
        {
            inventoryCanvas.GetComponent<Canvas>().enabled = false;
        }
    }

    // takes in a string of the desired item to craft
    // checks if there is enough raw materials, then crafts it
    void Craft(string craftedItem)
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

    // takes in 2 raw materials (order doesn't matter) and performs crafting
    public void Craft(string rawItem1, string rawItem2)
    {
        string craftedItem = null;
        // loop through recipe dictionary and get final item
        foreach (KeyValuePair<string, (string, string)> recipe in recipeDict)
        {
            if (recipe.Value == (rawItem1, rawItem2) || recipe.Value == (rawItem2, rawItem1))
            {
                craftedItem = recipe.Key;
            }
        }
        // check if there is enough raw materials and if crafted item is valid
        if (inventoryDict[rawItem1] > 0 && inventoryDict[rawItem2] > 0 && craftedItem != null)
        {
            inventoryDict[rawItem1]--;
            inventoryDict[rawItem2]--;
            inventoryDict[craftedItem]++;
        }
    }

    public void Place(string craftedItem)
    {
        if (inventoryDict[craftedItem] > 0)
        {
            GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            // set the tag
            item.tag = "Item";
            // set the name
            item.name = craftedItem;
            // TODO: set the sprite
            inventoryDict[craftedItem]--;
        }
    }

    // manages the crafting and placing hotkeys for the game
    void HotkeyManager()
    {
        // crafting hotkeys
        if (Input.GetKeyDown(KeyCode.T)) { Craft("Bomb"); }
        else if (Input.GetKeyDown(KeyCode.Y)) { Craft("BlueLight"); }
        else if (Input.GetKeyDown(KeyCode.U)) { Craft("RedLight"); }
        else if (Input.GetKeyDown(KeyCode.I)) { Craft("OrangeLight"); }
        else if (Input.GetKeyDown(KeyCode.O)) { Craft("PurpleLight"); }
        else if (Input.GetKeyDown(KeyCode.P)) { Craft("YellowLight"); }
        else if (Input.GetKeyDown(KeyCode.LeftBracket)) { Craft("CalciumHydroxide"); }
        // placing hotkeys
        else if (Input.GetKeyDown(KeyCode.F)) { Place("Bomb"); }
        else if (Input.GetKeyDown(KeyCode.G)) { Place("BlueLight"); }
        else if (Input.GetKeyDown(KeyCode.H)) { Place("RedLight"); }
        else if (Input.GetKeyDown(KeyCode.J)) { Place("OrangeLight"); }
        else if (Input.GetKeyDown(KeyCode.K)) { Place("PurpleLight"); }
        else if (Input.GetKeyDown(KeyCode.L)) { Place("YellowLight"); }
        else if (Input.GetKeyDown(KeyCode.Semicolon)) { Place("CalciumHydroxide"); }
    }

    // Update is called once per frame
    void Update()
    {
        PickObject();
        ToggleInventory();
        HotkeyManager();
    }
}
