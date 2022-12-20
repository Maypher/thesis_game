using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inherited by the controller. Used to actually handle states
public abstract class StateMachine<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private List<State<T>> _states;
    [SerializeField] private State<T> _activeState;

    // Initialize the StateMachine with the first state in _states
    protected virtual void Awake()
    {
        SetState(_states[0].GetType());   
    }

    public void SetState(Type newStateType)
    {
        if (_activeState != null) _activeState.Exit();

        _activeState = _states.Find(x => x.GetType() == newStateType);
        _activeState.Init(GetComponent<T>());
    }

    // Update is called once per frame
    void Update()
    {
        // CaptureInput() is ran first to update all variables that might be used in Update
        // Then run Update()
        // Finally check if the State need to change
        _activeState.CaptureInput();
        _activeState.Update();
        _activeState.ChangeState();
    }

    private void FixedUpdate()
    {
        _activeState.FixedUpdate();
    }
}
