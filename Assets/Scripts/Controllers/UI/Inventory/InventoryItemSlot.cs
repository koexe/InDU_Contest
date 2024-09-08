using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{
    SaveItem item;
    public int amount;
    [SerializeField] Image image;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] EventTrigger eventSystem;
    public void SetInventoryUI (InventoryUI _inventoryUI) => this.inventoryUI = _inventoryUI;

    public void SetItem(SaveItem item) => this.item = item;

    public void Initialization()
    {
        if (this.item == null) return;
        this.image.sprite = this.item.GetItemImage();
        return;
    }

    public void OnPointerEnter()
    {
        if(this.item == null) return;
        this.inventoryUI.GetExplainArea().ChageExplainArea(this.item);
        return;
    }
}
 