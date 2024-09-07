using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

[CreateAssetMenu(menuName ="BossPattern/",fileName ="Bosspattern/")]
public class BossPattern : ScriptableObject
{
    [SerializeField] protected float beforeAttackTime;
    [SerializeField] protected float afterAttackTime;

    [SerializeField] protected Animation animation;


    protected virtual IEnumerator ExecuteAttack()
    {
        yield return new WaitForSeconds(this.beforeAttackTime);


        AttackAction();


        yield return new WaitForSeconds(this.afterAttackTime);
    }

    protected virtual void AttackAction()
    {
        Debug.Log("Å©¾Æ¾Ó");
    }

}
