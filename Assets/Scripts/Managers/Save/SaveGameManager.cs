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
        //���� �۾� ����
        this.currentSaveData = new SaveData();
        currentSaveData.currentMap = "Map1";
        return;
    }



    public void SaveToJsonFile<T>(T data, string fileName)
    {
        // Ŭ���� ��ü�� JSON ���ڿ��� ��ȯ
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        // ���� ��� ���� (����Ƽ ������Ʈ ��)
        string path = Path.Combine(Application.persistentDataPath, fileName);

        // JSON ���ڿ��� ���Ͽ� ��
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