using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveModule : MonoBehaviour
{
    List<KeyCode> inputListX = new List<KeyCode>();
    List<KeyCode> inputListY = new List<KeyCode>();

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpPower = 10f;


    [Header("컴포넌트")]
    [SerializeField] GravityModule gravityModule;
    [SerializeField] Collider2D coll2D;


    private void Start()
    {
        SettingKeyboard();
    }

    private void FixedUpdate()
    {
        UpdateMove();
    }

    #region 이동 설정
    void SettingKeyboard()
    {

        IngameInputManager.instance.AddKeyboardAction_UpDown(KeyCode.A, () => RemoveKeyX(KeyCode.A), () => AddKeyX(KeyCode.A));

        IngameInputManager.instance.AddKeyboardAction_UpDown(KeyCode.D, () => RemoveKeyX(KeyCode.D), () => AddKeyX(KeyCode.D));

        IngameInputManager.instance.AddKeyboardAction_Down(KeyCode.Space, () => Jump());

        return;
    }


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
        Vector3 t_moveValue = Vector3.zero;
        // direction에 따라 캐릭터 이동 처리
        if (direction != Vector3.zero)
        {
            t_moveValue += direction * Time.fixedDeltaTime * moveSpeed;
        }

        if (this.gravityModule != null)
        {
            this.gravityModule.AddMovement(t_moveValue);
        }


        return t_moveValue;
    }

    void Jump()
    {
        this.gravityModule.AddJump(this.jumpPower);
    }

    void UpdateMove()
    {
        Vector3 moveValue = Vector3.zero;
        Vector3 moveDir = Vector3.zero;

        // 움직임 처리
        if (this.inputListX.Count > 0)
        {
            moveDir += GetDirectionFromKey(this.inputListX[inputListX.Count - 1]);
        }
        if (this.inputListY.Count > 0)
        {
            moveDir += GetDirectionFromKey(this.inputListY[inputListY.Count - 1]);

        }
        moveValue += Move(moveDir);


        return;
    }
    #endregion
}
