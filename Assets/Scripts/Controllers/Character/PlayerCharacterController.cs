using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<KeyCode> inputList = new List<KeyCode>();
    [Header("컴포넌트")]
    [SerializeField] DynamicGravity gravity;
    [SerializeField] Collider2D coll2D;
    [Header("플레이어 스텟")]
    [SerializeField] float moveSpeed;
    [Header("아이템 획득")]
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

    #region 이동 설정
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

    private Vector3 Move(Vector3 direction)
    {
        if (InGameManager.instance.state != InGameManager.GameState.InProgress) return Vector3.zero;

        Vector3 t_moveValue = Vector3.zero;
        // direction에 따라 캐릭터 이동 처리
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
        // 움직임 처리
        if (this.inputList.Count > 0)
        {
            moveDir = GetDirectionFromKey(this.inputList[inputList.Count - 1]);
            // 밀림 처리 후 이동
            moveValue += Move(moveDir);
        }
        // 충돌 체크 후 먼저 밀림 처리

        this.transform.position += moveValue;
        return;
    }


    #endregion
    #region 아이템 획득
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