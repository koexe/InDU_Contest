using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DynamicGravity : MonoBehaviour
{
    [SerializeField] Collider2D coll2D;
    [SerializeField] LayerMask wallMask;


    public Vector2 UpdateCheckWall(Vector3 _nextMovement)
    {
        // 지정된 LayerMask를 기준으로 겹치는 Collider를 모두 가져옴
        var t_colls = Physics2D.OverlapBoxAll(this.coll2D.bounds.center + _nextMovement, this.coll2D.bounds.size, 0, this.wallMask);
        Vector2 t_pushValue = Vector2.zero;

        foreach (var t in t_colls)
        {
            Rect t_rect1 = new Rect(this.coll2D.bounds.min, this.coll2D.bounds.size);
            Rect t_rect2 = new Rect(t.bounds.min, t.bounds.size);
            t_rect1.center += (Vector2)_nextMovement;

            Rect t_overlapArea = GetOverlapArea(t_rect1, t_rect2);

            // 충돌 영역이 넓은 방향에 따라 상하 또는 좌우로 푸시
            if (t_overlapArea.height < t_overlapArea.width)
            {
                // 중심이 위에 있을 경우 위로 밀어줌 (위로 올라가도록)
                if (t_overlapArea.center.y > this.coll2D.bounds.center.y)
                {
                    t_pushValue.y += t_overlapArea.height; // 위로 밀기
                }
                else
                {
                    t_pushValue.y -= t_overlapArea.height; // 아래로 밀기
                }
            }
            else
            {
                // 중심이 오른쪽에 있을 경우 오른쪽으로 밀어줌
                if (t_overlapArea.center.x > this.coll2D.bounds.center.x)
                {
                    t_pushValue.x += t_overlapArea.width; // 오른쪽으로 밀기
                }
                else
                {
                    t_pushValue.x -= t_overlapArea.width; // 왼쪽으로 밀기
                }
            }
        }

        // 최종 푸시 값을 반환
        return t_pushValue;
    }
    // 두 콜라이더의 겹치는 영역을 계산하는 메서드
    Rect GetOverlapArea(Rect _rect1, Rect _rect2)
    {
        // 각 콜라이더의 경계(Rect)를 가져옴


        // 겹치는 영역 계산
        float xMin = Mathf.Max(_rect1.xMin, _rect2.xMin);
        float xMax = Mathf.Min(_rect1.xMax, _rect2.xMax);
        float yMin = Mathf.Max(_rect1.yMin, _rect2.yMin);
        float yMax = Mathf.Min(_rect1.yMax, _rect2.yMax);

        // 겹치는 영역의 크기 계산
        if (xMax >= xMin && yMax >= yMin)
        {
            return new Rect(new Vector2(xMin, yMin), new Vector2(xMax - xMin, yMax - yMin));
        }
        else
        {
            return Rect.zero; // 겹치는 영역이 없으면 빈 Rect 반환
        }
    }

    private void Reset()
    {
        this.coll2D = this.transform.GetComponent<Collider2D>();
        return;
    }

}
