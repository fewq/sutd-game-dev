using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{

    RectTransform rectTransform;
    [SerializeField]
    private Canvas canvas;
    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData pointer){
        rectTransform.anchoredPosition += pointer.delta/canvas.scaleFactor;
    }

    // public void OnBeginDrag(PointerEventData pointer){
        
    // }

    public void OnEndDrag(PointerEventData pointer){
        // transform.localPosition = Vector3.zero;
        Debug.Log("Helo");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
