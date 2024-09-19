using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;




public class MapOptions : MonoBehaviour
{
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

    public Vector2 GetMapSize() => this.mapSize;
    public string GetMapName() => this.mapName;

    [Header("맵 세이브포인트")]
    [SerializeField] public Transform mapSaveTr;

    public MapItem[] GetMapItems()
    {
        return this.mapItems.transform.GetComponentsInChildren<MapItem>();
    }

    public void Initialization()
    {
        MapItem[] item = this.mapItems.GetComponentsInChildren<MapItem>();

        for (int i = 0; i < item.Length; i++) 
        {
            MapItem t_item = item[i];
            t_item.isGeted = SaveGameManager.instance.currentSaveData.mapItems[InGameManager.instance.GetCurrentMapName()][i];
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
    public bool isOpen;
    [Header("연결된 맵과 위치")]
    public GameObject linkedMap;
    public int linkedPoint;
}