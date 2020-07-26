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

    private List<GameObject> inventoryList = new List<GameObject>();
    Color originalColor;
    void Start(){
        craftButton.onClick.AddListener(craft);
        inventoryList = inventoryManager.getInventory;
        originalColor = validCraft.color;
    }
    // private Item itemList;
    public void craft()
    {
        var itemList = DropHandler.ItemList;
        

        if(itemList.Count == 2){
            
            var itemIndex1 = System.Convert.ToInt32((itemList[0].Substring(itemList[0].Length - 1)));
            var itemIndex2 = System.Convert.ToInt32((itemList[1].Substring(itemList[1].Length - 1)));
            
            string item1 = inventoryList[itemIndex1 - 1].name;
            string item2 = inventoryList[itemIndex2 - 1].name;
            GameObject.FindGameObjectWithTag("Player").GetComponent<CollectItem>().Craft(item1, item2);
        }
        else{
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
