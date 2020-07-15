using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/LookPlayer")]
public class LookPlayerDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool targetVisible = LookPlayer(controller);
        return targetVisible;
    }

    private bool LookPlayer(StateController controller)
    {
        // TODO: check if player in 5x5 range -- likely to be same as LookAction
        // if true:
        return true;

        // else if false, return to spawn point
    }

}