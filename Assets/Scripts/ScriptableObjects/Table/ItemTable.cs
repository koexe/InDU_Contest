using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Table/Item", fileName = "ItemTable")]
public class ItemTable : ScriptableObject
{
    [SerializeField] public List<ItemTableContains> itemTable;
}


[System.Serializable]
public class ItemTableContains
{
    [SerializeField] public int itemIndex;
    [SerializeField] public SOItem item;
}
