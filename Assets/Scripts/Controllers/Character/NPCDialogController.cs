using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogController : NPCController
{
    [Header("대화 옵션들")]
    [SerializeField] string npcName;
    [SerializeField] Dialog currentDialog;
    [SerializeField] int currentDialogIndex;
    [SerializeField] int[] allDialogIndex;

    [Header("UI")]
    [SerializeField] GameObject dialogUI;
    public override void InteractAction()
    {
        base.InteractAction();
        UIManager.instance.ShowUI(this.dialogUI, dialogUI.GetComponent<PopUpUI>().GetUiName(), -1, this.currentDialogIndex.ToString());
    }

}
