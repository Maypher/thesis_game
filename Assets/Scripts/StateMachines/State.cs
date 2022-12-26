
using UnityEngine;
using System;
using System.Collections.Generic;

// Abstract because it's used as a blueprint to create actual states.
// ScriptableObject to be able to create them from within the editor.
// <T> to give access to the character controller and its properties to states that inherit from this class.
public abstract class State<T> : ScriptableObject where T : MonoBehaviour
{
    // The controller of the game object
    // protected to only allow access to classes that inherit from this
    protected T controller;

    // Initialization method. Used to initialize variables and animations
    // Virtual to allow extension yet be able to call the predifined code with base.Init()
    public virtual void Init(T parent) => controller = parent;

    // Used for getting user input
    public abstract void CaptureInput();

    // Called each frame from the StateMachine
    public abstract void Update();

    // Called each physics frame from StateMachine
    public abstract void FixedUpdate();

    // Logic to change to another state (transitions)
    public abstract void ChangeState();

    // Called before initializing a new state. Used to clean up data and stop animations
    public abstract void Exit();

}
