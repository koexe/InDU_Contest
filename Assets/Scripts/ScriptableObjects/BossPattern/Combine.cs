using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "BossPattern/Boss1/combine", fileName = "Bosspattern/")]
public class Combine : BossPattern
{
    [SerializeField] List<BossPattern> bossPatternsSetting;

    [SerializeField] List<BossPattern> bossPatterns;

    [SerializeField] int currentIndex;

    public override void Initialization(BossController _bossController)
    {
        base.Initialization(_bossController);
        foreach (BossPattern pattern in bossPatternsSetting)
            this.bossPatterns.Add(Instantiate(pattern));
        foreach (BossPattern pattern in bossPatterns)
        {
            pattern.Initialization(_bossController);
            pattern.isAutoNextPattern = false;
        }

        this.currentIndex = 0;
    }

    public override void PatternProcess()
    {
        if (this.currentIndex == bossPatternsSetting.Count)
        {
            this.bossController.SelectNewPattern(this.isBasicAttack);
        }
        else
        {
            this.bossPatterns[this.currentIndex].PatternProcess();
            if (this.bossPatterns[this.currentIndex].patternState == PatternState.EndAttack)
            {
                this.currentIndex++;
            }
        }
    }

}
