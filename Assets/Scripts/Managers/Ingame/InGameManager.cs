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
    /// <summary>
    /// 생성하기 전에 사용할수 있는 static 변수에다가 하나만 만들 클론을 집어너놓는다. 이게 싱글턴임.
    /// </summary>
    public static InGameManager instance;

    public GameState state;
    [Header("현재 맵")]
    [SerializeField] string currentMapName;
    [SerializeField] MapOptions currentMapObject;
    [SerializeField] Transform mapParent;
    public MapOptions GetMapOptions() => this.currentMapObject;
    public string GetCurrentMapName() => this.currentMapName;

    [Header("플레이어")]
    [SerializeField] PlayerCharacterController currentPlayer;
    public PlayerCharacterController GetPlayerController() => this.currentPlayer;

    [SerializeField] GameObject Map;

    [SerializeField] GameObject redFilter;

    [SerializeField] GameObject[] Hp;

    [SerializeField] AudioClip bgm;

    private void Awake()
    {
        instance = this;
        Initialization();
        AudioManager.instance.PlayBGM(bgm);
        return;
    }


    public void Initialization()
    {
        this.Map = Resources.Load<GameObject>("Prefabs/Map/" + SaveGameManager.instance.currentSaveData.currentMap);
        if (this.Map == null)
        {
            Debug.Log("Map Not Found");
        }
        else
        {
            MoveMap(this.Map, 0, true);
        }
        return;
    }

    public void MoveMap(GameObject _prefab, int _index, bool _isInitial = false)
    {
        if (this.currentMapObject != null)
            Destroy(this.currentMapObject.gameObject);
        var t_map = Instantiate(_prefab, this.mapParent);
        this.currentMapObject = t_map.GetComponent<MapOptions>();
        this.currentMapName = this.currentMapObject.GetMapName();
        SaveGameManager.instance.currentSaveData.currentMap = this.currentMapName;
        this.currentMapObject.Initialization();
        this.currentPlayer.transform.parent = this.mapParent;
        Debug.Log(this.currentMapName);
        if (_isInitial && this.currentMapObject.mapSaveTr != null)
        {
            this.currentPlayer.transform.position = this.currentMapObject.mapSaveTr.position;
        }
        else
        {
            this.currentPlayer.transform.position = this.currentMapObject.GetMoveTransfrom(_index).position;
        }

        CameraController.instance.SetMapBoundary(this.currentMapObject.GetMapSize());


    }


    public void ShowRedFilter(float _duration)
    {
        StopAllCoroutines();
        StartCoroutine(Blink(_duration));
    }
    // 깜빡거림 Coroutine
    IEnumerator Blink(float _duration)
    {
        float elapsedTime = 0f;

        // blinkDuration 동안 반복
        while (elapsedTime < _duration)
        {
            // 타겟 오브젝트의 활성화 상태를 반전시킴
            redFilter.SetActive(!redFilter.activeSelf);

            // 경과 시간 업데이트
            elapsedTime += 0.5f;

            // blinkInterval(0.3초) 대기
            yield return new WaitForSeconds(0.5f);
        }

        // 깜빡거림이 끝나면 오브젝트를 활성 상태로 복구 (필요에 따라 수정 가능)
        redFilter.SetActive(false);
    }


    public void PlayEffect(string _name, Vector3 _postion)
    {
        var t_obj = Resources.Load<GameObject>($"Prefabs/Effect/{_name}");
        if (t_obj != null)
        {
            var t_instance = Instantiate(t_obj);
            t_instance.transform.position = _postion;
            return;
        }
        else
        {
            Debug.Log("Obj Not Found");
        }
    }

    public void ChangeHP(int _index)
    {
        if (_index == 3)
            _index--;
        this.Hp[_index].SetActive(true);
        foreach (var Hp in this.Hp)
            Hp.SetActive(false);
        for (int i = 0; i < _index; i++)
            this.Hp[i].SetActive(true);

    }

    public void DeadReturn()
    {
        UIManager.instance.ShowUI("GameOver");
        SaveGameManager.instance.ResetSave();
        this.Map = Resources.Load<GameObject>("Prefabs/Map/" + SaveGameManager.instance.currentSaveData.currentMap);
        if (this.Map == null)
        {
            Debug.Log("Map Not Found");
        }
        else
        {
            MoveMap(this.Map, 0, true);
        }

    }


}
