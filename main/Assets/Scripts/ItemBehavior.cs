using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    // all sprites 
    public Sprite bombSprite;
    public Sprite blueLightSprite;
    public Sprite redLightSprite;
    public Sprite orangeLightSprite;
    public Sprite purpleLightSprite;
    public Sprite yellowLightSprite;
    public Sprite calciumHydroxideSprite;

    // Start is called before the first frame update
    void Start()
    {
        string name = gameObject.name;
        if (name == "Bomb") { spriteRenderer.sprite = bombSprite; }
        else if (name == "BlueLight") { spriteRenderer.sprite = blueLightSprite; }
        else if (name == "RedLight") { spriteRenderer.sprite = redLightSprite; }
        else if (name == "OrangeLight") { spriteRenderer.sprite = orangeLightSprite; }
        else if (name == "PurpleLight") { spriteRenderer.sprite = purpleLightSprite; }
        else if (name == "YellowLight") { spriteRenderer.sprite = yellowLightSprite; }
        else if (name == "CalciumHydroxide") { spriteRenderer.sprite = calciumHydroxideSprite; }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
