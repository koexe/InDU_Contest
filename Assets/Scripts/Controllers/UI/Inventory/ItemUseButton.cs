using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUseButton : PopUpUI
{
    [SerializeField] Button useButton;
    [SerializeField] Button trashButton;

    public Button GetUseButton() => this.useButton;
    public Button GetTrashButton() => this.trashButton;
    public override void Initialization(string _custom)
    {   
        base.Initialization(_custom);
    }
}
