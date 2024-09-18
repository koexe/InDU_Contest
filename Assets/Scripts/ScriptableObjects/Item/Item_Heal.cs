using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "Item/»˙æ∆¿Ã≈€")]
public class Item_Heal : SOItem
{
    public override void UseItem()
    {
        base.UseItem();
        
        Debug.Log("§±§§§∑§©");


        InGameManager.instance.GetPlayerController().AddHp(1);
    }
}
