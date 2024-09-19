using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossPattern/Boss1/jump", fileName = "Bosspattern/")]
public class Boss1_Jump : BossPattern
{
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    [SerializeField] float elapsedTime;
    [SerializeField] float jumpDuration;
    [SerializeField] Vector3 targetPos;
    [SerializeField] Vector3 startPos;
    [SerializeField] LayerMask layer;

    public override void Initialization(BossController _bossController)
    {
        base.Initialization(_bossController);
        this.bossController.animator.Play("JumpReady");

    }
    public override void PatternProcess()
    {

        this.currentPatternTime += Time.fixedDeltaTime;

        if (this.currentPatternTime - this.lastTimeStamp >= this.beforeAttackTime && this.patternState == PatternState.BeforeAttack)
        {
            this.patternState = PatternState.InAttack;
            this.lastTimeStamp = this.currentPatternTime;

            this.startPos = this.bossController.transform.position;
            this.targetPos = InGameManager.instance.GetPlayerController().transform.position;
            if (this.startPos.x > this.targetPos.x)
                this.targetPos.x -= 2;
            else
                this.targetPos.x += 2;


            this.bossController.animator.SetTrigger("Jump");

            this.elapsedTime = 0f;
            this.jumpDuration = Vector3.Distance(this.startPos, this.targetPos) / this.speed;
            this.bossController.SetWalkArrow();
        }

        if (Vector3.Distance(this.bossController.transform.position, this.targetPos) < 0.2f && this.patternState == PatternState.InAttack)
        {
            this.patternState = PatternState.AfterAttack;
            this.lastTimeStamp = this.currentPatternTime;
            CameraController.instance.TriggerShake(3f);

            var t_player = Physics2D.OverlapCircle(
                                  this.bossController.transform.position,
                                  2f,
                                  this.layer);
            if (t_player != null)
            {
                t_player.transform.GetComponent<PlayerCharacterController>().AddHp(-1);
            }

            this.bossController.animator.Play("Idle");
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
        JumpMove();
    }
    void JumpMove()
    {
        this.elapsedTime += Time.fixedDeltaTime;

        // 이동 경로의 진행 비율
        float progress = this.elapsedTime / this.jumpDuration;

        // 몬스터의 x, y 이동 (직선 경로)
        Vector3 currentPosition = Vector3.Lerp(this.startPos, this.targetPos, progress);

        // 점프 높이 (포물선 형태)
        float height = Mathf.Sin(Mathf.PI * progress) * this.jumpHeight;

        // 최종 위치에 높이 반영 (z는 실제 높이가 아니라 y축으로 처리)
        currentPosition.y += height;

        // 몬스터 위치 갱신
        this.bossController.transform.position = currentPosition;
       

        return;
    }

}
