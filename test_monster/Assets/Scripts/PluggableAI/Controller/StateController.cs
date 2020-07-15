using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

	public State currentState;
	public Transform spawnLocation;
	public Transform eyes;
	public State remainState;
	public bool transitionStateChanged = false;

	[HideInInspector] public List<Transform> wayPointList;
	[HideInInspector] public int nextWayPoint;
	[HideInInspector] public Transform chaseTarget;

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