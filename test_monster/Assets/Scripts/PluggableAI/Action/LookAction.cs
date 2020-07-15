using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Look")]
public class LookAction : Action
{

    public override void Act(StateController controller)
    {
		CheckSurroundings(controller);
    }

	void CheckSurroundings(StateController controller)
	{
		// TODO: check if flame present in a 7x7 radius
		// TODO: check if player present in a 5x5 radius
		// move towards the flame/player is true
		// play either exclaim (player) or heart animation (flame)
		Debug.Log("Check surroundings");
	}

}