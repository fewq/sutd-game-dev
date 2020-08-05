using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Distracted")]
public class DistractedAction : Action
{
	private MonsterController monsterController;
    public override void Act(StateController controller)
    {
		if (monsterController == null)
        {
            monsterController = controller.monsterController;
        }
		LookAtFlame(controller);
    }

	void LookAtFlame(StateController controller)
	{
		// TODO: Make monster face the flame + heart animation
		//monsterController.heartExclaimation.SetActive(true);
		//monsterController.ChaseTarget(flame);
	}

}