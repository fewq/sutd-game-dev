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
        return false;
    }

}