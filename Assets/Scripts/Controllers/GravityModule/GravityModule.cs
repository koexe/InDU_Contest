using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityModule : MonoBehaviour
{


    [Header("현재 상태")]
    [SerializeField]
    protected bool isGrounded;

    [SerializeField]
    float currentGravitySpeed;

    [SerializeField]
    Vector2 currentOverlap;

    [SerializeField]
    Collider2D[] currentAttachColliders;

    [SerializeField] Vector2 currentMovementInput;

    [SerializeField] float currentJumpPower;

    [Space(10)]
    [Header("Components")]
    [SerializeField] Transform myTransform;

    [SerializeField] Collider2D colliderComponent;

    [Space(10)]
    [Header("Gravity Mask Settings")]
    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    LayerMask platformMask;

    [Space(10)]
    [Header("Gravity Settings")]
    [SerializeField] float gravityForce = 9.8f;

    [SerializeField] float jumpForce = 10f;

    [SerializeField] float maxGravitySpeed = 10.0f;

    [SerializeField] float groundPushOffset = 0.1f;


    private void Start()
    {
        if (this.myTransform == null)
            this.myTransform = this.transform;
        if (this.colliderComponent == null)
        {
            if (this.transform.TryGetComponent<Collider2D>(out Collider2D t_colliderComponent))
            {
                this.colliderComponent = t_colliderComponent;
            }
            else
            {
                Debug.Log("콜라이더 컴포넌트가 없습니다");
            }
        }
    }

    #region Gravity Update Process
    private void FixedUpdate()
    {
        Vector2 t_Result = this.myTransform.position;
        if (!this.isGrounded)
        {
            t_Result = ApplyGravity(t_Result);
        }
        t_Result = UpdateJumpPower(t_Result);
        t_Result = UpdateInputMovement(t_Result);
        t_Result = PushByGround(t_Result);


        this.myTransform.position = t_Result;
    }


    Vector2 ApplyGravity(Vector2 _currentPosition)
    {
        this.currentGravitySpeed += gravityForce * Time.fixedDeltaTime;
        _currentPosition.y -= this.currentGravitySpeed * Time.fixedDeltaTime;
        Vector2 _returnVector = _currentPosition;
        this.currentGravitySpeed = Mathf.Clamp(this.currentGravitySpeed, 0f, maxGravitySpeed);
        return _returnVector;
    }

    /// <summary>
    /// This method is push caused by the ground.
    /// </summary>
    /// <param name="_currentPosition"></param>
    /// <returns></returns>
    Vector2 PushByGround(Vector2 _currentPosition)
    {
        Vector2 _returnVector = _currentPosition;
        Collider2D[] t_attachedColliders = Physics2D.OverlapBoxAll(_returnVector, this.colliderComponent.bounds.size + new Vector3(0.15f, 0.15f), 0f, this.groundLayer);
        this.currentAttachColliders = t_attachedColliders;
        bool t_isColliderBottom = false;

        if (t_attachedColliders.Length > 0)
        {
            foreach (var t_collider in t_attachedColliders)
            {
                Rect t_colliderRect = new Rect(t_collider.bounds.min, t_collider.bounds.size);

                Rect t_playerRect = new Rect(
                    new Vector2(_returnVector.x - (this.colliderComponent.bounds.size.x / 2), _returnVector.y - (this.colliderComponent.bounds.size.y / 2)),
                    this.colliderComponent.bounds.size);

                Rect t_overlapRect = ParkMandooUtility.GetOverlapRect(t_playerRect, t_colliderRect);

                Vector2 t_overlap = CalculateOveralp(t_playerRect, t_overlapRect);
                //this.currentOverlap = t_overlap;
                if (Mathf.Abs(t_overlap.y) > 0f)
                    t_isColliderBottom = true;
                _returnVector = ApplayGroundPush(_returnVector, t_overlap);
            }
            if (t_isColliderBottom)
                OnEnterGround();
        }
        else
        {
            OnExitGround();
        }

        return _returnVector;
    }
    /// <summary>
    /// Calculate Overlaped Ground
    /// </summary> 
    /// <param name="_overlapRect"></param>
    /// <returns></returns>
    Vector2 CalculateOveralp(Rect _playerRect, Rect _overlapRect)
    {
        Vector2 _result = Vector2.zero;

        //상하
        if (Math.Abs(_overlapRect.width) > Math.Abs(_overlapRect.height))
        {

            if (_overlapRect.yMin > _playerRect.center.y)
                _result.y = -_overlapRect.height;
            else
                _result.y = _overlapRect.height;
        }
        else if (Math.Abs(_overlapRect.width) < Math.Abs(_overlapRect.height))
        {
            if (_overlapRect.xMin > _playerRect.center.x)
                _result.x = -_overlapRect.width;
            else
                _result.x = _overlapRect.width;
        }

        return _result;
    }
    /// <summary>
    /// Push positon by Overlaped Area
    /// </summary>
    /// <param name="_currentPosition"></param>
    /// <param name="_overlapSize"></param>
    /// <returns></returns>
    Vector2 ApplayGroundPush(Vector2 _currentPosition, Vector2 _overlapSize)
    {
        Vector2 _result = _currentPosition;


        if (_overlapSize.y < -0.01f)
        {
            _overlapSize.y -= groundPushOffset;

        }
        else if (_overlapSize.y > 0.01f)
        {
            _overlapSize.y += groundPushOffset;
        }
        else
        {
            if (this.isGrounded && _overlapSize.x == 0.0f)
                OnExitGround();

            _overlapSize.y = 0.0f;
        }

        if (_overlapSize.x < -0.01f)
        {
            _overlapSize.x -= groundPushOffset;
        }
        else if (_overlapSize.x > 0.01f)
        {
            _overlapSize.x += groundPushOffset;
        }
        else
        {
            _overlapSize.x = 0.0f;
        }



        _result += _overlapSize;
        return _result;
    }


    Vector2 UpdateInputMovement(Vector2 _currentPosition)
    {
        Vector2 _result = _currentPosition += this.currentMovementInput;

        this.currentMovementInput = Vector2.zero;

        return _result;

    }
    Vector2 UpdateJumpPower(Vector2 _currentPosition)
    {
        Vector2 _result = _currentPosition;
        _result.y += this.currentJumpPower;
        return _result;
    }
    #endregion

    #region Public Method

    public void AddMovement(Vector2 _movement)
    {
        this.currentMovementInput = _movement;
    }

    public void AddJump(float _jumpPower)
    {
        if (this.isGrounded)
            this.currentJumpPower = _jumpPower * Time.fixedDeltaTime;
    }
    #endregion

    void OnEnterGround()
    {
        if (this.currentJumpPower == 0)
        {
            this.isGrounded = true;
            this.currentGravitySpeed = 0f;
            this.currentJumpPower = 0f;
        }
        else
        {
            if (this.currentJumpPower <= this.currentGravitySpeed)
            {
                this.isGrounded = true;
                this.currentGravitySpeed = 0f;
                this.currentJumpPower = 0f;
            }
        }

    }

    void OnExitGround()
    {
        this.isGrounded = false;
    }

    private void OnDrawGizmos()
    {
        if (currentAttachColliders != null || this.currentAttachColliders.Length != 0)
        {
            Gizmos.color = Color.yellow;
            foreach (var collider in currentAttachColliders)
            {
                Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
            }
        }

    }
}
