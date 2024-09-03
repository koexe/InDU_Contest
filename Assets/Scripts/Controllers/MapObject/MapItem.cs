using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour
{
    [Header("������Ʈ")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D coll2D;
    [Header("������ SO")]
    [SerializeField] SOItem item;
    [Header("������ �Ծ�����")]
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
