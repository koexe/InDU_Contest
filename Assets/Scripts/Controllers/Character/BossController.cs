using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "BossPattern/", fileName = "Bosspattern/Boss1_Basic")]
public class BossController : MonoBehaviour
{
    [Header("보스 패턴 목록")]
    [SerializeField] List<BossPattern> bossPatterns = new List<BossPattern>();
    [SerializeField] BossPattern BasicBossPattern;
    Coroutine currentPattern;

    [Header("보스 능력치")]
    [SerializeField] float speed;


    Transform playerTr;

    private void Start()
    {
        this.playerTr = InGameManager.instance.GetPlayerController().transform;
    }
    enum BossState
    {
        Idle        = 0,
        InPattern   = 1,
        Stun        = 2,
    }

    private void FixedUpdate()
    {
        
    }


    void UpdatePattern()
    {
        
    }


}
