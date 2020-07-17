using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MonsterType
{
    public float speed{get; private set;}

    public Monsters type{get; private set;}

    public enum Monsters {
        GREENMONSTER = 0,
        YELLOWMONSTER = 1,
        REDMONSTER = 3
    }


    public MonsterType(Monsters type, float speed){
        this.type = type;
        this.speed = speed;
    }
}
