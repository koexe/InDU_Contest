using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SOItem : ScriptableObject
{
    [Header("아이템 정보")]
    [SerializeField] int itemIndex;
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] bool isUsable;
    [SerializeField] Sprite itemImage;
    public abstract void UseItem();
    public Sprite GetItemImage() => this.itemImage;
    public string GetItemName() => this.itemName;
    public string GetDescription() => this.itemDescription;
    public int GetItemIndex() => this.itemIndex;
    public bool IsUsable() => this.isUsable;
}
