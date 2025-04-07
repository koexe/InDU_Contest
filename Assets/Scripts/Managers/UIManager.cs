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
    Dictionary<string, UIBase> currentUIObjects;
    Dictionary<string, GameObject> UIPrefabs;

    [SerializeField] Canvas canvas;

    static Dictionary<Type, string> staticKeys = new Dictionary<Type, string>
{
    { typeof(UIBase), "InventoryKey" },
};

    private void Awake()
    {
        instance = this;
        this.currentUIObjects = new Dictionary<string, UIBase>();
        DontDestroyOnLoad(this);
        if (this.canvas == null)
        {
            this.canvas = this.transform.GetComponent<Canvas>();
        }
        return;
    }

    public void HideUI(string _identifier)
    {
        if (this.currentUIObjects.ContainsKey(_identifier))
        {
            this.currentUIObjects[_identifier].Hide();
        }
        else
        {
            Debug.Log("No Such Name UI");
        }
    }


    public UIBase ShowUI<T>(UIData _data) where T : UIBase
    {
        if (this.currentUIObjects.ContainsKey(_data.identifier) && !_data.isAllowMultifle)
        {
            Debug.Log("Same UI Already Added In Screen");
            this.currentUIObjects[_data.identifier].Hide();

            return this.currentUIObjects[_data.identifier];
        }

        UIBase t_UIObject = GameObject.Instantiate(UIPrefabs[staticKeys[typeof(T)]]).GetComponent<UIBase>();
        t_UIObject.transform.SetParent(this.canvas.transform, false);
        t_UIObject.sortingGroup.sortingLayerName = "UIElements";
        if (_data.order == -1)
        {
            int t_MinOrder = 9999;
            foreach (var obj in this.canvas.transform.GetComponentsInChildren<SortingGroup>())
            {
                if (obj.sortingOrder < t_MinOrder)
                    t_MinOrder = obj.sortingOrder;
            }
            t_UIObject.sortingGroup.sortingOrder = t_MinOrder;
        }
        else
        {
            t_UIObject.sortingGroup.sortingOrder = _data.order;
        }
        this.currentUIObjects.Add(_data.identifier, t_UIObject);
        t_UIObject.Initialization(_data);
        return t_UIObject;
    }

    #region Lagacy
    public UIBase ShowUI(GameObject _UiPrefab, string _Name, int _layerOrder = -1, string _custom = "")
    {
        if (this.currentUIObjects.ContainsKey(_Name))
        {
            Debug.Log("Same UI Already Added In Screen");
            Destroy(this.currentUIObjects[_Name].gameObject);
            this.currentUIObjects.Remove(_Name);
            return null;
        }

        UIBase t_UIObject = GameObject.Instantiate(_UiPrefab).GetComponent<UIBase>();
        t_UIObject.transform.SetParent(this.canvas.transform, false);
        var t_Ui = t_UIObject.transform.GetComponent<PopUpUI>();
        t_Ui.sortingGroup.sortingLayerName = "UIElements";
        //�� �տ� �α�
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
            //������ ������ �α�
            t_Ui.sortingGroup.sortingOrder = _layerOrder;
        }
        this.currentUIObjects.Add(_Name, t_UIObject);
        t_Ui.Initialization(_custom);
        return t_UIObject;
    }

    public UIBase ShowUI(string _UiPrefabName, int _layerOrder = -1, string _custom = "")
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
        UIBase t_UIObject = GameObject.Instantiate(_UiPrefab).GetComponent<UIBase>();
        t_UIObject.transform.SetParent(this.canvas.transform, false);
        var t_Ui = t_UIObject.transform.GetComponent<PopUpUI>();
        t_Ui.sortingGroup.sortingLayerName = "UIElements";

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
            t_Ui.sortingGroup.sortingOrder = _layerOrder;
        }
        this.currentUIObjects.Add(_Name, t_UIObject);
        t_Ui.Initialization(_custom);
        return t_UIObject;
    }

    public UIBase GetUI(string name)
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
#endregion
}

