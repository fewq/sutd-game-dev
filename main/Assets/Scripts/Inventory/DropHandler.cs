using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{

    private static List<GameObject> itemList = new List<GameObject>();

    public static bool dropStatus = false;

    public InventoryObject item;

    public void OnDrop(PointerEventData pointerevent){
        if(pointerevent.pointerDrag != null){
            pointerevent.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            dropStatus = true;
            itemList.Add(item.InventoryItem);
        }
    }

    public static List<GameObject> ItemList{
        get{
            return itemList;
        }
    }

}
