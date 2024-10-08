using System.Collections;
using System.Collections.Generic;
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
    Dictionary<int,Dialog> currentDialogDictionary;
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
                if(currentIndex != 0)
                {
                    if (!string.IsNullOrEmpty(this.currentDialogDictionary[currentIndex].linkDilog))
                        nextDialog = int.Parse(this.currentDialogDictionary[currentIndex].linkDilog);
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
                    Debug.Log("Next Dialog Missing in Dialog id: " + this.textUIManager.currentDialogIndex);
                break;
            case TextState.INPRINT:
                StopCoroutine(this.printCoroutine);
                ChangeDialog(this.textUIManager.currentDialogIndex);
                this.textUIManager.textState =TextState.WAIT;
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
        var dialogTemp = currentDialogDictionary[index];
        this.dialogText.text = dialogTemp.comment;
        if (this.currentDialogDictionary[index].isChoose)
        {
            int choiceDialog1 = int.Parse(this.currentDialogDictionary[index].Choice1[1]);
            int choiceDialog2 = int.Parse(this.currentDialogDictionary[index].Choice2[1]);
            int choiceDialog3 = int.Parse(this.currentDialogDictionary[index].Choice3[1]);
            this.textUIManager.EnableButtons(choiceDialog1, choiceDialog2, choiceDialog3);
        }
    }
    //다이얼로그 하나씩 출력하기
    public void ChangeDialogOneByOne(int index)
    {
        var dialogTemp = this.currentDialogDictionary[index];
        switch (dialogTemp.talkNameIdx)
        {
            case 1:
                this.nameText.text = dialogTemp.CharacterL[0];
                break;
            case 2:
                this.nameText.text = dialogTemp.CharacterC[0];
                break;
            case 3:
                this.nameText.text = dialogTemp.CharacterR[0];
                break;
        }
        this.printCoroutine = StartCoroutine(PrintRoutine(dialogTemp.comment,index));
    }
    //하나씩 출력하는 코루틴
    private IEnumerator PrintRoutine(string dialog,int index)
    {
        int dialogIndex = 0;
        this.dialogText.text = "";
        this.textUIManager.textState = TextState.INPRINT;
        while (true)
        {
            this.dialogText.text += dialog[dialogIndex];
            yield return new WaitForSeconds(this.printSpeed);
            dialogIndex++;
            if (dialogIndex >= dialog.Length) break;
        }
        this.textUIManager.textState = TextState.WAIT;
        if (this.currentDialogDictionary[this.textUIManager.currentDialogIndex].isChoose)
        {
            int choiceDialog1 = int.Parse(currentDialogDictionary[index].Choice1[1]);
            int choiceDialog2 = int.Parse(currentDialogDictionary[index].Choice2[1]);
            int choiceDialog3 = int.Parse(currentDialogDictionary[index].Choice3[1]);
            this.textUIManager.EnableButtons(choiceDialog1, choiceDialog2, choiceDialog3);
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
            switch (dialogTemp.talkNameIdx)
            {
                case 1:
                    this.nameText.text = dialogTemp.CharacterL[0];
                    break;
                case 2:
                    this.nameText.text = dialogTemp.CharacterC[0];
                    break;
                case 3:
                    this.nameText.text = dialogTemp.CharacterR[0];
                    break;
            }
            this.printCoroutine = StartCoroutine(PrintRoutine(dialogTemp.comment, index));
            this.textUIManager.choiceButtonController.gameObject.SetActive(false);
            this.textUIManager.textState = TextState.WAIT;
        }
        else
        {
            Debug.Log("Dialog index Missing.");
        }
    }
    #endregion
}