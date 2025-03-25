using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    Queue<TurnSequence> Turn;
    TurnSequence currentSequence;

    void SequenceAction()
    {
        this.currentSequence = Turn.Dequeue();
        this.currentSequence.SequenceAction();
    }

    void NextTurn()
    {

    }

    class TurnSequence
    {
        Action BeforeSequence;
        Action AfterSequence;

        SequenceState currentState;

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
}
