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
    [Header("대화 옵션들")]
    [SerializeField] string npcName;
    [SerializeField] Dialog currentDialog;
    [SerializeField] int currentDialogIndex;
    [SerializeField] int[] allDialogIndex;

    [Header("상호작용 옵션들")]
    [SerializeField] NPCInteractButton button;
    [SerializeField] Vector2 interactArea;
    [SerializeField] InteractType interactType;
    [SerializeField] bool isNowInInteractArea;
    [SerializeField] bool isCanInteract;
    [SerializeField] LayerMask interactLayerMask;
    [SerializeField] GameObject interactButton;
    [SerializeField] float interactTime;

    [Header("UI")]
    [SerializeField] GameObject dialogUI;


    public void Initialization()
    {
        this.button.action -= InteractWait;
        this.button.action += InteractWait;
    }
#if UNITY_EDITOR
    public void Start()
    {
        this.button.action -= InteractWait;
        this.button.action += InteractWait;
    }
#endif
    private void FixedUpdate()
    {
        CheckInteractPlayer();
        if (this.isNowInInteractArea)
            interactButton.SetActive(true);
        else
            interactButton.SetActive(false);
    }

    void CheckInteractPlayer()
    {
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
        UIManager.instance.ShowUI(this.dialogUI,dialogUI.GetComponent<PopUpUI>().GetUiName() , -1, this.currentDialogIndex.ToString());
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position, this.interactArea);
    }
}
