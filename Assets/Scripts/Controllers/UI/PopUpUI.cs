using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//��� UI�� ��ӹ޾ƾ� �մϴ�.
public class PopUpUI : MonoBehaviour
{
    public SortingGroup sortingGroup;
    public virtual void Initialization()
    {
        return;
    }
    private void Reset()
    {
        this.sortingGroup = this.transform.GetComponent<SortingGroup>();
        return;
    }
}
