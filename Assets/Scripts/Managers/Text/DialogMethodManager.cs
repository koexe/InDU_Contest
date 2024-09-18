using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


//���̾�α׿��� Ư�� ���ں��� Ư�� ���ڱ��� ���� �̸����� �ϴ� �޼��带 �����Ű�� ���� �Ŵ����Դϴ�.
public class DialogMethodManager : MonoBehaviour
{
    public static DialogMethodManager instance;
    [SerializeField] SOItem item_Berry;



    private void Awake()
    {
        instance = this;
    }

    public void InvokeMethod(string _methodName)
    {
        var method = this.GetType().GetMethod(_methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        if (method != null)
        {
            method.Invoke(this, null);
        }
        else
        {
            Debug.LogWarning($"Method '{_methodName}' not found!");
            return;
        }
    }

    #region �޼��� ����
    void TestMethod()
    {
        Debug.Log("TEST");
        return;
    }
    void ShakeScreen()
    {
        CameraController.instance.TriggerShake(0.5f);
    }

    void GetItemBerry()
    {
        Debug.Log("���� ������ ȹ��");
        this.item_Berry.GetItem();
    }

    void BackGroundToBlack()
    {
        var UI = UIManager.instance.GetUI("DialogUI");
        UI.GetComponent<TextUIManager>().BackGroundChange(true);
    }

    void BackGroundToNomal()
    {
        var UI = UIManager.instance.GetUI("DialogUI");
        UI.GetComponent<TextUIManager>().BackGroundChange(false);
    }
    void GetItemAxe()
    {

    }


    #endregion
}
