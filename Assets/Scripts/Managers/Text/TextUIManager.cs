using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

    public override void Initialization(string _custom)
    {
        //DataManager에서 Dialog Dictionary 가져오기
        this.currentDialogDictionary = AssetManager.Instance.GetDialogList();
        this.currentDialogIndex = int.Parse(_custom);
        Debug.Log(_custom);
        this.textState = TextState.WAIT;

        this.dialogController.Initialization();
        this.imageController.Initialization();

        if (!SaveGameManager.instance.currentSaveData.chatacterDialogs[int.Parse(_custom)])
        {
            SaveGameManager.instance.currentSaveData.chatacterDialogs[int.Parse(_custom)] = true;
        }
        else
        {
            int index = int.Parse(_custom);
            while (true) 
            {
                if (this.currentDialogDictionary.ContainsKey(index))
                    index++;
                else
                    break;
            }
            this.currentDialogIndex = index -1;
        }

        this.dialogController.ChangeDialog(this.currentDialogIndex);
    }

    public void OnClickDialog()
    {
        this.DialogClickAction.Invoke();
    }

    public void EnableButtons(int _index1, int _index2 = -1, int _index3 = -1)
    {
        this.textState = TextState.CHOOSE;
        this.choiceButtonController.button1.onClick.AddListener(() => this.dialogController.ChangeDialogButton(_index1));
        this.choiceButtonController.button1.onClick.AddListener(() => this.imageController.OnDialogTextDown());


        if (_index2 != -1)
        {
            this.choiceButtonController.button2.onClick.AddListener(() => this.dialogController.ChangeDialogButton(_index2));
            this.choiceButtonController.button2.onClick.AddListener(() => this.imageController.OnDialogTextDown());
        }
        else
        {
            this.choiceButtonController.button2.enabled = false;
        }

        if (_index3 != -1)
        {
            this.choiceButtonController.button3.onClick.AddListener(() => this.dialogController.ChangeDialogButton(_index3));
            this.choiceButtonController.button3.onClick.AddListener(() => this.imageController.OnDialogTextDown());
        }
        else
        {
            this.choiceButtonController.button2.enabled = false;
        }

        this.choiceButtonController.ChangeChoiceText(this.currentDialogDictionary[this.currentDialogIndex]);
        this.choiceButtonController.gameObject.SetActive(true);
    }


}
