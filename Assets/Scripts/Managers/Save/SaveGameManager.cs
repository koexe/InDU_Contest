using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager instance;
    public SaveData currentSaveData;
    public SaveData GetCurrentSaveData() => this.currentSaveData;

    public void SetCurrentSaveData(SaveData _data) => this.currentSaveData = _data; 
    SaveData previousSaveData { get; set; }

    private void Awake()
    {
        instance = this;
        this.Initialization();
        DontDestroyOnLoad(this);
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
    public List<SaveItem> items;
    public SaveData() 
    {
        this.items = new List<SaveItem>();
    }
}
[System.Serializable]
public class SaveItem
{
    SOItem item;
    public int amount;

    public int GetItemIndex() => item.GetItemIndex();
    public Sprite GetItemImage() => item.GetItemImage();
    public string GetItemName() => item.GetItemName();
    public string GetDescription() => item.GetItemDescription();
   
    public SaveItem(SOItem item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}