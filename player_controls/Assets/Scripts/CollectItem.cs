using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    // variable to hold GameObject to pick up
    private GameObject pickupItem;

    private BoxCollider2D playerCollider;

    // List to store the GameObejct that have been picked up
    public List<GameObject> ItemList = new List<GameObject>();

    // dictionary to store the GameObjects
    public Dictionary<string, int> itemDict;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
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
    // Update is called once per frame
    void Update()
    {
        PickObject();
    }
}
