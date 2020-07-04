using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MonsterType
{
    public float speed{get; private set;}

    public items type{get; private set;}

    public enum items {
        GREENMONSTER = 0,
    }


    public MonsterType(items type, float speed){
        this.type = type;
        this.speed = speed;
    }
}
