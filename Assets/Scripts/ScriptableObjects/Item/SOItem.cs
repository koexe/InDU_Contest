using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SOItem : ScriptableObject
{
    [Header("������ ����")]
    [SerializeField] int itemIndex;
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] bool isUsable;
    public abstract void UseItem();
}
