using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Distracted")]
public class DistractedAction : Action
{

    public override void Act(StateController controller)
    {
		LookAtFlame(controller);
    }

	void LookAtFlame(StateController controller)
	{
		// TODO: Make monster face the flame + heart animation
	}

}