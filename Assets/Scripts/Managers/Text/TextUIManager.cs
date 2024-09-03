using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUIManager : PopUpUI
{
    [SerializeField] private DialogTextController dialogController;
    [SerializeField] private ImageController imageController;
    public ChoiceButtonController choiceButtonController;
    
    public Action DialogClickAction;
    public Dictionary<int, Dialog> currentDialogDictionary;
    public TextState textState;
    public int currentDialogIndex = 0;
    private void Awake()
    {
        Initialization();
    }

    public override void Initialization()
    {
        //DataManager에서 Dialog Dictionary 가져오기
        this.currentDialogDictionary = AssetManager.Instance.GetDialogList();
        this.textState = TextState.WAIT;

        this.dialogController.Initialization();
        this.imageController.Initialization();
        this.DialogClickAction.Invoke();
    }

    public void OnClickDialog()
    {
        this.DialogClickAction.Invoke();
    }

    public void EnableButtons(int index1, int index2, int index3)
    {
        this.textState = TextState.CHOOSE;
        this.choiceButtonController.button1.onClick.AddListener(() => this.dialogController.ChangeDialogButton(index1));
        this.choiceButtonController.button1.onClick.AddListener(() => this.imageController.OnDialogTextDown());
        this.choiceButtonController.button2.onClick.AddListener(() => this.dialogController.ChangeDialogButton(index2));
        this.choiceButtonController.button2.onClick.AddListener(() => this.imageController.OnDialogTextDown());
        this.choiceButtonController.button3.onClick.AddListener(() => this.dialogController.ChangeDialogButton(index3));
        this.choiceButtonController.button3.onClick.AddListener(() => this.imageController.OnDialogTextDown());
        this.choiceButtonController.ChangeChoiceText(this.currentDialogDictionary[this.currentDialogIndex]);

        this.choiceButtonController.gameObject.SetActive(true);
    }
}
