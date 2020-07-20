using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour , IPointerDownHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    RectTransform rectTransform;
    [SerializeField]
    private Canvas canvas;
    Vector3 mOffset;
    
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
        // gameObject.transform.localPosition += Input.mousePosition;
        // this.transform.position = pointer.position;
        // Debug.Log(canvas.scaleFactor);
        // Debug.Log(pointer.delta/canvas.scaleFactor);
        // transform.position = Camera.main.(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        // Debug.Log(Input.mousePosition);
        rectTransform.anchoredPosition += pointer.delta / 0.0111f;
        // Debug.Log(rectTransform.anchoredPosition.ToString());
    }

    public void OnBeginDrag(PointerEventData pointer){
        Debug.Log("On Begin");
    }

    public void OnEndDrag(PointerEventData pointer){
        transform.localPosition = Vector3.zero;
        // Debug.Log("Helo");
    }

    // void OnMouseDrag(){
    //     transform.position = GetMousePosition() + mOffset;
    
    // }

    // void OnMouseDown()
    // {
    //     mOffset = gameObject.transform.position - GetMousePosition();
    // }

    // private void Update() {
    //     // updatePosition();
    // }
    
    // private Vector3 GetMousePosition(){
    //     return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    // }




}
