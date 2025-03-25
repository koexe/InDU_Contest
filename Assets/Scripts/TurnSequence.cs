using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurnSequence
{
    Action BeforeSequence;
    Action AfterSequence;

    [SerializeField] SequenceState currentState = SequenceState.Initialize;
    TurnSequence()
    {
        this.BeforeSequence = null;
        this.AfterSequence = null;

        this.currentState = SequenceState.BeforeAction;
    }

    public void AddBeforeSequence(Action _action)
    {
        BeforeSequence += _action;
    }

    public void AddAfterSequence(Action _action)
    {
        AfterSequence += _action;
    }

    public virtual void SequenceAction()
    {
        this.currentState = SequenceState.InAction;

    }

    public void ExecuteBeforeAction()
    {
        BeforeSequence?.Invoke();
        this.currentState = SequenceState.InAction;
    }
    public void ExecuteAfterAction()
    {
        AfterSequence?.Invoke();
        this.currentState = SequenceState.Done;
    }



    enum SequenceState
    {
        Initialize,
        BeforeAction,
        InAction,
        AfterAction,
        Done,
    }
}