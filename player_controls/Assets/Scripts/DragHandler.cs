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

    private CanvasGroup canvasGroup;
    
    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void OnPointerDown(PointerEventData pointer){
        // Debug.Log("Pointer down");
        // Debug.Log("Position" + pointer.position);
        print(rectTransform.name);
    }
    public void OnDrag(PointerEventData pointer){
        // canvasGroup.alpha = 0.1f;
        rectTransform.anchoredPosition += pointer.delta / canvas.scaleFactor;
        print(rectTransform.anchoredPosition);
    }

    public void OnBeginDrag(PointerEventData pointer){
        startPosition = pointer.pressPosition;
        canvasGroup.alpha = 0.5f;
        Debug.Log(canvasGroup.alpha);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData pointer){
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }


    private void Update() {
        // updatePosition();
    }
    



}
