using System.Collections.Generic;
using System;
using UnityEngine;


public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new();
    protected BaseState<EState> CurrentState;

    private void Start() 
    {
        CurrentState.EnterState();
    }
    private void Update() 
    {
        EState nextStateKey = CurrentState.GetNextState();

        if (nextStateKey.Equals(CurrentState.StateKey))
            CurrentState.UpdateState();
        else
            ChangeState(nextStateKey);
    }

    public void ChangeState(EState stateKey)
    {
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
    }

    private void FixedUpdate() 
    {
        CurrentState.FixedUpdateState();
    }
    private void OnTriggerEnter(Collider other) 
    {
        CurrentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other) 
    {
        CurrentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }
}