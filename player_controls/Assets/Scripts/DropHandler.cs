using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData pointerevent){
        print("Image " + GetComponent<RectTransform>().anchoredPosition.ToString());
        print("Drop pointer "+pointerevent.pointerDrag.GetComponent<RectTransform>().anchoredPosition.ToString());
        if(pointerevent.pointerDrag != null){
            pointerevent.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponentInParent<RectTransform>().anchoredPosition;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
}
