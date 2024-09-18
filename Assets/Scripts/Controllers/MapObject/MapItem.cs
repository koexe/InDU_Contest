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
    [SerializeField] public bool isGeted;


    public SOItem GetSOItem() => this.item;

    public void Initialization()
    {
        if(this.isGeted == true)
        {
            this.spriteRenderer.sprite = null;
            this.spriteRenderer.color = new Color(1, 1, 1, 0);
            this.coll2D.enabled = false;
        }
        else
        {
            this.spriteRenderer.sprite = this.item.GetItemImage();
            this.spriteRenderer.color = new Color(1, 1, 1, 1);
            this.coll2D.enabled = true;
        }
    }

    public void GetItem()
    {
        this.spriteRenderer.sprite = null;
        this.coll2D.enabled = false;
        this.item.GetItem();

        SaveGameManager.instance.currentSaveData.mapItems[InGameManager.instance.GetCurrentMapName()][this.transform.GetSiblingIndex()] = true;
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
