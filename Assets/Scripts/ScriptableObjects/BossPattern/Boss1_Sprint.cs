using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossPattern/Boss1/sprinte", fileName = "Bosspattern/")]
public class Boss1_Sprint : BossPattern
{
    [SerializeField] Vector3 targetPosition;
    [SerializeField] float speed;
    public override void Initialization(BossController _bossController)
    {
        base.Initialization(_bossController);

        return;
    }

    public override void PatternProcess()
    {

        this.currentPatternTime += Time.fixedDeltaTime;

        if (this.currentPatternTime - this.lastTimeStamp >= this.beforeAttackTime && this.patternState == PatternState.BeforeAttack)
        {
            this.patternState = PatternState.InAttack;
            this.lastTimeStamp = this.currentPatternTime;
            this.targetPosition = InGameManager.instance.GetPlayerController().transform.position;
            this.bossController.animator.Play("Run");
            this.bossController.animator.SetBool("IsInRun", true);
            this.bossController.SetWalkArrow();
        }

        if (this.bossController.transform.position == this.targetPosition && this.patternState == PatternState.InAttack)
        {
            this.patternState = PatternState.AfterAttack;
            this.lastTimeStamp = this.currentPatternTime;
            this.bossController.animator.SetBool("IsInRun", false);
            this.bossController.isCollisionEnabled = false;
        }


        if (this.currentPatternTime - this.lastTimeStamp >= this.afterAttackTime && this.patternState == PatternState.AfterAttack)
        {
            this.patternState = PatternState.EndAttack;
            this.lastTimeStamp = this.currentPatternTime;
        }
        if (this.patternState == PatternState.InAttack)
            AttackAction();

        if (this.patternState == PatternState.EndAttack && this.isAutoNextPattern)
            this.bossController.SelectNewPattern(this.isBasicAttack);
        return;
    }


    protected override void AttackAction()
    {
        base.AttackAction();
        this.bossController.isCollisionEnabled = true;
        this.bossController.MoveToPosition(this.targetPosition, this.speed);
    }
}
