using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<KeyCode> inputList = new List<KeyCode>();

    private void Start()
    {
        IngameInputManager.instance.AddKeyboardAction(KeyCode.W, () => AddKey(KeyCode.W));
        IngameInputManager.instance.AddKeyboardAction(KeyCode.A, () => AddKey(KeyCode.A));
        IngameInputManager.instance.AddKeyboardAction(KeyCode.S, () => AddKey(KeyCode.S));
        IngameInputManager.instance.AddKeyboardAction(KeyCode.D, () => AddKey(KeyCode.D));

        IngameInputManager.instance.AddKeyboardAction_Up(KeyCode.W, () => RemoveKey(KeyCode.W));
        IngameInputManager.instance.AddKeyboardAction_Up(KeyCode.S, () => RemoveKey(KeyCode.S));
        IngameInputManager.instance.AddKeyboardAction_Up(KeyCode.A, () => RemoveKey(KeyCode.A));
        IngameInputManager.instance.AddKeyboardAction_Up(KeyCode.D, () => RemoveKey(KeyCode.D));
    }

    private void Update()
    {
        // 입력 리스트에 값이 있을 때만 이동 처리
        if (this.inputList.Count > 0)
        {
            Move(GetDirectionFromKey(inputList[inputList.Count - 1]));
        }
    }

    private void AddKey(KeyCode key)
    {

        if (!inputList.Contains(key))
        {
            inputList.Add(key);
        }
    }

    private void RemoveKey(KeyCode key)
    {
        if (inputList.Contains(key))
        {
            inputList.Remove(key);
        }
        return;
    }

    private Vector3 GetDirectionFromKey(KeyCode key)
    {
        // KeyCode에 따라 방향 반환
        switch (key)
        {
            case KeyCode.W: return Vector3.up;
            case KeyCode.A: return Vector3.left;
            case KeyCode.S: return Vector3.down;
            case KeyCode.D: return Vector3.right;
            default: return Vector3.zero;
        }
    }

    private void Move(Vector3 direction)
    {
        if (InGameManager.instance.state != InGameManager.GameState.InProgress) return;

        // direction에 따라 캐릭터 이동 처리
        if (direction != Vector3.zero)
        {
            transform.position += direction * Time.deltaTime * 5f;
        }
    }
}