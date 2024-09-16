using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "BossPattern/Boss1/Boss1_Basic", fileName = "Bosspattern_Basic")]
public class Boss1_Basic : BossPattern
{
    [SerializeField] Vector2 playerDetectionSize;
    [SerializeField] LayerMask mask;
    protected override void AttackAction()
    {
        base.AttackAction();
        var t_player = Physics2D.OverlapBoxAll(
            this.bossController.transform.position,
            this.playerDetectionSize,
            0f,
            this.mask);

        if (t_player.Length >= 1 && this.bossController.isInPattern())
        {
            this.bossController.BasicAttack();
        }
        else
        {
            this.bossController.MoveToPlayer();
        }
    }
}
