using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DynamicMapTrigger : MonoBehaviour
{
    [Header("이벤트들")]
    [SerializeField] UnityEvent enterAction;
    [SerializeField] UnityEvent stayAction;
    [SerializeField] UnityEvent exitAction;

    //아직 작업 안함
    [Header("Stay 시간 설정")]
    [SerializeField] float maxWaitTime;
    [SerializeField]float currentWaitTime;
    [Header("여러번 사용 설정")]
    [SerializeField] bool isUseMultifle;

    [Header("충돌 마스크")]
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
