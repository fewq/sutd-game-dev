using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{
    private Transform target;
	private Vector2 direction;
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        target = Player.instance.transform;
		// controller.monsterController.ChaseTarget(target);
    }

}