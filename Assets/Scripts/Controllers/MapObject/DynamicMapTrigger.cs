using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DynamicMapTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent enterAction;
    [SerializeField] UnityEvent stayAction;
    [SerializeField] UnityEvent exitAction;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.enterAction?.Invoke();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        this.stayAction?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        this.exitAction?.Invoke();
    }
}
