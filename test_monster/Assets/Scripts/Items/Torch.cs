using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public enum favColors
    {
        Blue = 0,
        Yellow = 1,
        Red = 3,
        Orange = 4
    }

    public float flameDuration = 7f;
    public bool lightFlameTest = false;
    private bool flameLit = false;
    private SpriteRenderer flameRenderer;
    private AnimateController flameAniController;
    private List<MonsterController> attractedMonsters;
    private List<string> monsterNames;
    private Animator flameAnimator;

    // Start is called before the first frame update
    void Start()
    {
        monsterNames = new List<string>();
        attractedMonsters = new List<MonsterController>();
        flameRenderer = GetComponent<SpriteRenderer>();
        flameAniController = GetComponent<AnimateController>();
        flameAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lightFlameTest && !flameLit) LightTorch(favColors.Blue);
    }

    void LightTorch(favColors favColor)
    {
        gameObject.tag = favColor + "Flame";
        flameRenderer.sprite = flameAniController.spriteSet[(int)favColor + 1];
        //unlit torch has no animation, so it will not be in the animator controller set
        flameAnimator.runtimeAnimatorController = flameAniController.controllerSet[(int)favColor];
        flameAnimator.enabled = true;
        flameLit = true;
        StartCoroutine(Burn());
    }

    IEnumerator Burn()
    {
        yield return new WaitForSeconds(flameDuration);
        Debug.Log("Finished burning");
        lightFlameTest = false;
        // reset to unlit sprite
        flameRenderer.sprite = flameAniController.spriteSet[0];
        flameAnimator.enabled = false;
        flameLit = false;
        Debug.Log(attractedMonsters.Count);
        foreach (var monster in attractedMonsters)
        {
            Debug.Log("In here");
            monster.flameInRange = false;
            monster.stare = false;
            Debug.Log(monster.stare);
            monster.heartExclaimation.SetActive(false);
        }
        gameObject.tag = "UnlitTorch";
        attractedMonsters.Clear();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("trigger enter");
        // only carry out checks if flame is lit
        if (flameLit)
        {
            if (col.gameObject.CompareTag("Untagged"))
            {
                CheckAndActivateEnemyProcedures(col.transform.parent.gameObject);
            }
            else
            {
                CheckAndActivateEnemyProcedures(col.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {

        // only carry out checks if flame is lit
        if (flameLit)
        {
            // TODO: check if monster name already in list, if so, don't carry out procedures
            if (col.gameObject.CompareTag("Untagged"))
            {

                CheckAndActivateEnemyProcedures(col.transform.parent.gameObject);

            }
            else
            {
                CheckAndActivateEnemyProcedures(col.gameObject);
            }
        }
    }

    private void CheckAndActivateEnemyProcedures(GameObject gameObject)
    {
        if (monsterNames.Contains(gameObject.name))
        {
            return;
        }
        Debug.Log(gameObject.tag);
        if (gameObject.CompareTag("Enemy"))
        {
            monsterNames.Add(gameObject.name);
            Debug.Log("Mesmerise");
            Debug.Log(gameObject.GetComponent<MonsterController>());
            attractedMonsters.Add(gameObject.GetComponent<MonsterController>());
        }
    }
}
