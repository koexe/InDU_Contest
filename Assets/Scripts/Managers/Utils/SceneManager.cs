using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager instance;
    static string loadSceneName = "LoadingUI";
    
    private void Awake()
    {
        instance = this;
        SceneLoadManager.DontDestroyOnLoad(this);
        return;
    }
    public void LoadScene_Async(GameObject _LoadingUI,string _Scenename)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeScene_Async(_LoadingUI,_Scenename));
        return;
    }


    IEnumerator ChangeScene_Async(GameObject _LoadingUI, string _SceneName)
    {
        AsyncOperation t_asyncOper = SceneManager.LoadSceneAsync(_SceneName, LoadSceneMode.Single);
        t_asyncOper.allowSceneActivation = false;
        UIManager.instance.ShowUI(_LoadingUI, loadSceneName, -1);
        while (!t_asyncOper.isDone)
        {
            // �ε��� ���� �Ϸ�Ǿ����� Ȯ�� (progress�� 0.0f ~ 0.9f ����)
            if (t_asyncOper.progress >= 0.9f)
            {
                // �߰����� �ε� UI�� ó���� �� ���� (�ʿ��)
                // ����ڰ� Ư�� ��ư�� �����ų�, Ư�� ������ ������ �� ���� Ȱ��ȭ�� �� ����

                // ���⿡ ��� �ð��� �߰��ص� ��
                yield return new WaitForSeconds(1.5f);

                // allowSceneActivation�� true�� �����Ͽ� �� Ȱ��ȭ
                t_asyncOper.allowSceneActivation = true;
            }
            yield return null; // �� �����Ӹ��� ���
        }
        UIManager.instance.DeleteUI(loadSceneName);

        yield break;
    }
}
