using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DynamicMapTrigger : MonoBehaviour
{
    [Header("�̺�Ʈ��")]
    [SerializeField] UnityEvent enterAction;
    [SerializeField] UnityEvent stayAction;
    [SerializeField] UnityEvent exitAction;

    //���� �۾� ����
    [Header("Stay �ð� ����")]
    [SerializeField] float maxWaitTime;
    [SerializeField]float currentWaitTime;
    [Header("������ ��� ����")]
    [SerializeField] bool isUseMultifle;

    [Header("�浹 ����ũ")]
    [SerializeField] LayerMask mask;

    private void Start()
    {
        this.currentWaitTime = this.maxWaitTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.enterAction?.Invoke();
        return;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((mask.value & (1 << collision.gameObject.layer)) == 0) return;
        if (this.currentWaitTime == 0)
        {
            this.stayAction?.Invoke();
            this.currentWaitTime = this.maxWaitTime;
            return;
        }
        else
        {
            this.currentWaitTime = Mathf.MoveTowards(this.currentWaitTime, 0, Time.deltaTime);
            return;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        this.exitAction?.Invoke();
        return;
    }

    private void OnDrawGizmos()
    {
        if (this.GetComponent<Collider2D>() == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(this.GetComponent<Collider2D>().bounds.center,this.GetComponent<Collider2D>().bounds.size); 
    }
}
