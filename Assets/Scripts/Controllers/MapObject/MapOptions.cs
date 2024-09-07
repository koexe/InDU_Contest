using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum MapType
{
    TopView,
    SideView
}

public class MapOptions : MonoBehaviour
{
    [Header("맵 타입")]
    [SerializeField] MapType type;
    [Header("맵 이름")]
    [SerializeField] string mapName;
    [Header("맵 크기")]
    [SerializeField] Vector2 mapSize;

    [Header("맵 안의 아이템들")]
    [SerializeField] Transform mapItems;

    [Header("맵 안의 Npc들")]
    [SerializeField] Transform mapNPCs;

    [Header("맵 이동 포인트")]

    [SerializeField] List<MapMovePoint> movePoints;
    public Transform GetMoveTransfrom(int index) => this.movePoints[index].transform;


    public string GetMapName() => this.mapName;

    public void Initialization()
    {
        //아이템 초기화 코드 작성

        //맵 상호작용 초기화 코드 작성
        foreach(var t_item in this.mapItems.GetComponentsInChildren<MapItem>())
        {
            t_item.Initialization();
        }
        foreach(var t_Npcs in this.mapNPCs.GetComponentsInChildren<NPCController>())
        { 
            //t_Npcs.Initial
        }

    }
    public void MoveMap(int index)
    {
        if (this.movePoints[index].isOpen)
        {
            Debug.Log("MoveMap");
            InGameManager.instance.MoveMap(this.movePoints[index].linkedMap, this.movePoints[index].linkedPoint);
            return;

        }
        else
        {
            Debug.Log("Movepoint not open");
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

[System.Serializable]
public class MapMovePoint
{
    public Transform transform;
    public int point;
    public bool isOpen;
    [Header("연결된 맵과 위치")]
    public GameObject linkedMap;
    public int linkedPoint;
}