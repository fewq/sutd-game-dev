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

    // text for raw items
    public Text inventoryItemsText;

    public bool itemAdded = false;

    public InventoryObject inventoryObject;

    private InventoryManager invMngr;

    private GameObject InventoryBarCanvas;
    private GameObject sidebarCanvas;

    public GameObject itemPrefab;

    private int defaultNumber = 0 ;
    // Start is called before the first frame update
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
        inventoryItemsText = GameObject.Find("InventoryItemsText").GetComponent<Text>();
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
        if (pickupItem == null)
        {
            pickupItem = other.gameObject;
        }

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
            // reason for pressing Z to pick up: in the future, we would need to place items, and the player may accidentally pick it up if they walked arount it
            if (Input.GetKey(KeyCode.Z))
            {
                Debug.Log("picked up " + pickupItem.name);
                GameManager.Instance.PlaySFX("collectitem");
                // add to PickupList
                PickupList.Add(pickupItem);
                Debug.Log(pickupItem);
                // also add to dictionary for now
                inventoryDict[pickupItem.name] += 1;
                Debug.Log("inventoryDict entry -> " + pickupItem.name + ": " + inventoryDict[pickupItem.name]);
                // deactivate item
                pickupItem.SetActive(false);
                // destroy item instead
                // Destroy(pickupItem);
                pickupItem = null;
                itemAdded = true;
                inventoryObject.PickupList = PickupList;
            }
            // moved above
            // //update the scriptable object with the lists.
            // inventoryObject.Inventory = inventoryDict;
            // inventoryObject.PickupList = PickupList;
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
            sidebarCanvas.GetComponent<Canvas>().enabled = true;
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

            inventoryItemsText.text = $@"{inventoryDict["Fire"]}              {inventoryDict["Water"]}             {inventoryDict["CupricChloride"]}

{inventoryDict["LithiumChloride"]}              {inventoryDict["CalciumChloride"]}             {inventoryDict["PotassiumChloride"]}

{inventoryDict["SodiumChloride"]}              {inventoryDict["Caesium"]}             {inventoryDict["CalciumOxide"]}";

        }
        else
        {
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
        }
        else
        {
            throw new CustomException("Invalid Crafting Combination");
        }
    }

    public void Place(string craftedItem)
    {
        if (inventoryDict[craftedItem] > 0)
        {
            GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            if (craftedItem == "Bomb")
            {
                GameManager.Instance.SetBomb();
            }else if(craftedItem == "CalciumHydroxide")
            {
                GameManager.Instance.SetCAOH2();
            }
            inventoryDict[craftedItem]--;
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
        if (Input.GetKeyDown(KeyCode.B)) { Place("Bomb"); }
        else if (Input.GetKeyDown(KeyCode.G)) { Place("BlueLight"); }
        else if (Input.GetKeyDown(KeyCode.H)) { Place("RedLight"); }
        else if (Input.GetKeyDown(KeyCode.J)) { Place("OrangeLight"); }
        else if (Input.GetKeyDown(KeyCode.K)) { Place("PurpleLight"); }
        else if (Input.GetKeyDown(KeyCode.L)) { Place("YellowLight"); }
        else if (Input.GetKeyDown(KeyCode.C)) { Place("CalciumHydroxide"); }
    }

    // Update is called once per frame
    void Update()
    {
        PickObject();
        ToggleInventory();
        HotkeyManager();
    }
}
