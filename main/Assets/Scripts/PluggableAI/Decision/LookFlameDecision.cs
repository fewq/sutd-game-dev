using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/LookFlame")]
public class LookFlameDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool targetVisible = LookFlame(controller);
        return targetVisible;
    }

    private bool LookFlame(StateController controller)
    {
        // TODO: check if flame is still burning
        // if true:
        return false;
        // else if false, return to spawn point
    }

}