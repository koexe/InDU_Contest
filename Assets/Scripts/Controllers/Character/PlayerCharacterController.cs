using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<KeyCode> inputListX = new List<KeyCode>();
    private List<KeyCode> inputListY = new List<KeyCode>();
    [Header("������Ʈ")]
    [SerializeField] DynamicGravity gravity;
    [SerializeField] Collider2D coll2D;

    [Header("�÷��̾� ����")]
    [SerializeField] float moveSpeed;

    [Header("������ ȹ��")]
    [SerializeField] LayerMask itemGetMask;
    [SerializeField] float itemGetDelay;
    [SerializeField] float currentItemGetDelay;


    [Header("���� ��ȣ�ۿ����� ������Ʈ")]
    List<NPCController> nowInteractNPC = new List<NPCController>();
    [SerializeField] float currentInteractTime;
    [SerializeField] bool isNowInteract;

    public void AddNowInteractNPC(NPCController npc)
    {
        if (!this.nowInteractNPC.Contains(npc))
            this.nowInteractNPC.Add(npc);
    }
    public void RemoveNotInteractNPC(NPCController npc)
    {
        if (this.nowInteractNPC.Contains(npc))
            this.nowInteractNPC.Remove(npc);
    }


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
        UpdateInteractTime();
        return;
    }
    void SettingKeyboard()
    {
        IngameInputManager.instance.AddKeyboardAction(KeyCode.W, () => AddKeyY(KeyCode.W));
        IngameInputManager.instance.AddKeyboardAction(KeyCode.A, () => AddKeyX(KeyCode.A));
        IngameInputManager.instance.AddKeyboardAction(KeyCode.S, () => AddKeyY(KeyCode.S));
        IngameInputManager.instance.AddKeyboardAction(KeyCode.D, () => AddKeyX(KeyCode.D));

        IngameInputManager.instance.AddKeyboardAction_Up(KeyCode.W, () => RemoveKeyY(KeyCode.W));
        IngameInputManager.instance.AddKeyboardAction_Up(KeyCode.S, () => RemoveKeyY(KeyCode.S));
        IngameInputManager.instance.AddKeyboardAction_Up(KeyCode.A, () => RemoveKeyX(KeyCode.A));
        IngameInputManager.instance.AddKeyboardAction_Up(KeyCode.D, () => RemoveKeyX(KeyCode.D));

        IngameInputManager.instance.AddKeyboardAction(KeyCode.Space, () => GetItem());
        IngameInputManager.instance.AddKeyboardAction(KeyCode.Space, () => UpdateInteract());
        return;
    }
    #region �̵� ����


    private void AddKeyX(KeyCode key)
    {
        if (!this.inputListX.Contains(key))
        {
            this.inputListX.Add(key);
        }
        return;
    }
    private void AddKeyY(KeyCode key)
    {
        if (!this.inputListY.Contains(key))
        {
            this.inputListY.Add(key);
        }
        return;
    }

    private void RemoveKeyX(KeyCode key)
    {
        if (this.inputListX.Contains(key))
        {
            this.inputListX.Remove(key);
        }
        return;
    }
    private void RemoveKeyY(KeyCode key)
    {
        if (this.inputListY.Contains(key))
        {
            this.inputListY.Remove(key);
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
            t_moveValue += direction * Time.fixedDeltaTime * 5f;
        }

        Vector3 t_push = this.gravity.UpdateCheckWall(t_moveValue);

        t_moveValue -= t_push;

        return t_moveValue;
    }

    void UpdateMove()
    {
        Vector3 moveValue = Vector3.zero;
        Vector3 moveDir = Vector3.zero;
        // ������ ó��
        if (this.inputListX.Count > 0)
        {
            moveDir += GetDirectionFromKey(this.inputListX[inputListX.Count - 1]);
        }
        if (this.inputListY.Count > 0)
        {
            moveDir += GetDirectionFromKey(this.inputListY[inputListY.Count - 1]);

        }
        moveValue += Move(moveDir);
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

    #region ��ȣ�ۿ�
    public bool HoldInteract(float _maxWaitTime)
    {
        this.isNowInteract = true;
        if(this.currentInteractTime != _maxWaitTime)
        {
            this.currentInteractTime = Mathf.MoveTowards(this.currentInteractTime, _maxWaitTime, Time.fixedDeltaTime);
            return false;
        }
        else
        {
            this.currentInteractTime = 0f;
            return true;
        }

    }
    void UpdateInteractTime()
    {
        if(!this.isNowInteract)
        {
            this.currentInteractTime = Mathf.MoveTowards(this.currentInteractTime,0,Time.fixedDeltaTime);
        }
        this.isNowInteract = false;
    }

    void UpdateInteract()
    {
        if(this.nowInteractNPC.Count != 0)
        {
            this.nowInteractNPC[0].InteractWait();
        }
    }

    #endregion
}