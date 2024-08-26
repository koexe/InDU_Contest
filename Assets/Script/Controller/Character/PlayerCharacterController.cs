using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    private Stack<KeyCode> inputStack = new Stack<KeyCode>();

    void Update()
    {
        // WASD �Է� ó��
        if (Input.GetKeyDown(KeyCode.W))
        {
            inputStack.Push(KeyCode.W);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            inputStack.Push(KeyCode.A);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            inputStack.Push(KeyCode.S);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            inputStack.Push(KeyCode.D);
        }

        // Ű�� ������ �� ���ÿ��� ����
        if (Input.GetKeyUp(KeyCode.W))
        {
            RemoveKey(KeyCode.W);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            RemoveKey(KeyCode.A);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            RemoveKey(KeyCode.S);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            RemoveKey(KeyCode.D);
        }

        // ���ÿ��� ���� �ֱ� Ű�� ���� �������� ĳ���� �̵�
        if (inputStack.Count > 0)
        {
            Move(GetDirectionFromKey(inputStack.Peek()));
        }
    }

    void RemoveKey(KeyCode key)
    {
        // �ش� Ű�� ���ÿ��� �����ϰ� �ٽ� �׾� �ø�
        if (inputStack.Contains(key))
        {
            var tempStack = new Stack<KeyCode>(inputStack.Count);
            foreach (var k in inputStack)
            {
                if (k != key)
                {
                    tempStack.Push(k);
                }
            }

            inputStack.Clear();
            while (tempStack.Count > 0)
            {
                inputStack.Push(tempStack.Pop());
            }
        }
    }

    Vector3 GetDirectionFromKey(KeyCode key)
    {
        // KeyCode�� ���� ���� ��ȯ
        switch (key)
        {
            case KeyCode.W: return Vector3.forward;
            case KeyCode.A: return Vector3.left;
            case KeyCode.S: return Vector3.back;
            case KeyCode.D: return Vector3.right;
            default: return Vector3.zero;
        }
    }

    void Move(Vector3 direction)
    {
        // direction�� ���� ĳ���� �̵� ó��
        transform.Translate(direction * Time.deltaTime * 5f);
    } 

}
