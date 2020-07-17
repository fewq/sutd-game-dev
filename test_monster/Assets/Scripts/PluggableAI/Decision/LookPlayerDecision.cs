using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/LookPlayer")]
public class LookPlayerDecision : Decision
{
    private float lookRange = 40f;
    private Vector2 sizeOfRaycast = new Vector2(5,5);
    private MonsterController monsterController;
    public override bool Decide(StateController controller)
    {
        if (monsterController == null)
        {
            monsterController = controller.monsterController;
        }
        bool targetVisible = LookPlayer(controller);
        return targetVisible;
    }

    private bool LookPlayer(StateController controller)
    {
        RaycastHit2D hit;
        Vector2 origin = controller.eyes.position;
        Vector2 direction = controller.eyes.forward;
        
        Debug.DrawRay(origin, direction.normalized * lookRange, Color.red);

        hit = Physics2D.BoxCast(origin, sizeOfRaycast, 0, direction, lookRange);
        if (hit!=null && hit.collider.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            // if not at spawn point, return to spawn point
            if (monsterController.m_SpawnPoint.position != monsterController.GetComponent<Transform>().position)
            {
                monsterController.ChaseTarget(monsterController.m_SpawnPoint);
            }
            return false;
        }
    }

}