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
        return false;
    }

}