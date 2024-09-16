using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] NPCInteractButton button;
    [SerializeField] Vector2 interactArea;
    [SerializeField] InteractType interactType;
    [SerializeField] protected bool isNowInInteractArea;
    [SerializeField] protected bool isCanInteract;
    [SerializeField] LayerMask interactLayerMask;
    [SerializeField] GameObject interactButton;
    [SerializeField] float interactTime;

    public virtual void Initialization()
    {
        this.button.action -= InteractWait;
        this.button.action += InteractWait;
        this.interactButton.SetActive(false);
    }
#if UNITY_EDITOR
    public virtual void Start()
    {
        Initialization();
    }
#endif
    protected virtual void FixedUpdate()
    {
        if (!this.isCanInteract) return;
        CheckInteractPlayer();
        if (this.isNowInInteractArea)
            interactButton.SetActive(true);
        else
            interactButton.SetActive(false);
    }

    void CheckInteractPlayer()
    {
        if (!this.isCanInteract) return;
        var t_colls = Physics2D.OverlapBoxAll(this.transform.position, this.interactArea, 0f, this.interactLayerMask);
        if (t_colls.Length == 0)
        {
            this.isNowInInteractArea = false;
            InGameManager.instance.GetPlayerController().RemoveNotInteractNPC(this);
        }
        else
        {
            this.isNowInInteractArea = true;
            InGameManager.instance.GetPlayerController().AddNowInteractNPC(this);
        }

    }

    public virtual void InteractWait()
    {
        if (!this.isCanInteract) return;
        if (this.interactType == InteractType.Tap)
        {
            InteractAction();
        }
        else if (this.interactType == InteractType.Hold)
        {
            if (InGameManager.instance.GetPlayerController().HoldInteract(this.interactTime))
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



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position, this.interactArea);
    }
}
