using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour , IPointerDownHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    RectTransform rectTransform;
    [SerializeField]
    private Canvas canvas;
    Vector2 localpoint;
    
    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        Debug.Log("Awake");
        
    }

    // public void updatePosition(){
        
    //     RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localpoint);
    //     rectTransform.anchoredPosition = localpoint;
    // }
    public void OnPointerDown(PointerEventData pointer){
        Debug.Log("Pointer down");
    }
    public void OnDrag(PointerEventData pointer){
        // rectTransform.localPosition += Input.mousePosition;
        rectTransform.anchoredPosition += pointer.delta/canvas.scaleFactor;
        Debug.Log(rectTransform.anchoredPosition.ToString());
    }

    public void OnBeginDrag(PointerEventData pointer){
        Debug.Log("On Begin");
    }

    public void OnEndDrag(PointerEventData pointer){
        transform.localPosition = Vector3.zero;
        // Debug.Log("Helo");
    }

    private void Update() {
        // updatePosition();
    }


}
