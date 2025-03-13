using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPopup : PopUpUI
{
    const string optionUI = "Option";
    public override void Initialization(string _custom)
    {
        base.Initialization(_custom);

    }

    public void CloseTab()
    {
        UIManager.instance.DeleteUI(optionUI);
        return;
    }
}
