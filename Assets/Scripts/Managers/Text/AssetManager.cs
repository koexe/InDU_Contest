using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    private static AssetManager _instance;
    public static AssetManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(AssetManager)) as AssetManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private DataManager _dataManager;
    public string _currentChapter;

    public Dictionary<string, Sprite[]> CharacterImageDictionary = new Dictionary<string, Sprite[]>();
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
        SetImageDictionary("Hunter001");
        SetImageDictionary("Mom001");
        SetImageDictionary("RedHood001");
        SetImageDictionary("Wolf 001");
        _currentChapter = "Chapter1";
    }
    public Dictionary<int,Dialog> GetDialogList()
    {
        return _dataManager._dialogDictionary[_currentChapter];
    }

    void SetImageDictionary(string folderPath)
    {
        string imagesFolderPath = "Sprites/Characters/" + folderPath;

        // Resources 폴더에서 모든 스프라이트를 불러옵니다.
        Sprite[] sprites = Resources.LoadAll<Sprite>(imagesFolderPath);
        

        // 스프라이트 배열을 순회하면서 이름을 키로 사용하여 딕셔너리에 추가합니다.
        foreach (Sprite sprite in sprites)
        {
            string name_Temp = sprite.name;
            if (!this.CharacterImageDictionary.ContainsKey(folderPath))
            {
                this.CharacterImageDictionary.Add(folderPath, new Sprite[] { sprite });
            }
            else
            {
                // 같은 이름을 가진 스프라이트가 있으면 배열에 추가
                List<Sprite> spriteList = new List<Sprite>(this.CharacterImageDictionary[folderPath]);
                spriteList.Add(sprite);
                this.CharacterImageDictionary[folderPath] = spriteList.ToArray();
            }
        }
    }
}
