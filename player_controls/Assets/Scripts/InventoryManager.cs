using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private List<Transform> itemPlaceholders = new List<Transform>();
    private List<Image> inventoryImages = new List<Image>();
    public List<GameObject> itemList = new List<GameObject>();

    private List<GameObject> inventoryItems = new List<GameObject>();
    //Should have an event that tells when an item has been added and then update the inventory.
    private GameObject player;

    private bool itemAdded;
    Transform inventory;
    // Start is called before the first frame update
    void Awake()
    {
        for(int i=1; i<9; i++){
            itemPlaceholders.Add(GameObject.FindGameObjectWithTag("Item"+i.ToString()).transform);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        itemList = player.GetComponent<CollectItem>().GetItems();

        foreach(Transform slot in itemPlaceholders){
            Image itemImage = slot.GetChild(0).GetComponent<Image>();
            // Image itemImage = slot.
            // Image itemImage = 
            if(itemImage != null){
                itemImage.enabled = false;
            }
            inventoryImages.Add(itemImage);


        }

    }
    
    void SetInventory(){
        for(int j=0; j<inventoryImages.Count; j++){
            if(!inventoryImages[j].IsActive()){
                inventoryImages[j].enabled = true;
                inventoryImages[j].transform.localScale *= 3f;
                var toAdd = itemList[itemList.Count - 1];
                inventoryImages[j].sprite = toAdd.transform.GetComponent<SpriteRenderer>().sprite;
                inventoryItems.Add(toAdd.gameObject);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        itemList = player.GetComponent<CollectItem>().GetItems();
        itemAdded = player.GetComponent<CollectItem>().itemAdded;
        if(itemAdded){
            SetInventory();
        }
        player.GetComponent<CollectItem>().itemAdded = false;
    }
}
