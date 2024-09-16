using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossTrap : NPCController
{
    [Space(12)]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D coll;
    [SerializeField] float notInteractDestoryTime;
    [SerializeField] float autoDestroyTime;
    [SerializeField] float currentInterval;

    public override void Initialization()
    {
        base.Initialization();
        Color t_color = this.spriteRenderer.color;
        t_color.a = 0.5f;
        this.spriteRenderer.color = t_color;
        this.currentInterval = this.notInteractDestoryTime;
        this.coll.enabled = false;
    }
    public override void Start()
    {
        Initialization();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckDestroyTime();
    }

    void CheckDestroyTime()
    {
        if (this.currentInterval == 0f)
        {
            //Destroy(this.gameObject);
        }
        else
        {
            this.currentInterval = Mathf.MoveTowards(this.currentInterval, 0f, Time.fixedDeltaTime);
        }
    }

    public override void InteractAction()
    {
        base.InteractAction();
        Color t_color = this.spriteRenderer.color;
        t_color.a = 1f;
        this.spriteRenderer.color = t_color;
        this.coll.enabled = true;
        this.currentInterval = this.autoDestroyTime;

        this.isCanInteract = false;
    }
}
