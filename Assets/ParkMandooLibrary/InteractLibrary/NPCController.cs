using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public enum InteractType
{
    Tap,
    Hold
}

public class NPCController : MonoBehaviour
{
    [Header("상호작용 옵션들")]
    [Header("상호작용 버튼")]
    [SerializeField] GameObject interactButton;
    [SerializeField] NPCInteractButton button;
    [Header("상호작용 범위")]
    [SerializeField] Vector2 interactArea;
    [Header("상호작용 방식(클릭, 홀드)")]
    [SerializeField] InteractType interactType;
    [Header("현재 상호작용 중인지")]
    [SerializeField] protected bool isNowInInteractArea;
    [Header("현재 상호작용 가능한지")]
    [SerializeField] protected bool isCanInteract;
    [Header("상호작용 확인할 레이어")]
    [SerializeField] LayerMask interactLayerMask;
    [Header("상호작용 대기 시간(홀드)")]
    [SerializeField] float interactTime;

    [Header("현재 상호작용 중인 컴포넌트")]
    [SerializeField] InteractModule interactModule;

    public virtual void Initialization()
    {
        this.button.action -= InteractWait;
        this.button.action += InteractWait;
        this.interactButton.SetActive(false);
    }

    public virtual void Start()
    {
        Initialization();
    }
    protected virtual void FixedUpdate()
    {
        if (!this.isCanInteract) return;
        CheckInteractPlayer();
        if (this.isNowInInteractArea)
            this.interactButton.SetActive(true);
        else
            this.interactButton.SetActive(false);
    }

    void CheckInteractPlayer()
    {
        if (!this.isCanInteract) return;
        var t_colls = Physics2D.OverlapBoxAll(this.transform.position, this.interactArea, 0f, this.interactLayerMask);
        if (t_colls.Length == 0)
        {
            this.isNowInInteractArea = false;
            if (this.interactModule != null)
            {
                this.interactModule.RemoveNotInteractNPC(this);
                this.interactModule = null;
            }
            else
                return;
        }
        else
        {
            this.isNowInInteractArea = true;
            foreach(var col in t_colls)
            {
                if(TryGetComponent<InteractModule>(out var t_module))
                {
                    this.interactModule = t_module;
                    this.interactModule.AddNowInteractNPC(this);
                }
            }
        }

    }

    public virtual void InteractWait()
    {
        if (!this.isCanInteract || this.interactModule == null) return;
        if (this.interactType == InteractType.Tap)
        {
            InteractAction();
        }
        else if (this.interactType == InteractType.Hold)
        {
            if (this.interactModule.HoldInteract(this.interactTime))
            {
                InteractAction();
            }
            else
            {
                return;
            }
        }
    }

    public virtual void InteractAction()
    {
        Debug.Log($"상호작용 오브젝트 이름:{this.name}");
        this.interactButton.SetActive(false);
    }



    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position, this.interactArea);
    }
}
