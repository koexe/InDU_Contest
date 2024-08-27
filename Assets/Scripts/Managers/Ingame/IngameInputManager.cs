using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameInputManager : MonoBehaviour
{
    public static IngameInputManager instance;
    public delegate void KeyAction();
    Dictionary<KeyCode, KeyAction> KeyBoardActions_Down;
    Dictionary<KeyCode, KeyAction> KeyBoardActions;
    Dictionary<KeyCode, KeyAction> KeyBoardActions_Up;


    private void Awake()
    {
        instance = this;
        this.KeyBoardActions = new Dictionary<KeyCode, KeyAction>();
        this.KeyBoardActions_Down = new Dictionary<KeyCode, KeyAction>();
        this.KeyBoardActions_Up = new Dictionary<KeyCode, KeyAction>();
        return;
    }

    private void Update()
    {
        foreach(var t_Action in KeyBoardActions_Down)
        {
            if(Input.GetKeyDown(t_Action.Key))
            {
                t_Action.Value.Invoke();
            }
        }
        foreach (var t_Action in KeyBoardActions_Up)
        {
            if (Input.GetKeyUp(t_Action.Key))
            {
                t_Action.Value.Invoke();
            }
        }
        foreach (var t_Action in KeyBoardActions)
        {
            if (Input.GetKey(t_Action.Key))
            {
                t_Action.Value.Invoke();
            }
        }
        return;
    }
    public void AddKeyboardAction_Down(KeyCode _Key, KeyAction _Action)
    {

        if (!this.KeyBoardActions_Down.ContainsKey(_Key))
        {
            this.KeyBoardActions_Down.Add(_Key, _Action);
            return;
        }
        else
        {
            Debug.Log("이미 추가된 키코드입니다. 키 액선을 추가합니다.");
            this.KeyBoardActions_Down[_Key] += _Action;
            return;
        }
    }
    public void AddKeyboardAction_Up(KeyCode _Key, KeyAction _Action)
    {

        if (!this.KeyBoardActions_Up.ContainsKey(_Key))
        {
            this.KeyBoardActions_Up.Add(_Key, _Action);
            return;
        }
        else
        {
            Debug.Log("이미 추가된 키코드입니다. 키 액선을 추가합니다.");
            this.KeyBoardActions_Up[_Key] += _Action;
            return;
        }
    }

    public void AddKeyboardAction(KeyCode _Key, KeyAction _Action)
    {

        if (!this.KeyBoardActions.ContainsKey(_Key))
        {
            this.KeyBoardActions.Add(_Key, _Action);
            return;
        }
        else
        {
            Debug.Log("이미 추가된 키코드입니다. 키 액선을 추가합니다.");
            this.KeyBoardActions[_Key] += _Action;
            return;
        }
    }

}
