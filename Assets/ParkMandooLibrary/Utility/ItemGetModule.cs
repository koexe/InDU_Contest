using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetModule : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] Collider2D coll2D;
    [Header("아이템 획득")]
    [SerializeField] LayerMask itemGetMask;
    [SerializeField] float itemGetDelay;
    [SerializeField] float currentItemGetDelay;


    private void Start()
    {
        SettingKeyboard();
    }
    private void FixedUpdate()
    {
        UpdateGetItemDelay();
        return;
    }

    void SettingKeyboard()
    {
        IngameInputManager.instance.AddKeyboardAction(KeyCode.Space, () => GetItem());
        return;
    }



    #region 아이템 획득
    public void GetItem()
    {
        if (this.currentItemGetDelay != 0f) return;
        var items = Physics2D.OverlapBoxAll(this.coll2D.bounds.center, this.coll2D.bounds.size, 0f, this.itemGetMask);
        foreach (var item in items)
        {
            var t_MapItem = item.GetComponent<MapItem>();
            t_MapItem.GetItem();

        }
        return;
    }
    void UpdateGetItemDelay()
    {
        if (this.currentItemGetDelay != 0f)
        {
            return;
        }
        else
        {
            this.currentItemGetDelay = Mathf.MoveTowards(this.currentItemGetDelay, 0f, Time.fixedDeltaTime);
        }
    }
    #endregion
}
