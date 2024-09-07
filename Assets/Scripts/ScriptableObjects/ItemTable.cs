using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Table/Item", fileName = "ItemTable")]
public class ItemTable : ScriptableObject
{
    [SerializeField] List<ItemTableContains> itemTable;
}


[System.Serializable]
public class ItemTableContains
{
    [SerializeField] public string itemName;
    [SerializeField] public SOItem item;
}
