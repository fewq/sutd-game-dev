using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName="CraftItemValues", menuName="CraftItemVal", order=2)]
public class CraftItemValues : ScriptableObject
{  
    //item image must be disabled at the end

    private Dictionary<string, (Image, Vector2)> startVals = new Dictionary<string, (Image, Vector2)>();


    public void Set(string key, Image image, Vector2 vector2)
{
    if (startVals.ContainsKey(key))
    {
        startVals[key] = (image, vector2);
    }
    else
    {
        startVals.Add(key, (image, vector2));
    }
}

public (Image, Vector2) Get(string key)
{

    if (startVals.ContainsKey(key))
    {
        return startVals[key];
    }
    else
    {
        return (null, new Vector2 (0,0));
    }

}

}
