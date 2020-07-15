using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Kill")]
public class KillAction : Action
{

    public override void Act(StateController controller)
    {
		Kill(controller);
    }

	void Kill(StateController controller)
	{
		// TODO: wait for event on whether player is in range?
	}

}