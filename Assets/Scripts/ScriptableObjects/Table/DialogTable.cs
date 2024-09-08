using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Table/Dialog", fileName = "DialogTable")]
public class DialogTable : ScriptableObject
{
    [SerializeField] List<DialogTableList> table;


    public Dictionary<int, bool> GetDialogTable()
    {
        Dictionary<int, bool> t_result = new Dictionary<int, bool>();

        foreach(var t_table in table)
        {
            foreach(var t_dilogIndex in t_table.indexs)
            {
                t_result.Add(t_dilogIndex, false);
            }

        }
        return t_result;
    }



}
[System.Serializable]
public class DialogTableList
{
    [SerializeField] public string chatacterName;
    [SerializeField] public int[] indexs;
}