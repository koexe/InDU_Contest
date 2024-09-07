using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Table/Dialog", fileName = "DialogTable")]
public class DialogTable : ScriptableObject
{
    [SerializeField] List<DialogTableList> table;


    public Dictionary<string, Dictionary<int, bool>> GetDialogTable()
    {
        Dictionary<string, Dictionary<int, bool>> result = new Dictionary<string, Dictionary<int, bool>>();

        foreach(var t_table in table)
        {
            Dictionary<int,bool> t_dialogSave = new Dictionary<int,bool>();
            foreach(var t_dilogIndex in t_table.indexs)
            {
                t_dialogSave.Add(t_dilogIndex, false);
            }

            result.Add(t_table.chatacterName, t_dialogSave);
        }
        return result;

    }



}
[System.Serializable]
public class DialogTableList
{
    [SerializeField] public string chatacterName;
    [SerializeField] public int[] indexs;
}