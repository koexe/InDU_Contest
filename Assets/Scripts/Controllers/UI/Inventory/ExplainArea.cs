using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExplainArea : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("이미지와 텍스트들")]
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI indexText;

    public void ChageExplainArea(SaveItem _Item)
    {
        this.image.sprite = _Item.GetItemImage();
        this.descriptionText.text = _Item.GetDescription();
        this.nameText.text = _Item.GetItemName();
        this.indexText.text = _Item.GetItemIndex().ToString();
        return;
    }
}
