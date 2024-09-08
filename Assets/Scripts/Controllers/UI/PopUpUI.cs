using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//��� UI�� ��ӹ޾ƾ� �մϴ�.
public class PopUpUI : MonoBehaviour
{
    [Header("UI �ʼ� ������Ʈ")]
    public SortingGroup sortingGroup;
    [Header("UI �̸�")]
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
