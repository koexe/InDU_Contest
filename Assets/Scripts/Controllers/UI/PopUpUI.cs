using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//모든 UI가 상속받아야 합니다.
public class PopUpUI : MonoBehaviour
{
    [Header("UI 필수 컴포넌트")]
    public SortingGroup sortingGroup;
    [Header("UI 이름")]
    [SerializeField] string uiName;
    public virtual void Initialization(string _custom = "")
    {
        return;
    }
    public virtual void DeleteUI()
    {
        UIManager.instance.DeleteUI(this.uiName);
        return;
    }

    private void Reset()
    {
        this.sortingGroup = this.transform.GetComponent<SortingGroup>();
        return;
    }
}
