using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager instance;
    SaveData currentSaveData;
    public SaveData GetCurrentSaveData() => this.currentSaveData;
    SaveData previousSaveData { get; set; }

    private void Awake()
    {
        instance = this;
        this.Initialization();
        return;
    }
    public void Initialization()
    {
        //이후 작업 예정
        this.currentSaveData = new SaveData();
        currentSaveData.currentMap = "Map1";
        return;
    }



    public void SaveToJsonFile<T>(T data, string fileName)
    {
        // 클래스 객체를 JSON 문자열로 변환
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        // 파일 경로 생성 (유니티 프로젝트 내)
        string path = Path.Combine(Application.persistentDataPath, fileName);

        // JSON 문자열을 파일에 씀
        File.WriteAllText(path, json);

        Debug.Log($"Data saved to {path}");
    }

    public object LoadFromJson<T>(string _fileName) where T : class
    {
        string path = Path.Combine(Application.persistentDataPath, _fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            T t_result = JsonConvert.DeserializeObject<T>(json);

            Debug.Log("Data loaded successfully!");
            return t_result;
        }
        else
        {
            Debug.LogWarning("File not found!");
            return null;
        }
    }

}

[System.Serializable]
public class SaveData
{
    public string currentMap;
    public List<SOItem> items;
    
}