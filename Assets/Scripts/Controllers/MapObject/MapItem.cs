using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D coll2D;
    [Header("아이템 SO")]
    [SerializeField] SOItem item;
    [Header("이전에 먹었는지")]
    [SerializeField] bool isGeted;


    public SOItem GetSOItem() => this.item;

    public void Initialization()
    {
        if(this.isGeted == true)
        {
            this.spriteRenderer.sprite = null;
            this.coll2D.enabled = false;
        }
        else
        {
            this.spriteRenderer.sprite = this.item.GetItemImage();
            this.coll2D.enabled = true;
        }
    }

    public void GetItem()
    {
        this.spriteRenderer.sprite = null;
        this.coll2D.enabled = false;
        this.item.GetItem();
        return;
    }


    public void Reset()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        if (this.item != null)
            this.spriteRenderer.sprite = this.item.GetItemImage();
        return;
    }
}
