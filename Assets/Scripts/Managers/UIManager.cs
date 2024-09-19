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
    // 직렬화 -> 굉장히 어려운 개념이다. 
    // 그래서 직렬화가 뭐임?
    // 에디터상에서 우리가 수정은 하는데, 실제 스크립트에서는 숨기고싶다.

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

    public GameObject ShowUI (GameObject _UiPrefab, string _Name, int _layerOrder = -1, string _custom = "")
    {
        if(this.currentUIObjects.ContainsKey(_Name))
        {
            Debug.Log("Same UI Already Added In Screen");
            Destroy(this.currentUIObjects[_Name].gameObject);
            this.currentUIObjects.Remove(_Name);
            return null;
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
        t_Ui.Initialization(_custom);
        return t_UIObject;
    }

    public GameObject ShowUI(string _UiPrefabName, int _layerOrder = -1, string _custom = "")
    {
        var _UiPrefab = Resources.Load<GameObject>($"Prefabs/UI/{_UiPrefabName}");
        Debug.Log(_custom);

        string _Name = _UiPrefab.transform.GetComponent<PopUpUI>().GetUiName();

        if (this.currentUIObjects.ContainsKey(_Name))
        {
            Debug.Log("Same UI Already Added In Screen");
            Destroy(this.currentUIObjects[_Name].gameObject);
            this.currentUIObjects.Remove(_Name);
            return null;
        }
        var t_UIObject = GameObject.Instantiate(_UiPrefab);
        t_UIObject.transform.SetParent(this.canvas.transform, false);
        var t_Ui = t_UIObject.transform.GetComponent<PopUpUI>();
        t_Ui.sortingGroup.sortingLayerName = "UIElements";
        //맨 앞에 두기
        if (_layerOrder == -1)
        {
            int t_MinOrder = 9999;
            foreach (var obj in this.canvas.transform.GetComponentsInChildren<SortingGroup>())
            {
                if (obj.sortingOrder < t_MinOrder)
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
        t_Ui.Initialization(_custom);
        return t_UIObject;
    }

    public GameObject GetUI(string name)
    {
        if (this.currentUIObjects.ContainsKey(name))
        {
            return this.currentUIObjects[name];
        }
        else
        {
            Debug.Log("No Such Name UI");
            return null;
        }
    }

    public void DeleteUI(string name)
    {
        if (this.currentUIObjects.ContainsKey(name))
        {
            GameObject.Destroy(this.currentUIObjects[name]);
            this.currentUIObjects.Remove(name);
        }
        else
        {
            Debug.Log("Already Deleted UI");
        }

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
