using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public enum TextState
{
    WAIT,
    INPRINT,
    CHOOSE,
    PAUSE
}
public class DialogTextController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextUIManager textUIManager;
    Dictionary<int, Dialog> currentDialogDictionary;
    Coroutine printCoroutine;
    public float printSpeed = 1f;

    public void Initialization()
    {
        this.textUIManager.DialogClickAction -= OnDialogTextDown;
        this.textUIManager.DialogClickAction += OnDialogTextDown;
        this.currentDialogDictionary = this.textUIManager.currentDialogDictionary;


        return;
    }
    #region 텍스트 변경 기능
    //텍스트 영역을 눌렀을 때 호출되는 함수
    public void OnDialogTextDown()
    {
        //현재 텍스트 진행 상황에 따른 처리
        switch (this.textUIManager.textState)
        {
            case TextState.WAIT:
                int nextDialog = 0;
                int currentIndex = this.textUIManager.currentDialogIndex;

                //연결된 텍스트가 있다면 해당 인덱스로 넘어감
                if (currentIndex != 0)
                {
                    if (!string.IsNullOrEmpty(this.currentDialogDictionary[currentIndex].linkDilog) &&
                         !string.IsNullOrEmpty(this.currentDialogDictionary[currentIndex].linkCondition[0]))
                    {
                        if (SaveGameManager.instance.currentSaveData.chatacterDialogs
                            [int.Parse(this.currentDialogDictionary[currentIndex].linkCondition[0])] ==
                            bool.Parse(this.currentDialogDictionary[currentIndex].linkCondition[1]))
                        {
                            nextDialog = int.Parse(this.currentDialogDictionary[currentIndex].linkDilog);

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
                            }
                        }
                        else
                        {
                            this.textUIManager.DeleteUI();
                        }
                    }
                    //없다면 바로 다음 인덱스로 넘어감
                    else
                        nextDialog = currentIndex + 1;
                }
                else
                    nextDialog = 1;



                //현재 다이얼로그 목록에 해당 인덱스가 있는지 검사
                if (this.currentDialogDictionary.ContainsKey(nextDialog))
                {
                    textUIManager.currentDialogIndex = nextDialog;
                    ChangeDialogOneByOne(nextDialog);
                }
                else
                {
                    Debug.Log("Next Dialog Missing in Dialog id: " + this.textUIManager.currentDialogIndex);
                    this.textUIManager.DeleteUI();
                }
                break;
            case TextState.INPRINT:
                StopCoroutine(this.printCoroutine);
                ChangeDialog(this.textUIManager.currentDialogIndex);
                break;
            case TextState.CHOOSE:
                break;
            case TextState.PAUSE:
                break;
        }
    }
    //다이얼로그 한번에 바꾸기
    public void ChangeDialog(int index)
    {
        var dialogTemp = this.currentDialogDictionary[index];
        this.nameText.text = CodeToName(dialogTemp.Character[0]);

        PrintAll(dialogTemp.comment, index, 0f);
        return;
    }
    //다이얼로그 하나씩 출력하기
    public void ChangeDialogOneByOne(int index)
    {
        var dialogTemp = this.currentDialogDictionary[index];
        this.nameText.text = CodeToName(dialogTemp.Character[0]);
        this.printCoroutine = StartCoroutine(PrintRoutine(dialogTemp.comment, index, this.printSpeed));
    }

    public void PrintAll(string _dialog, int _index, float _printSpeed)
    {
        int dialogIndex = 0;
        this.dialogText.text = "";
        this.textUIManager.textState = TextState.INPRINT;
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
        this.textUIManager.textState = TextState.WAIT;




        if (this.currentDialogDictionary[this.textUIManager.currentDialogIndex].isChoose)
        {
            int choiceDialog1 = int.Parse(currentDialogDictionary[_index].Choice1[1]);
            int choiceDialog2 = -1;
            int choiceDialog3 = -1;

            if (string.IsNullOrEmpty(currentDialogDictionary[_index].Choice2[0]) != true)
                choiceDialog2 = int.Parse(currentDialogDictionary[_index].Choice2[1]);

            if (string.IsNullOrEmpty(currentDialogDictionary[_index].Choice3[0]) != true)
                choiceDialog3 = int.Parse(currentDialogDictionary[_index].Choice3[1]);

            this.textUIManager.EnableButtons(choiceDialog1, choiceDialog2, choiceDialog3);
            this.textUIManager.textState = TextState.CHOOSE;
            return;
        }
    }




    //하나씩 출력하는 코루틴
    private IEnumerator PrintRoutine(string _dialog, int _index, float _printSpeed)
    {
        int dialogIndex = 0;
        this.dialogText.text = "";
        this.textUIManager.textState = TextState.INPRINT;
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
        this.textUIManager.textState = TextState.WAIT;

        if (this.currentDialogDictionary[this.textUIManager.currentDialogIndex].isChoose)
        {
            int choiceDialog1 = int.Parse(currentDialogDictionary[_index].Choice1[1]);
            int choiceDialog2 = -1;
            int choiceDialog3 = -1;

            if (!string.IsNullOrEmpty(currentDialogDictionary[_index].Choice2[1]))
                choiceDialog2 = int.Parse(currentDialogDictionary[_index].Choice2[1]);

            if (!string.IsNullOrEmpty(currentDialogDictionary[_index].Choice3[0]))
                choiceDialog3 = int.Parse(currentDialogDictionary[_index].Choice3[1]);

            this.textUIManager.EnableButtons(choiceDialog1, choiceDialog2, choiceDialog3);
            this.textUIManager.textState = TextState.CHOOSE;
            yield break;
        }
    }
    #endregion
    #region 버튼에 들어갈 함수
    //선택지 인덱스에 따른 버튼 동작
    public void ChangeDialogButton(int index)
    {
        if (this.currentDialogDictionary.ContainsKey(index))
        {
            this.textUIManager.currentDialogIndex = index;
            var dialogTemp = this.currentDialogDictionary[index];
            this.nameText.text = CodeToName(this.currentDialogDictionary[index].Character[0]);
            this.printCoroutine = StartCoroutine(PrintRoutine(dialogTemp.comment, index, this.printSpeed));
            this.textUIManager.choiceButtonController.gameObject.SetActive(false);

        }
        else
        {
            Debug.Log("Dialog index Missing.");
        }
    }
    #endregion


    string CodeToName(string _code)
    {
        switch (_code)
        {
            case "RedHood001":
                return "빨간모자";
            case "Mom001":
                return "엄마";
            case "Hunter001":
                return "사냥꾼";
            case "Wolf 001":
                return "늑대";
            default:
                return "";
        }
    }


}