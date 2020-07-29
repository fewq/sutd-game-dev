using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragHandler : MonoBehaviour ,IPointerDownHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    private RectTransform rectTransform;
    [SerializeField]
    private Canvas canvas;

    Vector2 startPosition;
    
    private Image startImage;

    string itemName;

    private CanvasGroup canvasGroup;

    [SerializeField]
    private InventoryObject  InventoryList;

    private CraftItemValues craftItemVals;

    private Dictionary<string, (Image, Vector2)> initVals;

    

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        itemName = transform.name;
        startPosition = rectTransform.anchoredPosition;
    }


    public void OnPointerDown(PointerEventData pointer){
        // Debug.Log("Pointer down");
        // Debug.Log("Position" + pointer.position);
    }
    public void OnDrag(PointerEventData pointer){
        // canvasGroup.alpha = 0.1f;
        rectTransform.anchoredPosition += pointer.delta / canvas.scaleFactor;
        DropHandler.dropStatus = false;
    }

    //we have to check if the number of items is more than 1 and see if to keep the placeholder.
    public void OnBeginDrag(PointerEventData pointer){
        transform.SetAsLastSibling();
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
        DropHandler.dropStatus = false;
        InventoryList.ItemName = itemName;
        startImage = pointer.selectedObject.transform.GetComponent<Image>();
    }

    public void OnEndDrag(PointerEventData pointer){
        
        if(!DropHandler.dropStatus)
        {
            rectTransform.anchoredPosition = startPosition;
            print("End position" + startPosition.ToString());
        }
        else
        {
            initVals.Add(itemName, (startImage, startPosition));
            craftItemVals.Set(itemName, startImage, startPosition);
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }


    private void Update() {
        // updatePosition();

    }
    



}
