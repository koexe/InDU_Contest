using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "Item/Apple")]
public class Item_Apple : SOItem
{
    [SerializeField] int triggerAmount;
    [SerializeField] string triggerIndex;
    public override void GetItem()
    {
        base.GetItem();
        int index = SaveGameManager.instance.currentSaveData.items.FindIndex(x => x.GetItemIndex() == this.itemIndex);

        if (index != -1)
        {
            if (SaveGameManager.instance.currentSaveData.items[index].amount == this.triggerAmount)
            {
                UIManager.instance.ShowUI("DialogUI", -1, this.triggerIndex);
            }
        }
    }
}
