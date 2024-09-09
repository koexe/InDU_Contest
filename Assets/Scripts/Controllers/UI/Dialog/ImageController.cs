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
    [SerializeField] private Image characterImage;
    [SerializeField] TextUIManager textUIManager;


    public void Initialization()
    {
        this.textUIManager.DialogClickAction -= OnDialogTextDown;
        this.textUIManager.DialogClickAction += OnDialogTextDown;
        return;
    }
    void ChangeImage(string name, int imageIndex)
    {
        if (!string.IsNullOrEmpty(name))
        {
            this.characterImage.sprite = AssetManager.Instance.CharacterImageDictionary[name][imageIndex -1];
        }
        //if (!string.IsNullOrEmpty(name))
        //{
        //    switch (position)
        //    {
        //        case Position.LEFT:
        //            Character_L.sprite = CharacterImageDictionary[name][imageIndex];
        //            break;
        //        case Position.CENTER:
        //            Character_C.sprite = CharacterImageDictionary[name][imageIndex];
        //            break;
        //        case Position.RIGHT:
        //            Character_R.sprite = CharacterImageDictionary[name][imageIndex];
        //            break;
        //    }
        //}
        //else
        //{
        //    switch (position)
        //    {
        //        case Position.LEFT:
        //            Character_L.sprite = null;
        //            break;
        //        case Position.CENTER:
        //            Character_C.sprite = null;
        //            break;
        //        case Position.RIGHT:
        //            Character_R.sprite = null;
        //            break;
        //    }
        //}
    }

    public void OnDialogTextDown()
    {
        int index = this.textUIManager.currentDialogIndex;
        if (!string.IsNullOrEmpty(this.textUIManager.currentDialogDictionary[index].Character[0]))
        {
            string name = this.textUIManager.currentDialogDictionary[index].Character[0];
            int t_characterIndex = int.Parse(this.textUIManager.currentDialogDictionary[index].Character[1]);
            ChangeImage(name, t_characterIndex);
        }
        //int index = this.textUIManager.currentDialogIndex;
        //string name_L = "";
        //string name_C = "";
        //string name_R = "";
        //int index_L = -1;
        //int index_C = -1;
        //int index_R = -1;

        //if (!string.IsNullOrEmpty(this.textUIManager.currentDialogDictionary[index].CharacterL[0]))
        //{
        //    name_L = this.textUIManager.currentDialogDictionary[index].CharacterL[0];
        //    index_L = int.Parse(this.textUIManager.currentDialogDictionary[index].CharacterL[1]);
        //}
        //if (!string.IsNullOrEmpty(this.textUIManager.currentDialogDictionary[index].CharacterC[0]))
        //{
        //    name_C = this.textUIManager.currentDialogDictionary[index].CharacterC[0];
        //    index_C = int.Parse(this.textUIManager.currentDialogDictionary[index].CharacterC[1]);
        //}
        //if (!string.IsNullOrEmpty(this.textUIManager.currentDialogDictionary[index].CharacterR[0]))
        //{
        //    name_R = this.textUIManager.currentDialogDictionary[index].CharacterR[0];
        //    index_R = int.Parse(this.textUIManager.currentDialogDictionary[index].CharacterR[1]);
        //}
        //ChangeImage(name_L, index_L, Position.LEFT);
        //ChangeImage(name_C, index_C, Position.CENTER);
        //ChangeImage(name_R, index_R, Position.RIGHT);
    }


}
