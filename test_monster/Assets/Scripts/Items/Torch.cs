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
    private Animator flameAnimator;

    // Start is called before the first frame update
    void Start()
    {
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
        foreach (var monster in attractedMonsters)
        {
            monster.flameInRange = false;
            monster.stare = false;
            monster.heartExclaimation.SetActive(false);
        }
        attractedMonsters.Clear();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
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

    private void OnCollisionStay2D(Collision2D col)
    {

        // only carry out checks if flame is lit
        if (flameLit)
        {
            Debug.Log(gameObject.tag);

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
        Debug.Log(gameObject.tag);
        if (gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Mesmerise");
            Debug.Log(gameObject.GetComponent<MonsterController>());
            attractedMonsters.Add(gameObject.GetComponent<MonsterController>());
        }
    }
}
