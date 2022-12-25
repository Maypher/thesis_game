using System;
using System.Collections.Generic;
using UnityEngine;

// Thanks to x1r15/PitilT for the state machine implementation
// https://github.com/x1r15/StateMachine/tree/master/Scripts/StateMachine
//https://www.youtube.com/watch?v=HK2gEE1ugZk

// Inherited by the controller. Used to actually handle states
public abstract class StateMachine<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    protected List<State<T>> _states;
    [SerializeField] private State<T> _activeState;
    public State<T> PreviousState { get; private set; };

    // Initialize the StateMachine with the first state in _states
    protected virtual void Awake()
    {
        SetState(_states[0].GetType());   
    }

    public void SetState(Type newStateType)
    {
        if (_activeState != null) _activeState.Exit();

        PreviousState = _activeState;
        _activeState = _states.Find(x => x.GetType() == newStateType);
        _activeState.Init(GetComponent<T>());
    }

    public State<T> GetState(Type state)
    {
        return _states.Find(s => s.GetType() == state);
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

    void FixedUpdate()
    {
        _activeState.FixedUpdate();
    }
}
