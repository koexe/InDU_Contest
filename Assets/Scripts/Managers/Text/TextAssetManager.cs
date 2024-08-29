using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextAssetManager : MonoBehaviour
{
    private static TextAssetManager _instance;
    public static TextAssetManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(TextAssetManager)) as TextAssetManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private DataManager _dataManager;
    public string _currentChapter;
    // 인스턴스에 접근하기 위한 프로퍼티

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);

        _dataManager = new DataManager();
        _dataManager.Init();
        _currentChapter = "Chapter1";
    }
    public Dictionary<int,Dialog> GetDialogList()
    {
        return _dataManager._dialogDictionary[_currentChapter];
    }
}
