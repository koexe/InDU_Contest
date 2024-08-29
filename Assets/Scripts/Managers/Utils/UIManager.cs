using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    Dictionary<string, GameObject> currentUIObjects;
    [SerializeField] Canvas canvas;


    private void Awake()
    {
        instance = this;
        this.currentUIObjects = new Dictionary<string, GameObject>();   
        DontDestroyOnLoad(this);
        if(this.canvas == null)
        {
            this.canvas =this.transform.GetComponent<Canvas>();
        }
        return;
    }

    public void ShowUI (GameObject _UiPrefab, string _Name, int _layerOrder = -1, string _custom = "")
    {
        if(this.currentUIObjects.ContainsKey(_Name))
        {
            Debug.Log("Same UI Already Added In Screen");
            Destroy(this.currentUIObjects[_Name].gameObject);
            this.currentUIObjects.Remove(_Name);
        }



        var t_UIObject = GameObject.Instantiate( _UiPrefab);
        t_UIObject.transform.SetParent(this.canvas.transform, false);
        var t_Ui = t_UIObject.transform.GetComponent<PopUpUI>();
        t_Ui.sortingGroup.sortingLayerName = "UIElements";
        //맨 앞에 두기
        if (_layerOrder == -1 )
        {
            int t_MinOrder = 9999;
            foreach (var obj in this.canvas.transform.GetComponentsInChildren<SortingGroup>())
            {
                if(obj.sortingOrder < t_MinOrder)
                    t_MinOrder = obj.sortingOrder;
            }
            t_Ui.sortingGroup.sortingOrder = t_MinOrder;
        }
        else
        {
            //지정한 오더에 두기
            t_Ui.sortingGroup.sortingOrder = _layerOrder;
        }
        this.currentUIObjects.Add(_Name, t_UIObject);
        return;
    }

    public void DeleteUI(string name)
    {
        GameObject.Destroy(this.currentUIObjects[name]);
        this.currentUIObjects.Remove(name);
        return;
    }

    public void DeleteUI_ALL()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
        this.currentUIObjects.Clear();
        return;
    }
}
