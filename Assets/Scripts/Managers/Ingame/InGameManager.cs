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
    /// �����ϱ� ���� ����Ҽ� �ִ� static �������ٰ� �ϳ��� ���� Ŭ���� ����ʳ��´�. �̰� �̱�����.
    /// </summary>
    public static InGameManager instance;

    public GameState state;
    [Header("���� ��")]
    [SerializeField] string currentMapName;
    [SerializeField] MapOptions currentMapObject;
    [SerializeField] Transform mapParent;
    public MapOptions GetMapOptions() => this.currentMapObject;

    [Header("�÷��̾�")]
    [SerializeField] PlayerController currentPlayer;
    public PlayerController GetPlayerController() => this.currentPlayer;

    [SerializeField] GameObject Map;

    [SerializeField] GameObject redFilter;


    private void Awake()
    {
        instance = this;
        Initialization();
        return;
    }


    public void Initialization()
    {
        MoveMap(this.Map, 0);
        return;
    }

    public void MoveMap(GameObject _prefab, int _index)
    {
        if (this.currentMapObject != null)
            Destroy(this.currentMapObject.gameObject);
        var t_map = Instantiate(_prefab, this.mapParent);
        this.currentMapObject = t_map.GetComponent<MapOptions>();
        this.currentMapObject.Initialization();
        this.currentPlayer.transform.parent = this.mapParent;
        this.currentMapName = this.currentMapObject.GetMapName();
        Debug.Log(this.currentMapName);
        this.currentPlayer.transform.position = this.currentMapObject.GetMoveTransfrom(_index).position;
        CameraController.instance.SetMapBoundary(this.currentMapObject.GetMapSize());
    }


    public void ShowRedFilter(float _duration)
    {
        StopAllCoroutines();
        StartCoroutine(Blink(_duration));
    }
    // �����Ÿ� Coroutine
    IEnumerator Blink(float _duration)
    {
        float elapsedTime = 0f;

        // blinkDuration ���� �ݺ�
        while (elapsedTime < _duration)
        {
            // Ÿ�� ������Ʈ�� Ȱ��ȭ ���¸� ������Ŵ
            redFilter.SetActive(!redFilter.activeSelf);

            // ��� �ð� ������Ʈ
            elapsedTime += 0.5f;

            // blinkInterval(0.3��) ���
            yield return new WaitForSeconds(0.5f);
        }

        // �����Ÿ��� ������ ������Ʈ�� Ȱ�� ���·� ���� (�ʿ信 ���� ���� ����)
        redFilter.SetActive(false);
    }
}
