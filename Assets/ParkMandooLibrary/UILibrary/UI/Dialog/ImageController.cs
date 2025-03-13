using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
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
            this.characterImage.color = new Color(1, 1, 1, 1);
            this.characterImage.sprite = AssetManager.Instance.CharacterImageDictionary[name][imageIndex -1];
        }
        else
        {
            this.characterImage.color = new Color(1, 1, 1, 0);
            this.characterImage.sprite = null;
        }

    }

    public void OnDialogTextDown()
    {
        int index = this.textUIManager.currentDialogIndex;
        int t_characterIndex = -1;
        string t_name = null;
        if (!string.IsNullOrEmpty(this.textUIManager.currentDialogDictionary[index].Character[0]))
        {
            t_name = this.textUIManager.currentDialogDictionary[index].Character[0];
            t_characterIndex = int.Parse(this.textUIManager.currentDialogDictionary[index].Character[1]);
        }
        ChangeImage(t_name, t_characterIndex);
    }


}
