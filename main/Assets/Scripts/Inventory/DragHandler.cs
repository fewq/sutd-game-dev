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
    }

    //we have to check if the number of items is more than 1 and see if to keep the placeholder.
    public void OnBeginDrag(PointerEventData pointer){
        transform.SetAsLastSibling();
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
        InventoryList.InventoryItem = item;
    }

    public void OnEndDrag(PointerEventData pointer){
        
        rectTransform.anchoredPosition = startPosition;
        if(ClickManager.ItemList.Contains(item)){
            string slot = ClickManager.ItemSelected[item];
            ClickManager.ItemList.Remove(item); //Removes item from crafting list if we undrag it
            craftItemVals.Remove(item.name); //remove from crafting items dictionary
            if(slot == "item1"){
                GameObject.Find("OnClickManager").GetComponent<ClickManager>().ItemBoolSet(1); //release the item lock\
                print("item1 released");
                ClickManager.ItemSelected.Remove(item);
            }
            if(slot == "item2"){
                GameObject.Find("OnClickManager").GetComponent<ClickManager>().ItemBoolSet(2); //release the item lock
                print("item2 released");
                ClickManager.ItemSelected.Remove(item);
            }
            
            
        }
        
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData pointer){
        if (pointer.button == PointerEventData.InputButton.Right){
            if(ClickManager.ItemList.Count == 2 || ClickManager.ItemList.Contains(item)){ //don't be sneaky
                return;
            }
            GameObject.Find("OnClickManager").GetComponent<ClickManager>().changeItemPos(item);
            craftItemVals.Set(item.name, rectTransform, startPosition); //set vals for returning image to start position aft craft

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
