using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


//���̾�α׿��� Ư�� ���ں��� Ư�� ���ڱ��� ���� �̸����� �ϴ� �޼��带 �����Ű�� ���� �Ŵ����Դϴ�.
public class DialogMethodManager : MonoBehaviour
{
    public static DialogMethodManager instance;

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
    #endregion
}
