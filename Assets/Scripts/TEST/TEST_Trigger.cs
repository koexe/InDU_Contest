using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Trigger : MonoBehaviour
{
    [SerializeField] GameObject UiPrefab;

    public void ShowUIPrefab()
    {
        UIManager.instance.ShowUI(this.UiPrefab, "Text");
    }
}
