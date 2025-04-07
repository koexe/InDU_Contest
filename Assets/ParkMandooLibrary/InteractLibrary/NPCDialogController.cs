using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogController : NPCController
{
    [Header("��ȭ �ɼǵ�")]
    [SerializeField] string npcName;
    [SerializeField] Dialog currentDialog;
    [SerializeField] int currentDialogIndex;
    [SerializeField] int[] allDialogIndex;

    [Header("UI")]
    [SerializeField] GameObject dialogUI;
    public override void InteractAction()
    {
        base.InteractAction();
        UIManager.instance.ShowUI("DialogUI", -1, this.currentDialogIndex.ToString());
    }

}
