using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

[CreateAssetMenu(menuName ="BossPattern/",fileName ="Bosspattern/")]
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

    public void Initialization()
    {
        this.currentPatternTime = 0f;
        this.patternState = PatternState.BeforeAttack;
        return;
    }


    protected virtual void PatternProcess()
    {
        this.currentPatternTime += Time.fixedDeltaTime;

        if (this.currentPatternTime >= this.beforeAttackTime && this.patternState == PatternState.BeforeAttack)
            patternState = PatternState.InAttack;

        if(this.currentPatternTime >= this.attackTime && this.patternState == PatternState.InAttack)
            patternState = PatternState.AfterAttack;

        if (this.currentPatternTime >= this.afterAttackTime && this.patternState == PatternState.AfterAttack)
            patternState = PatternState.EndAttack;

        if(this.currentPatternTime >= this.animationPlayTime)
            


        if (this.patternState == PatternState.InAttack)
            AttackAction();
        return;
    }



    protected virtual void AttackAction()
    {
        Debug.Log("Å©¾Æ¾Ó");
    }
}

public enum PatternState
{
    BeforeAttack,
    InAttack,
    AfterAttack,
    EndAttack,
}