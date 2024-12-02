using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BossPattern/Boss1/HideAttack", fileName = "Bosspattern/")]
public class Boss1_HideAttack : BossPattern
{
    [SerializeField] Vector3 targetBushPosition;
    [SerializeField] float speed;

    [SerializeField] int currentHideCount;
    public override void Initialization(BossController _bossController)
    {
        base.Initialization(_bossController);
        return;
    }

    public override void PatternProcess()
    {

        this.currentPatternTime += Time.fixedDeltaTime;
        //패턴 선딜 시간
        if (this.currentPatternTime - this.lastTimeStamp >= this.beforeAttackTime && this.patternState == PatternState.BeforeAttack)
        {
            this.lastTimeStamp = this.currentPatternTime;
            this.targetBushPosition = this.bossController.bushs_1[Random.Range(0, this.bossController.bushs_1.Length)].transform.position;
            this.patternState = PatternState.InAttack;
        }
        //패턴 진행
        else if (this.patternState == PatternState.InAttack)
            AttackAction();
        //패턴 후딜 시간
        else if (Vector2.Distance(this.bossController.transform.position, this.targetBushPosition) < 1 && this.patternState == PatternState.InAttack)
        {
            this.patternState = PatternState.AfterAttack;
            this.lastTimeStamp = this.currentPatternTime;

            this.bossController.StartHide();
        }
        else if (this.currentPatternTime - this.lastTimeStamp >= this.afterAttackTime && this.patternState == PatternState.AfterAttack)
        {
            this.patternState = PatternState.EndAttack;
            this.lastTimeStamp = this.currentPatternTime;
            this.bossController.StartHide();
            this.bossController.transform.position = this.bossController.bushs_1[Random.Range(0, this.bossController.bushs_1.Length)].transform.position;
        }
        else if (this.patternState == PatternState.EndAttack && this.isAutoNextPattern)
        {
            this.bossController.StartShow();
            this.bossController.SelectNewPattern(this.isBasicAttack);
        }
        else if (this.patternState == PatternState.EndAttack)
        {
            this.bossController.StartShow();
        }

        return;
    }

    protected override void AttackAction()
    {
        base.AttackAction();
        this.bossController.MoveToPosition(this.targetBushPosition, this.speed);
        this.bossController.isCollisionEnabled = true;
        
    }





    public override bool CheckCondition()
    {
        if (this.currentCoolTime == 0f)
            return true;
        else
            return false;
    }
}
