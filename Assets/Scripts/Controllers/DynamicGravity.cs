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
        // ������ LayerMask�� �������� ��ġ�� Collider�� ��� ������
        var t_colls = Physics2D.OverlapBoxAll(this.coll2D.bounds.center + _nextMovement, this.coll2D.bounds.size, 0, this.wallMask);
        Vector2 t_pushValue = Vector2.zero;

        foreach (var t in t_colls)
        {
            Rect t_rect1 = new Rect(this.coll2D.bounds.min, this.coll2D.bounds.size);
            Rect t_rect2 = new Rect(t.bounds.min, t.bounds.size);
            t_rect1.center += (Vector2)_nextMovement;

            Rect t_overlapArea = GetOverlapArea(t_rect1, t_rect2);

            // �浹 ������ ���� ���⿡ ���� ���� �Ǵ� �¿�� Ǫ��
            if (t_overlapArea.height < t_overlapArea.width)
            {
                // �߽��� ���� ���� ��� ���� �о��� (���� �ö󰡵���)
                if (t_overlapArea.center.y > this.coll2D.bounds.center.y)
                {
                    t_pushValue.y += t_overlapArea.height; // ���� �б�
                }
                else
                {
                    t_pushValue.y -= t_overlapArea.height; // �Ʒ��� �б�
                }
            }
            else
            {
                // �߽��� �����ʿ� ���� ��� ���������� �о���
                if (t_overlapArea.center.x > this.coll2D.bounds.center.x)
                {
                    t_pushValue.x += t_overlapArea.width; // ���������� �б�
                }
                else
                {
                    t_pushValue.x -= t_overlapArea.width; // �������� �б�
                }
            }
        }

        // ���� Ǫ�� ���� ��ȯ
        return t_pushValue;
    }
    // �� �ݶ��̴��� ��ġ�� ������ ����ϴ� �޼���
    Rect GetOverlapArea(Rect _rect1, Rect _rect2)
    {
        // �� �ݶ��̴��� ���(Rect)�� ������


        // ��ġ�� ���� ���
        float xMin = Mathf.Max(_rect1.xMin, _rect2.xMin);
        float xMax = Mathf.Min(_rect1.xMax, _rect2.xMax);
        float yMin = Mathf.Max(_rect1.yMin, _rect2.yMin);
        float yMax = Mathf.Min(_rect1.yMax, _rect2.yMax);

        // ��ġ�� ������ ũ�� ���
        if (xMax >= xMin && yMax >= yMin)
        {
            return new Rect(new Vector2(xMin, yMin), new Vector2(xMax - xMin, yMax - yMin));
        }
        else
        {
            return Rect.zero; // ��ġ�� ������ ������ �� Rect ��ȯ
        }
    }

    private void Reset()
    {
        this.coll2D = this.transform.GetComponent<Collider2D>();
        return;
    }

}
