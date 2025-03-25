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
}
