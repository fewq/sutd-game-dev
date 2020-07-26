using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour ,IPointerDownHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    private RectTransform rectTransform;
    [SerializeField]
    private Canvas canvas;
    Vector3 mOffset;

    Vector2 startPosition;

    string itemName;

    private CanvasGroup canvasGroup;

    private bool dropStatus;
    [SerializeField]
    private Item item;

    private inventoryItems InventoryList;

    
    
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

    public void OnBeginDrag(PointerEventData pointer){
        transform.SetAsLastSibling();
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
        DropHandler.dropStatus = false;
        item.ItemName = itemName;
    }

    public void OnEndDrag(PointerEventData pointer){
        
        if(!DropHandler.dropStatus)
        {
            rectTransform.anchoredPosition = startPosition;
            print("End position" + startPosition.ToString());
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }


    private void Update() {
        // updatePosition();

    }
    



}
