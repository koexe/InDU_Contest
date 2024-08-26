using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    Dictionary<string, GameObject> currentUICompnents;
    Canvas currentCanvas;
    public void SetCurerntCanvas(Canvas canvas) => this.currentCanvas = canvas;    

    private void Awake()
    {
        instance = this;
        this.currentUICompnents = new Dictionary<string, GameObject>();   
        DontDestroyOnLoad(this);
        if(this.currentCanvas == null)
        {
            this.currentCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }
        return;
    }

    public void ShowUI (GameObject _UiPrefab, string _Name, string _custom , int _layerOrder = -1 )
    {
        var t_UIObject = GameObject.Instantiate( _UiPrefab);
        t_UIObject.transform.SetParent(this.currentCanvas.transform, false);
        var t_SortingGroup = t_UIObject.transform.GetComponent<SortingGroup>();
        t_SortingGroup.sortingLayerName = "UIElements";
        //맨 앞에 두기
        if (_layerOrder == -1 )
        {
            int t_MinOrder = 9999;
            foreach (var obj in this.currentCanvas.transform.GetComponentsInChildren<SortingGroup>())
            {
                if(obj.sortingOrder < t_MinOrder)
                    t_MinOrder = obj.sortingOrder;
            }
            t_SortingGroup.sortingOrder = t_MinOrder;
        }
        else
        {
            //지정한 오더에 두기
            t_SortingGroup.sortingOrder = _layerOrder;
        }
        this.currentUICompnents.Add(_Name, t_UIObject);
        return;
    }

    public void DeleteUI(string name)
    {
        GameObject.Destroy(this.currentUICompnents[name]);
        this.currentUICompnents.Remove(name);
        return;
    }
}
