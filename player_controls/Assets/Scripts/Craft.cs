using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Craft : MonoBehaviour
{
    public Button craftButton;

    public InventoryManager inventoryManager;

    public Text validCraft;

    [SerializeField]
    private CraftItemValues startVals;

    InventoryObject inventoryObject;

    private List<GameObject> inventoryList = new List<GameObject>();
    Color originalColor;
    void Start(){
        craftButton.onClick.AddListener(craft);
        inventoryList = inventoryManager.getInventory;
        originalColor = validCraft.color;
    }
    // private Item itemList;
    //Need to find the count of the items
    public void craft()
    {
        var itemList = DropHandler.ItemList;
        

        if(itemList.Count == 2)
        {
            
            var itemIndex1 = System.Convert.ToInt32((itemList[0].name.Substring(itemList[0].name.Length - 1)));
            var itemIndex2 = System.Convert.ToInt32((itemList[1].name.Substring(itemList[1].name.Length - 1)));
            
            string item1 = inventoryList[itemIndex1 - 1].name;
            string item2 = inventoryList[itemIndex2 - 1].name;

            GameObject.FindGameObjectWithTag("Player").GetComponent<CollectItem>().Craft(item1, item2);

            //Get the item gameobject, reset the components. Tuples are indexed via Item1, Item2...
            //Clear the dictionary afterwards

            var val1 = startVals.Get(item1);
            var val2 = startVals.Get(item2);
            
            var rectTransform1 = val1.Item1;
            var rectTransform2 = val2.Item1;

            var startPos1 = val1.Item2;
            var startPos2 = val2.Item2;

            rectTransform1.anchoredPosition = startPos1;
            rectTransform2.anchoredPosition = startPos2;

        }
        else
        {
            StartCoroutine(FadeOutRoutine());
            
        }

    }
    private IEnumerator FadeOutRoutine()
         { 
            validCraft.text = "INVALID!!";
             
             for (float t = 0.01f; t < 1.5f; t += Time.deltaTime)
             {
                 validCraft.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t/2f));
                 yield return null;
             }
            validCraft.text = "";
            validCraft.color = originalColor;
         }
    
}
