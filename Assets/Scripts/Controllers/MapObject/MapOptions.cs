using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;




public class MapOptions : MonoBehaviour
{
    [Header("�� �̸�")]
    [SerializeField] string mapName;
    [Header("�� ũ��")]
    [SerializeField] Vector2 mapSize;

    [Header("�� ���� �����۵�")]
    [SerializeField] Transform mapItems;

    [Header("�� ���� Npc��")]
    [SerializeField] Transform mapNPCs;

    [Header("�� �̵� ����Ʈ")]
    [SerializeField] List<MapMovePoint> movePoints;
    public Transform GetMoveTransfrom(int index) => this.movePoints[index].transform;

    public Vector2 GetMapSize() => this.mapSize;
    public string GetMapName() => this.mapName;

    [Header("�� ���̺�����Ʈ")]
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
    [Header("����� �ʰ� ��ġ")]
    public GameObject linkedMap;
    public int linkedPoint;
}