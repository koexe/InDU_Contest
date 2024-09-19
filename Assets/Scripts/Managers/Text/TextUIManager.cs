using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection;
using UnityEngine;

public class TextUIManager : PopUpUI
{
    [SerializeField] private DialogTextController dialogController;
    [SerializeField] private ImageController imageController;
    [SerializeField] GameObject backGround;
    public ChoiceButtonController choiceButtonController;

    public Action DialogClickAction;
    public Dictionary<int, Dialog> currentDialogDictionary;
    public TextState textState;
    public int currentDialogIndex = 0;

    public override void Initialization(string _custom)
    {
        InGameManager.instance.state = InGameManager.GameState.Pause;
        base.Initialization(_custom);
        //DataManager에서 Dialog Dictionary 가져오기
        this.currentDialogDictionary = AssetManager.Instance.GetDialogList();
        this.currentDialogIndex = int.Parse(_custom);

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
            this.currentDialogIndex = index - 1;

            if (!string.IsNullOrEmpty(currentDialogDictionary[currentDialogIndex].linkCondition[0]))
                CheckLink();

        }
        this.dialogController.ChangeDialog(this.currentDialogIndex);
        this.imageController.OnDialogTextDown();


    }

    public override void DeleteUI()
    {
        base.DeleteUI();
        InGameManager.instance.state = InGameManager.GameState.InProgress;
    }

    public void CheckLink()
    {


        if (SaveGameManager.instance.currentSaveData.chatacterDialogs[int.Parse(this.currentDialogDictionary[currentDialogIndex].linkCondition[0])] == bool.Parse(this.currentDialogDictionary[currentDialogIndex].linkCondition[1]))
        {
            int nextDialog = int.Parse(this.currentDialogDictionary[currentDialogIndex].linkDilog);

            if (SaveGameManager.instance.currentSaveData.chatacterDialogs[nextDialog] == true)
            {
                int index = nextDialog;
                while (true)
                {
                    if (this.currentDialogDictionary.ContainsKey(index))
                        index++;
                    else
                        break;
                }
                nextDialog = index - 1;
                this.currentDialogIndex = nextDialog;
            }

        }
    }



    public void OnClickDialog()
    {
        this.DialogClickAction.Invoke();
        SaveGameManager.instance.currentSaveData.chatacterDialogs[this.currentDialogIndex] = true;
    }

    public void EnableButtons(int _index1, int _index2 = -1, int _index3 = -1)
    {
        this.textState = TextState.CHOOSE;
        this.choiceButtonController.button1.onClick.RemoveAllListeners();
        this.choiceButtonController.button2.onClick.RemoveAllListeners();
        this.choiceButtonController.button3.onClick.RemoveAllListeners();

        this.choiceButtonController.button1.onClick.AddListener(() => this.dialogController.ChangeDialogButton(_index1));
        this.choiceButtonController.button1.onClick.AddListener(() => this.imageController.OnDialogTextDown());

        if (_index2 == -1)
        {
            this.choiceButtonController.button2.gameObject.SetActive(false);
        }
        else
        {
            this.choiceButtonController.button2.onClick.AddListener(() => this.dialogController.ChangeDialogButton(_index2));
            this.choiceButtonController.button2.onClick.AddListener(() => this.imageController.OnDialogTextDown());
            this.choiceButtonController.button2.gameObject.SetActive(true);
        }

        if (_index3 == -1)
        {
            this.choiceButtonController.button3.gameObject.SetActive(false);
        }
        else
        {
            this.choiceButtonController.button3.onClick.AddListener(() => this.dialogController.ChangeDialogButton(_index3));
            this.choiceButtonController.button3.onClick.AddListener(() => this.imageController.OnDialogTextDown());
            this.choiceButtonController.button3.gameObject.SetActive(true);
        }

        this.choiceButtonController.ChangeChoiceText(this.currentDialogDictionary[this.currentDialogIndex]);
        this.choiceButtonController.gameObject.SetActive(true);
    }

    public void BackGroundChange(bool _onOff)
    {
        this.backGround.SetActive(_onOff);
        return;
    }
}
