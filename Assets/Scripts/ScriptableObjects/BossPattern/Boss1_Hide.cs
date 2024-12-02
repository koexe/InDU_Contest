using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BossPattern/Boss1/hide", fileName = "Bosspattern/")]
public class Boss1_Hide : BossPattern
{
    [SerializeField] Vector3 targetBushPosition;
    [SerializeField] float speed;
    public override void Initialization(BossController _bossController)
    {
        base.Initialization(_bossController);
        //this.targetBushPosition = this.bossController.bushs[Random.Range(0, this.bossController.bushs.Count)].transform.position;
    }

    public override void PatternProcess()
    {

        this.currentPatternTime += Time.fixedDeltaTime;

        if (this.currentPatternTime - this.lastTimeStamp >= this.beforeAttackTime && this.patternState == PatternState.BeforeAttack)
        {
            this.lastTimeStamp = this.currentPatternTime;
            this.targetBushPosition = this.bossController.bushs[Random.Range(0, this.bossController.bushs.Count)].transform.position;
            this.patternState = PatternState.InAttack;
        }

        if (this.bossController.transform.position == this.targetBushPosition && this.patternState == PatternState.InAttack)
        {
            this.patternState = PatternState.AfterAttack;
            this.lastTimeStamp = this.currentPatternTime;
            
            this.bossController.StartHide();
        }


        if (this.currentPatternTime - this.lastTimeStamp >= this.afterAttackTime && this.patternState == PatternState.AfterAttack)
        {
            this.patternState = PatternState.EndAttack;
            this.lastTimeStamp = this.currentPatternTime;
            this.bossController.StartHide();
            this.bossController.transform.position = this.bossController.bushs[Random.Range(0, this.bossController.bushs.Count)].transform.position;
        }
        if (this.patternState == PatternState.InAttack)
            AttackAction();

        if (this.patternState == PatternState.EndAttack && this.isAutoNextPattern)
        {
            this.bossController.StartShow();    
            this.bossController.SelectNewPattern(this.isBasicAttack);
        }
        else if(this.patternState == PatternState.EndAttack)
        {
            this.bossController.StartShow();
        }

        return;
    }

    protected override void AttackAction()
    {
        base.AttackAction();
        this.bossController.MoveToPosition(this.targetBushPosition, this.speed);
    }
}
