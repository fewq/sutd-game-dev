using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private Monster goblinMonster;
    public GameObject GoblinPrefab;

    public GameObject Background;
    private float rangeX, rangeY;

    // Start is called before the first frame update
    void Start()
    {
        goblinMonster = new Monster(MonsterType.items.GREENMONSTER, Random.Range(1, 10), 100);

        // rangeX = Background.GetComponent<Grid>().sprite.bounds.extents.x*0.8f;
        // rangeY = Background.GetComponent<Grid>().sprite.bounds.extents.y*0.8f;
        
        rangeX = 1 * 0.8f;
        rangeY = 2 * 0.8f;

        instantiate();

    }

    private void instantiate(){
    //instantiate
    for (int i = 0; i< goblinMonster.amount; i++){
        goblinMonster.add(Instantiate(GoblinPrefab, new Vector2(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY)), Quaternion.identity));
    }

   }


}
