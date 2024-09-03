using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<KeyCode> inputList = new List<KeyCode>();
    [Header("������Ʈ")]
    [SerializeField] DynamicGravity gravity;
    [SerializeField] Collider2D coll2D;
    [Header("�÷��̾� ����")]
    [SerializeField] float moveSpeed;
    [Header("������ ȹ��")]
    [SerializeField] LayerMask itemGetMask;
    [SerializeField] float itemGetDelay;
    [SerializeField] float currentItemGetDelay;

    private void Start()
    {
        SettingKeyboard();
        return;
    }

    public void Initialization()
    {
        SettingKeyboard();
        return;
    }
    private void FixedUpdate()
    {
        UpdateMove();
        UpdateGetItemDelay();
        return;
    }

    #region �̵� ����
    void SettingKeyboard()
    {
        IngameInputManager.instance.AddKeyboardAction(KeyCode.W, () => AddKey(KeyCode.W));
        IngameInputManager.instance.AddKeyboardAction(KeyCode.A, () => AddKey(KeyCode.A));
        IngameInputManager.instance.AddKeyboardAction(KeyCode.S, () => AddKey(KeyCode.S));
        IngameInputManager.instance.AddKeyboardAction(KeyCode.D, () => AddKey(KeyCode.D));

        IngameInputManager.instance.AddKeyboardAction_Up(KeyCode.W, () => RemoveKey(KeyCode.W));
        IngameInputManager.instance.AddKeyboardAction_Up(KeyCode.S, () => RemoveKey(KeyCode.S));
        IngameInputManager.instance.AddKeyboardAction_Up(KeyCode.A, () => RemoveKey(KeyCode.A));
        IngameInputManager.instance.AddKeyboardAction_Up(KeyCode.D, () => RemoveKey(KeyCode.D));

        IngameInputManager.instance.AddKeyboardAction(KeyCode.Space, () => GetItem());
        return;
    }

    private void AddKey(KeyCode key)
    {
        if (!this.inputList.Contains(key))
        {
            this.inputList.Add(key);
        }
        return;
    }

    private void RemoveKey(KeyCode key)
    {
        if (this.inputList.Contains(key))
        {
            this.inputList.Remove(key);
        }
        return;
    }

    private Vector3 GetDirectionFromKey(KeyCode key)
    {
        // KeyCode�� ���� ���� ��ȯ
        switch (key)
        {
            case KeyCode.W: return Vector3.up;
            case KeyCode.A: return Vector3.left;
            case KeyCode.S: return Vector3.down;
            case KeyCode.D: return Vector3.right;
            default: return Vector3.zero;
        }
    }

    private Vector3 Move(Vector3 direction)
    {
        if (InGameManager.instance.state != InGameManager.GameState.InProgress) return Vector3.zero;

        Vector3 t_moveValue = Vector3.zero;
        // direction�� ���� ĳ���� �̵� ó��
        if (direction != Vector3.zero)
        {
            t_moveValue += direction * Time.deltaTime * 5f;
        }

        Vector3 t_push = this.gravity.UpdateCheckWall(t_moveValue);

        t_moveValue -= t_push;

        return t_moveValue;
    }

    void UpdateMove()
    {
        Vector3 moveValue = Vector3.zero;
        Vector3 moveDir;
        // ������ ó��
        if (this.inputList.Count > 0)
        {
            moveDir = GetDirectionFromKey(this.inputList[inputList.Count - 1]);
            // �и� ó�� �� �̵�
            moveValue += Move(moveDir);
        }
        // �浹 üũ �� ���� �и� ó��

        this.transform.position += moveValue;
        return;
    }


    #endregion
    #region ������ ȹ��
    public void GetItem()
    {
        if (this.currentItemGetDelay != 0f) return;
        var items = Physics2D.OverlapBoxAll(this.coll2D.bounds.center, this.coll2D.bounds.size, 0f, this.itemGetMask);
        foreach (var item in items)
        {
            var t_MapItem = item.GetComponent<MapItem>();
            t_MapItem.GetItem();
           
        }
        return;
    }
    void UpdateGetItemDelay()
    {
        if (this.currentItemGetDelay != 0f)
        {
            return;
        }
        else
        {
            this.currentItemGetDelay = Mathf.MoveTowards(this.currentItemGetDelay, 0f, Time.fixedDeltaTime);
        }
    }

    #endregion
}