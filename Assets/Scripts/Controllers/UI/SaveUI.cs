using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveUI : PopUpUI
{
    public override void Initialization(string _custom = "")
    {
        base.Initialization(_custom);
        InGameManager.instance.state = InGameManager.GameState.Pause;
    }

    public override void DeleteUI()
    {
        base.DeleteUI();
        InGameManager.instance.state = InGameManager.GameState.InProgress;
    }



    public void Yes()
    {
        SaveGameManager.instance.SavetoFile();
        DeleteUI();
    }
    public void No()
    {
        DeleteUI();
    }

}
