using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOptions : MonoBehaviour
{
    [Header("�� �̸�")]
    [SerializeField] string mapName;
    [Header("�� ũ��")]
    [SerializeField] Vector2 mapSize;

    [Header("����� ��")]
    [SerializeField] GameObject previousMap;
    [SerializeField] GameObject nextMap;

    [Header("�� ���� �����۵�")]
    [SerializeField] GameObject mapItems;

    [Header("�� �Ա�, �ⱸ")]

    [SerializeField] Transform startTransform;
    [SerializeField] Transform exitTransform;

    public Transform GetStartTransform() => this.startTransform;
    public Transform GetExitTransform() => this.exitTransform;
    public string GetMapName() => this.mapName;

    public void Initialization()
    {
        //������ �ʱ�ȭ �ڵ� �ۼ�

        //�� ��ȣ�ۿ� �ʱ�ȭ �ڵ� �ۼ�
    }
    public void MoveMap(bool _isNext)
    {
        if (_isNext)
        {
            if (this.nextMap != null)
            {
                InGameManager.instance.MoveMap(this.nextMap, _isNext);
            }
            return;
        }
        else
        {
            if (this.previousMap != null)
            {
                InGameManager.instance.MoveMap(this.previousMap, _isNext);
            }
            return;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(this.transform.position, this.mapSize);
        return;
    }
}
