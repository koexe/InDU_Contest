using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : NPCController
{
    public override void InteractAction()
    {
        base.InteractAction();
        UIManager.instance.ShowUI("SaveUI");
    }
}
