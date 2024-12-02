using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

//[CreateAssetMenu(menuName ="BossPattern/",fileName ="Bosspattern/")]
public class BossPattern : ScriptableObject
{
    [SerializeField] protected float beforeAttackTime;
    [SerializeField] protected float attackTime;
    [SerializeField] protected float afterAttackTime;
    [SerializeField] protected float animationPlayTime;
    [SerializeField] public PatternState patternState;

    [SerializeField] protected Animation animation;
    [SerializeField] protected Animator animator;

    [SerializeField] protected float currentPatternTime;

    [SerializeField] protected BossController bossController;

    [SerializeField] public bool isBasicAttack;

    [SerializeField] protected float lastTimeStamp;

    [SerializeField] public bool isAutoNextPattern;

    public virtual void Initialization(BossController _bossController)
    {
        this.currentPatternTime = 0f;
        this.patternState = PatternState.BeforeAttack;
        this.bossController = _bossController;
        return;
    }


    public virtual void PatternProcess()
    {
        this.currentPatternTime += Time.fixedDeltaTime;

        if (this.currentPatternTime - this.lastTimeStamp >= this.beforeAttackTime && this.patternState == PatternState.BeforeAttack)
        {
            this.patternState = PatternState.InAttack;
            this.lastTimeStamp = this.currentPatternTime;
        }
        if (this.currentPatternTime - this.lastTimeStamp >= this.attackTime && this.patternState == PatternState.InAttack)
        {
            this.patternState = PatternState.AfterAttack;
            this.lastTimeStamp = this.currentPatternTime;
        }
        if (this.currentPatternTime - this.lastTimeStamp >= this.afterAttackTime && this.patternState == PatternState.AfterAttack)
        {
            this.patternState = PatternState.EndAttack;
            this.lastTimeStamp = this.currentPatternTime;
        }


        if (this.patternState == PatternState.InAttack)
            AttackAction();


        if(this.patternState == PatternState.EndAttack && this.isAutoNextPattern)
            this.bossController.SelectNewPattern(this.isBasicAttack);


        return;
    }



    protected virtual void AttackAction()
    {

    }
}

public enum PatternState
{
    BeforeAttack,
    InAttack,
    AfterAttack,
    EndAttack,
}