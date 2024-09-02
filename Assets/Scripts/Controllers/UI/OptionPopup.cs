using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPopup : PopUpUI
{
    const string optionUI = "Option";
    public override void Initialization()
    {
        base.Initialization();

    }

    public void CloseTab()
    {
        UIManager.instance.DeleteUI(optionUI);
        return;
    }
}
