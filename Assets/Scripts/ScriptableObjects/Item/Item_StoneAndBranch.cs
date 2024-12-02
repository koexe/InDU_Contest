using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "Item/StoneAndBranch")]
public class Item_StoneAndBranch : SOItem
{
    [SerializeField] int triggerAmount;
    [SerializeField] string triggerIndex;
    public override void GetItem()
    {
        base.GetItem();
        int index1 = SaveGameManager.instance.currentSaveData.items.FindIndex(x => x.GetItemIndex() == 4);
        int index2 = SaveGameManager.instance.currentSaveData.items.FindIndex(x => x.GetItemIndex() == 5);

        bool t_isCompleted_1 = false;
        bool t_isCompleted_2 = false;

        Debug.Log($"index1:{index1} index2 :{index2}");

        if (index1 != -1)
        {
            if (SaveGameManager.instance.currentSaveData.items[index1].amount == this.triggerAmount)
            {
                t_isCompleted_1 = true;
            }
        }

        if (index2 != -1)
        {
            if (SaveGameManager.instance.currentSaveData.items[index2].amount == this.triggerAmount)
            {
                t_isCompleted_2 = true;
            }
        }

        if (t_isCompleted_2 && t_isCompleted_1)
        {
            UIManager.instance.ShowUI("DialogUI", -1, this.triggerIndex);
        }

    }
}
