using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// using System;

public class Craft : MonoBehaviour
{
    public Button craftButton;

    public Text validCraft;

    [SerializeField]
    private CraftItemValues startVals;

    private List<string> flavour = new List<string>() { "What are you doing?", "Try Harder", "Noope", "Don't Give Up!", "????", "This doesn't seem quite right...", "Yikes"};

    public List<GameObject> inventoryList = new List<GameObject>();

    Color originalColor;
    void Start()
    {
        craftButton.onClick.AddListener(craft);
        // inventoryList = inventoryManager.getInventory;
        originalColor = validCraft.color;
    }
    // private Item itemList;
    //Need to find the count of the items
    public void craft()
    {
        var itemList = DropHandler.ItemList;
        inventoryList = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>().getInventory;

        if (itemList.Count == 2)
        {

            var itemIndex1 = System.Convert.ToInt32((itemList[0].name.Substring(itemList[0].name.Length - 1)));
            var itemIndex2 = System.Convert.ToInt32((itemList[1].name.Substring(itemList[1].name.Length - 1)));

            print(inventoryList.Count);
            try
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<CollectItem>().Craft(itemList[0].tag, itemList[1].tag);
            }

            catch (CustomException)
            {
                StartCoroutine(FadeOutRoutine());
            }

            //Get the item gameobject, reset the components. Tuples are indexed via Item1, Item2...
            //Clear the dictionary afterwards

            var val1 = startVals.Get(itemList[0].name);
            var val2 = startVals.Get(itemList[1].name);

            var rectTransform1 = val1.Item1;
            var rectTransform2 = val2.Item1;

            var startPos1 = val1.Item2;
            var startPos2 = val2.Item2;

            rectTransform1.anchoredPosition = startPos1;
            rectTransform2.anchoredPosition = startPos2;

            itemList.Clear();

            //So that the highlight color works when hover over
            craftButton.enabled = false;
            craftButton.enabled = true;

        }
        else
        {
            StartCoroutine(FadeOutRoutine());

            craftButton.enabled = false;
            craftButton.enabled = true;

        }

    }
    private IEnumerator FadeOutRoutine()
    {

        validCraft.text = flavour[UnityEngine.Random.Range(0, 4)];

        for (float t = 0.01f; t < 1.5f; t += Time.deltaTime)
        {
            validCraft.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / 2f));
            yield return null;
        }
        validCraft.text = "";
        validCraft.color = originalColor;
    }

}
