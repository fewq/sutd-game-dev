using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject
{

	public Action[] actions;
	public Transition[] transitions;
	public Color sceneGizmoColor = Color.grey;

    public void UpdateState(StateController controller)
	{
		DoActions(controller);
		CheckTransitions(controller);
	}

	private void DoActions(StateController controller)
	{
		for (int i = 0; i < actions.Length; i++) actions[i].Act(controller);
	}

	private void CheckTransitions(StateController controller)
	{
		controller.transitionStateChanged = false; // reset
		for (int i = 0; i < transitions.Length; ++i)
		{
			// check if the previous transition has caused a change. If yes, stop. Let Update() in StateController run again in the next state
			if (controller.transitionStateChanged)
			{
				break;
			}
			bool decisionSucceded = transitions[i].decision.Decide(controller);
			if (decisionSucceded)
			{
				controller.TransitionToState(transitions[i].trueState);
			}
			else
			{
				controller.TransitionToState(transitions[i].falseState);
			}
		}
	}

}