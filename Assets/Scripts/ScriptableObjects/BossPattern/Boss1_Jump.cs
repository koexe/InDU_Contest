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

        // �̵� ����� ���� ����
        float progress = this.elapsedTime / this.jumpDuration;

        // ������ x, y �̵� (���� ���)
        Vector3 currentPosition = Vector3.Lerp(this.startPos, this.targetPos, progress);

        // ���� ���� (������ ����)
        float height = Mathf.Sin(Mathf.PI * progress) * this.jumpHeight;

        // ���� ��ġ�� ���� �ݿ� (z�� ���� ���̰� �ƴ϶� y������ ó��)
        currentPosition.y += height;

        // ���� ��ġ ����
        this.bossController.transform.position = currentPosition;
       

        return;
    }

}
