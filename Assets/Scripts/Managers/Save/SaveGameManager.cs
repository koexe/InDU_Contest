using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager instance;
    const string fileName = "SaveData";

    [Header("����Ǿ��ִ� ���̺�")]
    SaveData saveInFile;
    [Header("���� ���̺�")]
    public SaveData currentSaveData;
    public SaveData GetCurrentSaveData() => this.currentSaveData;
    public void SetCurrentSaveData(SaveData _data) => this.currentSaveData = _data;

    [SerializeField] DialogTable dialogTable;
    [SerializeField] List<GameObject> MapPrefab;
    [SerializeField] ItemTable itemTable;

    public bool isSaveDebug;


    SaveData previousSaveData { get; set; }

    private void Awake()
    {
        instance = this;
        this.Initialization();
        DontDestroyOnLoad(this);
        Debug.Log("���̺� �ʱ�ȭ �Ϸ�");
        return;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SavetoFile();
        }
    }
#endif
    void SavetoFile()
    {
        foreach (var item in this.currentSaveData.items)
        {
            this.currentSaveData.itemNames.Add(new SaveItemMinimal(item.GetItemIndex(), item.amount));
        }
        SaveToJsonFile<SaveData>(this.currentSaveData, fileName);
    }

    void LoadToFile()
    {
        this.saveInFile = LoadFromJson<SaveData>(fileName);
        this.saveInFile.items.Clear();
        this.currentSaveData = this.saveInFile;

        foreach (var item in this.currentSaveData.itemNames)
        {
            Debug.Log(item.index);

            int index = this.itemTable.itemTable.FindIndex(x => x.itemIndex == item.index);
            Debug.Log(index);
            Debug.Log(this.itemTable.itemTable[index].itemIndex);
            Debug.Log(this.itemTable.itemTable[index].item.GetItemImage().name);
            this.currentSaveData.items.Add(new SaveItem(Instantiate(this.itemTable.itemTable[index].item), item.amount));
        }
    }


    public void Initialization()
    {
        if (isSaveDebug == true)
        {
            this.saveInFile = new SaveData();
            this.saveInFile.currentMap = "Map1";
            this.saveInFile.chatacterDialogs = new Dictionary<int, bool>();
            this.saveInFile.mapItems = new Dictionary<string, List<bool>>();
            this.currentSaveData = this.saveInFile;
            CheckMapItem();
        }
        else
        {
            LoadToFile();
            //this.saveInFile = LoadFromJson<SaveData>(fileName);
            //this.currentSaveData = this.saveInFile;

        }
        //���� �۾� ����

        return;
    }

    public void CheckMapItem()
    {
        foreach (var map in MapPrefab)
        {
            var Items = map.transform.GetComponent<MapOptions>().GetMapItems();
            this.currentSaveData.mapItems.Add(map.name, new List<bool>());
            foreach (var item in Items)
            {
                this.currentSaveData.mapItems[map.name].Add(item.isGeted);
            }
        }
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

    public T LoadFromJson<T>(string _fileName) where T : class
    {
        string path = Path.Combine(Application.persistentDataPath, _fileName);
        Debug.Log(path);
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
    public Dictionary<int, bool> chatacterDialogs;
    public List<SaveItemMinimal> itemNames;

    public Dictionary<string, List<bool>> mapItems;
    public SaveData()
    {
        this.items = new List<SaveItem>();
        this.itemNames = new List<SaveItemMinimal>();
        this.chatacterDialogs = new Dictionary<int, bool>();
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

    public SOItem GetSOItem() => item;

    public void SetItem(SOItem item) => this.item = item;
    public void SetAmount(int amount) => this.amount = amount;

    public SaveItem(SOItem _item, int _amount)
    {
        this.item = _item;
        this.amount = _amount;
        if (_item == null)
            Debug.Log("������ ��");
    }


}
[System.Serializable]
public class SaveItemMinimal
{
    public int index;
    public int amount;

    public SaveItemMinimal(int _index, int _amount)
    {
        this.index = _index;
        this.amount = _amount;
    }
}
