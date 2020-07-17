using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

	public Transform eyes;
	public State currentState;
	public State remainState;
	public bool transitionStateChanged = false;

	public void TransitionToState(State nextState)
	{
		if (nextState == remainState) return;
		currentState = nextState;
		transitionStateChanged = true;
	}

	void Update()
	{
		currentState.UpdateState(this);
	}
}