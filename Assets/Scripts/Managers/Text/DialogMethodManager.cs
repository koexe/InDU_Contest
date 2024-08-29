using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


//다이얼로그에서 특정 문자부터 특정 문자까지 안의 이름으로 하는 메서드를 실행시키기 위한 매니저입니다.
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

    #region 메서드 모음
    void TestMethod()
    {
        Debug.Log("TEST");
        return;
    }
    #endregion
}
