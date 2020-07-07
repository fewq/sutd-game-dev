using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private GameObject item;

    private BoxCollider2D playerCollider ;

    public List<GameObject> ItemList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D other) {
        
        if(item == null){
            item = other.gameObject;
        }
        else{
            item = null;
            item = other.gameObject;
        }
    }

    void PickObject(){
        if(item != null){
            
            if(Input.GetKey(KeyCode.E)){
                Debug.Log(item);
                ItemList.Add(item);
                item.SetActive(false);
                item = null;
            }
            
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        PickObject();
    }
}
