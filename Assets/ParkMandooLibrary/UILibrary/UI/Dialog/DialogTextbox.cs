using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
public enum TextBoxState
{
    NotInitialized,
    WAIT,
    INPRINT,
    CHOOSE,
    PAUSE,
    NotShow,
}
public class DialogTextbox : UIBase
{
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextBoxState textState;

    [Header("Current Dialog Info")]
    [SerializeField] int currentIndex;
    [SerializeField] Dialog[] currentDialog;

    Coroutine printCoroutine;
    public float printSpeed = 1f;

    public override void Initialization(UIData _data)
    {
        this.textState = TextBoxState.NotInitialized;
        DialogTextboxData t_data = _data as DialogTextboxData;
        if (t_data != null)
        {
            this.currentDialog = t_data.dialogs.ToArray();
        }
        else
        {
            Debug.Log("Data Type not Match!!");
        }
        this.dialogText.text = "";
        this.nameText.text = "";
        this.textState = TextBoxState.NotShow;
        this.currentIndex = 0;
    }

    public override void Show(UIData _data)
    {
        DialogTextboxData t_data = _data as DialogTextboxData;
        if (t_data != null)
        {
            this.currentDialog = t_data.dialogs.ToArray();
        }
        else
        {
            Debug.Log("Data Type not Match!!");
        }
        this.dialogText.text = "";
        this.nameText.text = "";
        this.currentIndex = 0;
        this.textState = TextBoxState.WAIT;
        ChangeDialog(this.currentIndex);
        this.isShow = true;
    }

    public override void Hide()
    {
        this.gameObject.SetActive(false);
        this.isShow = false;
    }


    #region 텍스트 변경 기능
    /// <summary>
    /// On Click TextBox
    /// </summary>
    public void OnDialogTextDown()
    {
        //현재 텍스트 진행 상황에 따른 처리
        switch (this.textState)
        {
            case TextBoxState.WAIT:
                this.currentIndex += 1;
                ChangeDialog(this.currentIndex);
                break;
            case TextBoxState.INPRINT:
                this.currentIndex += 1;
                StopCoroutine(this.printCoroutine);
                ChangeDialog(this.currentIndex);
                break;
            case TextBoxState.CHOOSE:
            case TextBoxState.PAUSE:
            default:
                break;
        }
    }
    //다이얼로그 한번에 바꾸기
    public void ChangeDialog(int index)
    {
        var dialogTemp = this.currentDialog[index];
        PrintAll(dialogTemp.comment, index, 0f);
        return;
    }
    //다이얼로그 하나씩 출력하기
    public void ChangeDialogOneByOne(int index)
    {
        var dialogTemp = this.currentDialog[index];
        this.printCoroutine = StartCoroutine(PrintRoutine(dialogTemp.comment, index, this.printSpeed));
    }

    public void PrintAll(string _dialog, int _index, float _printSpeed)
    {
        int dialogIndex = 0;
        this.dialogText.text = "";
        this.textState = TextBoxState.INPRINT;
        Debug.Log(_dialog);

        while (true)
        {
            if (_dialog[dialogIndex] == '$')
            {
                string t_methodName = "";
                dialogIndex++;
                while (true)
                {
                    t_methodName += _dialog[dialogIndex];
                    dialogIndex++;
                    if (_dialog[dialogIndex] == '$')
                    {
                        DialogMethodManager.instance.InvokeMethod(t_methodName);
                        dialogIndex++;
                        break;
                    }
                }
                if (dialogIndex >= _dialog.Length) break;
            }

            StringBuilder sb = new StringBuilder(this.dialogText.text);
            sb.Append(_dialog[dialogIndex]);
            this.dialogText.text = sb.ToString();
            dialogIndex++;
            if (dialogIndex >= _dialog.Length) break;
        }
        this.textState = TextBoxState.WAIT;


        //  Debug.Log(this.textUIManager.currentDialogIndex);


        if (this.currentDialog[this.currentIndex].isChoose)
        {
            int choiceDialog1 = int.Parse(this.currentDialog[_index].Choice1[1]);
            int choiceDialog2 = -1;
            int choiceDialog3 = -1;

            if (string.IsNullOrEmpty(this.currentDialog[_index].Choice2[0]) != true)
                choiceDialog2 = int.Parse(this.currentDialog[_index].Choice2[1]);

            if (string.IsNullOrEmpty(this.currentDialog[_index].Choice3[0]) != true)
                choiceDialog3 = int.Parse(this.currentDialog[_index].Choice3[1]);

            //this.textUIManager.EnableButtons(choiceDialog1, choiceDialog2, choiceDialog3);
            this.textState = TextBoxState.CHOOSE;
            return;
        }
    }

    //하나씩 출력하는 코루틴
    private IEnumerator PrintRoutine(string _dialog, int _index, float _printSpeed)
    {
        int dialogIndex = 0;
        this.dialogText.text = "";
        this.textState = TextBoxState.INPRINT;
        while (true)
        {
            if (_dialog[dialogIndex] == '$')
            {
                string t_methodName = "";
                dialogIndex++;
                while (true)
                {
                    t_methodName += _dialog[dialogIndex];
                    dialogIndex++;
                    if (_dialog[dialogIndex] == '$')
                    {
                        DialogMethodManager.instance.InvokeMethod(t_methodName);
                        dialogIndex++;
                        break;
                    }
                }
                if (dialogIndex >= _dialog.Length) break;
            }

            StringBuilder sb = new StringBuilder(this.dialogText.text);
            sb.Append(_dialog[dialogIndex]);
            this.dialogText.text = sb.ToString();
            yield return new WaitForSeconds(_printSpeed);
            dialogIndex++;
            if (dialogIndex >= _dialog.Length) break;
        }
        this.textState = TextBoxState.WAIT;

        if (this.currentDialog[this.currentIndex].isChoose)
        {
            int choiceDialog1 = int.Parse(this.currentDialog[_index].Choice1[1]);
            int choiceDialog2 = -1;
            int choiceDialog3 = -1;

            if (!string.IsNullOrEmpty(this.currentDialog[_index].Choice2[1]))
                choiceDialog2 = int.Parse(this.currentDialog[_index].Choice2[1]);

            if (!string.IsNullOrEmpty(this.currentDialog[_index].Choice3[0]))
                choiceDialog3 = int.Parse(this.currentDialog[_index].Choice3[1]);

            //this.textUIManager.EnableButtons(choiceDialog1, choiceDialog2, choiceDialog3);
            this.textState = TextBoxState.CHOOSE;
            yield break;
        }
    }

    #endregion
}

public class DialogTextboxData : UIData
{
    public List<Dialog> dialogs;
}