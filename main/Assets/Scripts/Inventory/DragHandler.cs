using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragHandler : MonoBehaviour , IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerClickHandler
{

    private RectTransform rectTransform;
    [SerializeField]
    private Canvas canvas;

    Vector2 startPosition;
    
    private Image startImage;

    GameObject item;

    private CanvasGroup canvasGroup;

    [SerializeField]
    private InventoryObject  InventoryList;

    [SerializeField]
    private CraftItemValues craftItemVals;


    

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        item = this.gameObject;
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData pointer){
        rectTransform.anchoredPosition += pointer.delta / canvas.scaleFactor;
        DropHandler.dropStatus = false;
    }

    //we have to check if the number of items is more than 1 and see if to keep the placeholder.
    public void OnBeginDrag(PointerEventData pointer){
        transform.SetAsLastSibling();
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
        DropHandler.dropStatus = false;
        InventoryList.InventoryItem = item;
        if(DropHandler.ItemList.Contains(item)){
            DropHandler.ItemList.Remove(item);
        }
    }

    public void OnEndDrag(PointerEventData pointer){
        
        if(!DropHandler.dropStatus)
        {
            rectTransform.anchoredPosition = startPosition;
            List<GameObject> itemSelected = GameObject.Find("OnClickManager").GetComponent<ClickManager>().itemSelected;
            if(itemSelected.Contains(item)){
                itemSelected.Remove(item);
            }
        }
        else
        {
            craftItemVals.Set(item.name, rectTransform, startPosition);
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData pointer){
        if (pointer.button == PointerEventData.InputButton.Right){
            if(ClickManager.ItemList.Count == 2){
                return;
            }
            GameObject.Find("OnClickManager").GetComponent<ClickManager>().changeItemPos(item);
            craftItemVals.Set(item.name, rectTransform, startPosition);

        }
    }

    
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // clickCraft();
    }
}
