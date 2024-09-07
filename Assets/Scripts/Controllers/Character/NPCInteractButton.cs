using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCInteractButton : MonoBehaviour
{
    [SerializeField] NPCController controller;
    public delegate void ButtonAction();
    public ButtonAction action;
    

    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� �� Raycast�� Ŭ�� ����
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Raycast�� Collider2D�� �ε����� ��
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                this.action.Invoke();
            }
        }
    }
}
