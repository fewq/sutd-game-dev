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

    GameObject item;

    private CanvasGroup canvasGroup;

    [SerializeField]
    private InventoryObject  InventoryList;
    
    [SerializeField]
    private CraftItemValues craftItemVals;

    private Dictionary<string, (RectTransform, Vector2)> initVals = new Dictionary<string, (RectTransform, Vector2)>();

    

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        item = this.gameObject;
        print(item.name);
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
        InventoryList.InventoryItem = item;
    }

    public void OnEndDrag(PointerEventData pointer){
        
        if(!DropHandler.dropStatus)
        {
            rectTransform.anchoredPosition = startPosition;
            print("End position" + startPosition.ToString());
        }
        else
        {
            initVals.Add(item.name, (rectTransform, startPosition));
            craftItemVals.Set(item.name, rectTransform, startPosition);
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }


    private void Update() {
        // updatePosition();

    }
    



}
