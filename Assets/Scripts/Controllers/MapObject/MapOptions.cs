using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOptions : MonoBehaviour
{
    [Header("맵 이름")]
    [SerializeField] string mapName;
    [Header("맵 크기")]
    [SerializeField] Vector2 mapSize;

    [Header("연결된 맵")]
    [SerializeField] GameObject previousMap;
    [SerializeField] GameObject nextMap;

    [Header("맵 안의 아이템들")]
    [SerializeField] GameObject mapItems;

    [Header("맵 입구, 출구")]

    [SerializeField] Transform startTransform;
    [SerializeField] Transform exitTransform;

    public Transform GetStartTransform() => this.startTransform;
    public Transform GetExitTransform() => this.exitTransform;
    public string GetMapName() => this.mapName;

    public void Initialization()
    {
        //아이템 초기화 코드 작성

        //맵 상호작용 초기화 코드 작성
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
