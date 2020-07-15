using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{

    public override void Act(StateController controller)
    {
		Chase(controller);
    }

	private void Chase(StateController controller)
	{
		Debug.Log("Chase!");
	}

}