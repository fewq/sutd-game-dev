using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster : MonsterType
{   
    public int amount{get; private set;}

    public GameObject[] monsters;

    private int index = 0;

    public Monster(items type, int amount, float speed) : base(type, speed)
    {
        this.amount = amount;
        monsters = new GameObject[amount];

    }
    
    public void add(GameObject monster){
        if (index < amount){
           //set its index
        //    monster.GetComponent<BasicObjectScript>().index = index;
        //    monster.GetComponent<BasicObjectScript>().type = type;
           monsters[index] = monster;
           index ++;

        }
    }
}
