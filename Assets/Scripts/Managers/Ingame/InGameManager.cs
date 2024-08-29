using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public enum GameState
    {
        InProgress = 0,
        Pause = 1,
        InEvent = 2,

    }
    public static InGameManager instance;

    public GameState state;
    [Header("현재 맵")]
    [SerializeField] string currentMapName;
    [SerializeField] MapOptions currentMapObject;
    [SerializeField] Transform mapParent;

    [Header("플레이어")]
    [SerializeField] PlayerController currentPlayer;

    [SerializeField] GameObject Map;


    private void Awake()
    {
        instance = this;
        Initialization();
        return;
    }


    public void Initialization()
    {
        MoveMap(this.Map, false);
        return;
    }

    public void MoveMap(GameObject _prefab, bool _isNext)
    {
        if (this.currentMapObject != null)
            Destroy(this.currentMapObject.gameObject);
        var t_map = Instantiate(_prefab, this.mapParent);
        this.currentMapObject = t_map.GetComponent<MapOptions>();
        this.currentPlayer.transform.parent = this.mapParent;
        this.currentMapName = this.currentMapObject.GetMapName();
        Debug.Log(this.currentMapName);
        if (_isNext)
        {
            this.currentPlayer.transform.position = this.currentMapObject.GetStartTransform().position;
            return;
        }
        else
        {
            this.currentPlayer.transform.position = this.currentMapObject.GetExitTransform().position;
            return;
        }
    }
}
