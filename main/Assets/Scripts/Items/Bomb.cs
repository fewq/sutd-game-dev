using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float countdown = 2f;
    Color startColor;
    Color toChangeColor;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = GameManager.Instance.gridScale;
        startColor = this.GetComponent<SpriteRenderer>().color;
        toChangeColor = this.GetComponent<SpriteRenderer>().color;

    }
    
    IEnumerator flashingBomb(){
        for(float t = 0.01f; t< 2.0f; t+= Time.deltaTime){
            this.GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, Color.red, Mathf.Min(1,t/2f));
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
            countdown -= Time.deltaTime;
            if(GameObject.Find("Player").GetComponent<CollectItem>().inventoryState){
                countdown = 2f;
                return;
            }
            StartCoroutine(flashingBomb());
            if (countdown <= 0f)
            {
                Debug.Log("EKUSUPLOSION!");
                //Change GameManager to singleton
                //FindObjectOfType<GameManager>().Explode(transform.position);
                GameManager.Instance.Explode(transform.position);
                
                Destroy(gameObject);
            }
    }
        
}
