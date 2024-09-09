using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private string _chapter1;
    public Dictionary<string, Dictionary<int, Dialog>> _dialogDictionary = new Dictionary<string, Dictionary<int, Dialog>>();


    public void Init()
    {
        _chapter1 = "Chapter1";
        Dictionary<int, Dialog> dialogList_Chapter1 = CSVReader.Read("TextAssets/Chapter1/" + _chapter1);
        _dialogDictionary.Add(_chapter1, dialogList_Chapter1);

        foreach (var t_dialog in dialogList_Chapter1)
        {
            if(!SaveGameManager.instance.currentSaveData.chatacterDialogs.ContainsKey(t_dialog.Key))
            {
                SaveGameManager.instance.currentSaveData.chatacterDialogs.Add(t_dialog.Key, false);
            }
            else
            {
                Debug.Log(t_dialog.Key);
            }

        }
    }

}
