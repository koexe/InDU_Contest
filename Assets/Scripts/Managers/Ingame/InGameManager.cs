using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public enum GameState
    {
        InProgress = 0,
        Pause      = 1,
        InEvent    = 2,

    }
    public static InGameManager instance;

    public GameState state;
    private void Awake()
    {
        instance = this;
        return;
    }
}
