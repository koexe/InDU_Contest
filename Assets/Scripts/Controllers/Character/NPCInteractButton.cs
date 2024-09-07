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
        // 마우스 왼쪽 버튼 클릭 시 Raycast로 클릭 감지
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Raycast가 Collider2D에 부딪혔을 때
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                this.action.Invoke();
            }
        }
    }
}
