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
    [Header("�� Ÿ��")]
    [SerializeField] MapType type;
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


    public string GetMapName() => this.mapName;

    public void Initialization()
    {
        //������ �ʱ�ȭ �ڵ� �ۼ�

        //�� ��ȣ�ۿ� �ʱ�ȭ �ڵ� �ۼ�
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
    [Header("����� �ʰ� ��ġ")]
    public GameObject linkedMap;
    public int linkedPoint;
}