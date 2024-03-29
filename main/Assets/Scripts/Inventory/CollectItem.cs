﻿using System.Collections;
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

    // text for raw items
    public Text inventoryItemsText;

    public bool itemAdded = false;

    public InventoryObject inventoryObject;

    private InventoryManager invMngr;

    private GameObject InventoryBarCanvas;
    private GameObject sidebarCanvas;

    public GameObject itemPrefab;

    public CustomInputManager customInputManager;

    public Text naughtyText;

    private int defaultNumber = 0 ;
    // Start is called before the first frame update

    private List<string> rawItems = new List<string>() {"Caesium", "Fire", "Water", "CupricChloride", "LithiumChloride", "CalciumChloride", "SodiumChloride", "CalciumOxide", "PotassiumChloride"};
    void Start()
    {
        playerCollider = gameObject.GetComponent<BoxCollider2D>();

        // create dicitonary to store items with sample numbers
        inventoryDict = new Dictionary<string, int>(){
            {"Fire", defaultNumber},
            {"Water", defaultNumber},
            {"CupricChloride", defaultNumber},
            {"LithiumChloride", defaultNumber},
            {"CalciumChloride", defaultNumber},
            {"PotassiumChloride", defaultNumber},
            {"SodiumChloride", defaultNumber},
            {"Caesium", defaultNumber},
            {"CalciumOxide", defaultNumber},
            {"Bomb", defaultNumber},
            {"BlueLight", defaultNumber},
            {"RedLight", defaultNumber},
            {"OrangeLight", defaultNumber},
            {"PurpleLight", defaultNumber},
            {"YellowLight", defaultNumber},
            {"CalciumHydroxide", defaultNumber}
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
        // get inventoryItemText Reference
        // inventoryItemsText = GameObject.Find("InventoryItemsText").GetComponent<Text>();
        // get inventoryCanvas reference
        inventoryCanvas = GameObject.Find("Inventory").GetComponent<Canvas>();

        invMngr = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

        InventoryBarCanvas = GameObject.Find("InventoryBar");
        sidebarCanvas = GameObject.Find("SideBar");

        inventoryObject.PickupList = PickupList;

        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // might need to check that this has an "item" tag in the future with "other.tag" since there will be acid rivers
        if (other.CompareTag("Item"))
        {
            pickupItem = other.gameObject;
        }
        else
        {
            //Debug.Log(other);
        }

    }

    // this method removes the pickupItem as a target once the player steps out of the collider
    private void OnTriggerExit2D(Collider2D other)
    {
        pickupItem = null;
    }

    void PickObject()
    {
        if (pickupItem != null && rawItems.Contains(pickupItem.name))
        {
            // reason for pressing Z to pick up: in the future, we would need to place items, and the player may accidentally pick it up if they walked arount it
            Debug.Log("picked up " + pickupItem.name);
            
            // add to PickupList
            PickupList.Add(pickupItem);
            Debug.Log(pickupItem);
            // also add to dictionary for now
            inventoryDict[pickupItem.name] += 1;
            Debug.Log("inventoryDict entry -> " + pickupItem.name + ": " + inventoryDict[pickupItem.name]);
            // deactivate item
            pickupItem.SetActive(false);
            GameManager.Instance.PlaySFX("collectitem");
            // destroy item instead
            // Destroy(pickupItem);
            pickupItem = null;
            
            inventoryObject.PickupList = PickupList;

            int count = 1;
            foreach(string item in inventoryDict.Keys){
                if(item == "Bomb"){
                    break;
                }
                // print(GameObject.FindGameObjectWithTag("test").GetComponent<Text>());
                Text itemText = GameObject.FindGameObjectWithTag("text"+count.ToString()).GetComponent<Text>();
                count += 1;
                itemText.text = inventoryDict[item].ToString();
            }
            
            itemAdded = true;
            // }
            // moved above
            // //update the scriptable object with the lists.
            // inventoryObject.Inventory = inventoryDict;
            // inventoryObject.PickupList = PickupList;
        }
    }

    void ToggleInventory()
    {
        if (customInputManager.GetKeyDown("Inventory"))
        {
            inventoryState = !inventoryState;
            print("InventoryState: " + inventoryState);
            GameManager.Instance.PlaySFX("toggleinv");
        }
        if (inventoryState)
        {
            inventoryCanvas.GetComponent<Canvas>().enabled = true;
            sidebarCanvas.GetComponent<Canvas>().enabled = true;
//             inventoryText.text = $@"Inventory: 
// Fire: {inventoryDict["Fire"]}
// Water: {inventoryDict["Water"]}
// CupricChloride: {inventoryDict["CupricChloride"]} 
// LithiumCloride: {inventoryDict["LithiumChloride"]}
// CalciumChloride: {inventoryDict["CalciumChloride"]}
// PotassiumChloride: {inventoryDict["PotassiumChloride"]}
// SodiumCloride: {inventoryDict["SodiumChloride"]}
// Caesium: {inventoryDict["Caesium"]}
// CalciumOxide: {inventoryDict["CalciumOxide"]}
// Bomb: {inventoryDict["Bomb"]} (Craft: T; Place: F)
// BlueLight: {inventoryDict["BlueLight"]} (Craft: Y; Place: G)
// RedLight: {inventoryDict["RedLight"]} (Craft: U; Place: H)
// OrangeLight: {inventoryDict["OrangeLight"]} (Craft: I; Place: J)
// PurpleLight: {inventoryDict["PurpleLight"]} (Craft: O; Place: K)
// YellowLight: {inventoryDict["YellowLight"]} (Craft: P; Place: L)
// CalciumHydroxide: {inventoryDict["CalciumHydroxide"]} (Craft: [; Place: ;)";
        }
        else
        {
            naughtyText.text = "";
            inventoryCanvas.GetComponent<Canvas>().enabled = false;
            sidebarCanvas.GetComponent<Canvas>().enabled = false;
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
            // craftedItems.Add(GameObject.Find(craftedItem));
            GameObject.FindGameObjectWithTag(craftedItem).GetComponent<Text>().text = inventoryDict[craftedItem].ToString();
            if (inventoryDict[rawItem1] == 0)
            {
                GameObject.FindGameObjectWithTag(rawItem1).GetComponent<Image>().enabled = false;
            }
            if (inventoryDict[rawItem2] == 0)
            {
                GameObject.FindGameObjectWithTag(rawItem2).GetComponent<Image>().enabled = false;
            }


            foreach (GameObject item in PickupList)
            {
                if (item.name == rawItem1)
                {
                    PickupList.Remove(item);
                    break;
                }
            }
            foreach (GameObject item in PickupList)
            {
                if (item.name == rawItem2)
                {
                    PickupList.Remove(item);
                    break;
                }
            }

            int count = 1;
            foreach(string item in inventoryDict.Keys){
                if(item == "Bomb"){
                    break;
                }
                // print(GameObject.FindGameObjectWithTag("test").GetComponent<Text>());
                Text itemText = GameObject.FindGameObjectWithTag("text"+count.ToString()).GetComponent<Text>();
                count += 1;
                itemText.text = inventoryDict[item].ToString();
            }
            GameManager.Instance.PlaySFX("successcraft");
        }
        else
        {
            GameManager.Instance.PlaySFX("failcraft");
            throw new CustomException("Invalid Crafting Combination");
        }
    }

    public void Place(string craftedItem)
    {
        if (inventoryDict[craftedItem] > 0 && !inventoryState)
        {
            GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            if (craftedItem == "Bomb")
            {
                GameManager.Instance.SetBomb();
            }else if(craftedItem == "CalciumHydroxide")
            {
                GameManager.Instance.SetCAOH2();
            }
            else if (craftedItem == "BlueLight")
            {
                GameManager.Instance.SetTorch("Blue");
            }else if(craftedItem == "OrangeLight")
            {
                GameManager.Instance.SetTorch("Orange");
            }
            else if (craftedItem == "PurpleLight")
            {
                GameManager.Instance.SetTorch("Purple");
            }
            else if (craftedItem == "RedLight")
            {
                GameManager.Instance.SetTorch("Red");
            }
            else if (craftedItem == "YellowLight")
            {
                GameManager.Instance.SetTorch("Yellow");
            }
            inventoryDict[craftedItem]--;
            GameObject.FindGameObjectWithTag(craftedItem).GetComponent<Text>().text = inventoryDict[craftedItem].ToString();
        }
        else if(inventoryState){
            naughtyText.text = "CLOSE THE INVENTORY >:(";
        }
    }

    // manages the crafting and placing hotkeys for the game
    void HotkeyManager()
    {
        // crafting hotkeys
        //if (Input.GetKeyDown(KeyCode.T)) { Craft("Bomb"); }
        //else if (Input.GetKeyDown(KeyCode.Y)) { Craft("BlueLight"); }
        //else if (Input.GetKeyDown(KeyCode.U)) { Craft("RedLight"); }
        //else if (Input.GetKeyDown(KeyCode.I)) { Craft("OrangeLight"); }
        //else if (Input.GetKeyDown(KeyCode.O)) { Craft("PurpleLight"); }
        //else if (Input.GetKeyDown(KeyCode.P)) { Craft("YellowLight"); }
        //else if (Input.GetKeyDown(KeyCode.LeftBracket)) { Craft("CalciumHydroxide"); }
        // placing hotkeys
        if (customInputManager.GetKeyDown("DropBomb")) { Place("Bomb"); }
        else if (customInputManager.GetKeyDown("DropBlueLight")) { Place("BlueLight"); }
        else if (customInputManager.GetKeyDown("DropRedLight")) { Place("RedLight"); }
        else if (customInputManager.GetKeyDown("DropOrangeLight")) { Place("OrangeLight"); }
        else if (customInputManager.GetKeyDown("DropPurpleLight")) { Place("PurpleLight"); }
        else if (customInputManager.GetKeyDown("DropYellowLight")) { Place("YellowLight"); }
        else if (customInputManager.GetKeyDown("DropCalciumHydroxide")) { Place("CalciumHydroxide"); }
    }

    // Update is called once per frame
    void Update()
    {
        PickObject();
        ToggleInventory();
        HotkeyManager();
    }
}
