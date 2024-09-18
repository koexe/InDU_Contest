using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : PopUpUI
{
    [SerializeField] List<SaveItem> items;
    [Header("슬롯 들어있는 트랜스폼")]
    [SerializeField] Transform slotTransform;
    [Header("설명 영역")]
    [SerializeField] ExplainArea explainArea;
    [Header("사용 버튼")]
    [SerializeField] GameObject button;

    public ExplainArea GetExplainArea() => this.explainArea;
    public GameObject GetButton() => this.button;

    public override void Initialization(string _custom)
    {
        base.Initialization(_custom);
        this.items = SaveGameManager.instance.GetCurrentSaveData().items;
        Refresh();
        InGameManager.instance.state = InGameManager.GameState.Pause;

        return;
    }

    public void Sort()
    {
        this.items.Sort((x, y) => x.GetItemIndex().CompareTo(y.GetItemIndex()));
        Refresh();
        return;
    }

    public void Refresh()
    {
        int index = 0;
        foreach (InventoryItemSlot slot in this.slotTransform.GetComponentsInChildren<InventoryItemSlot>())
        {
            if (this.items.Count <= index)
                break;
            else
                slot.SetItem(this.items[index]);
            index++;
        }
        foreach (InventoryItemSlot slot in this.slotTransform.GetComponentsInChildren<InventoryItemSlot>())
        {
            slot.Initialization();
            slot.SetInventoryUI(this);
        }
        return;
    }

    private void OnDestroy()
    {
        var t_currentSaveData = SaveGameManager.instance.GetCurrentSaveData();
        t_currentSaveData.items = null;
        t_currentSaveData.items = this.items;
        SaveGameManager.instance.SetCurrentSaveData(t_currentSaveData);
        InGameManager.instance.state = InGameManager.GameState.InProgress;
        return;
    }
}
