using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
enum Position
{
    LEFT,
    CENTER,
    RIGHT
}

public class ImageController : MonoBehaviour
{
    [SerializeField] private Image Character_L;
    [SerializeField] private Image Character_C; 
    [SerializeField] private Image Character_R;
    [SerializeField] TextUIManager textUIManager;


    public Dictionary<string, Sprite[]> CharacterImageDIctionary = new Dictionary<string, Sprite[]>();
    public void Initialization()
    {
        SetImageDictionary();
        this.textUIManager.DialogClickAction -= OnDialogTextDown;
        this.textUIManager.DialogClickAction += OnDialogTextDown;
    }
    void ChangeImage(string name,int imageIndex, Position position)
    {
        if (!string.IsNullOrEmpty(name))
        {
            switch (position)
            {
                case Position.LEFT:
                    Character_L.sprite = CharacterImageDIctionary[name][imageIndex];
                    break;
                case Position.CENTER:
                    Character_C.sprite = CharacterImageDIctionary[name][imageIndex];
                    break;
                case Position.RIGHT:
                    Character_R.sprite = CharacterImageDIctionary[name][imageIndex];
                    break;
            }
        }
        else
        {
            switch (position)
            {
                case Position.LEFT:
                    Character_L.sprite = null;
                    break;
                case Position.CENTER:
                    Character_C.sprite = null;
                    break;
                case Position.RIGHT:
                    Character_R.sprite = null;
                    break;
            }
        }
    }

    public void OnDialogTextDown()
    {
        int index = this.textUIManager.currentDialogIndex;
        string name_L = "";
        string name_C = "";
        string name_R = "";
        int index_L = -1;
        int index_C = -1;
        int index_R = -1;

        if (!string.IsNullOrEmpty(this.textUIManager.currentDialogDictionary[index].CharacterL[0]))
        {
            name_L = this.textUIManager.currentDialogDictionary[index].CharacterL[0];
            index_L = int.Parse(this.textUIManager.currentDialogDictionary[index].CharacterL[1]);
        }
        if (!string.IsNullOrEmpty(this.textUIManager.currentDialogDictionary[index].CharacterC[0]))
        {
            name_C = this.textUIManager.currentDialogDictionary[index].CharacterC[0];
            index_C = int.Parse(this.textUIManager.currentDialogDictionary[index].CharacterC[1]);
        }
        if (!string.IsNullOrEmpty(this.textUIManager.currentDialogDictionary[index].CharacterR[0]))
        {
            name_R = this.textUIManager.currentDialogDictionary[index].CharacterR[0];
            index_R = int.Parse(this.textUIManager.currentDialogDictionary[index].CharacterR[1]);
        }
        ChangeImage(name_L, index_L, Position.LEFT);
        ChangeImage(name_C, index_C, Position.CENTER);
        ChangeImage(name_R, index_R, Position.RIGHT);
    }

    void SetImageDictionary()
    {
        string basePath = "Assets/Resources/";
        string imagesFolderPath = "Sprites/Characters";
       
        DirectoryInfo di = new DirectoryInfo(basePath + imagesFolderPath);
        foreach (FileInfo file in di.GetFiles())
        {
            string name_Temp = Path.GetFileNameWithoutExtension(file.Name);
            Sprite[] sprites = Resources.LoadAll<Sprite>(imagesFolderPath + "/" + name_Temp);
            CharacterImageDIctionary.Add(name_Temp, sprites);
        }
    }
}
