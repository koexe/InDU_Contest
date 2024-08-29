using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//모든 UI가 상속받아야 합니다.
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
