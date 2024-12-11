using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController1 : NPCController
{
    enum BossStates
    {
        Dialog,
        InPattern,
        EndPattern,
        Stun,
    }

    [Header("컴포넌트")]
    [SerializeField] BossStates state;
    [SerializeField] Collider2D coll2d;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;


    [SerializeField] Vector3 crossScratchArea;
    [SerializeField] Vector3 basicScratchArea;
    [SerializeField] Vector3 basicAttackOffset;
    [SerializeField] LayerMask playerLayerMask;

    [Header("보스 패턴 설정")]
    [SerializeField] bool isStarted = false;
    [SerializeField] bool isCollisionEnabled;
    [SerializeField] bool currentMoveArrow;

    [SerializeField] bool isPattern2;

    [SerializeField] float jumpHeight;
    [SerializeField] float bushMoveSpeed;
    [SerializeField] float chargeSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] int ambushChargeCount;
    [SerializeField] Transform[] bushTransform1;
    [SerializeField] Transform[] bushTransform2;

    [SerializeField] GameObject trapPrefab;
    [SerializeField] GameObject currentTrapObj;

    [SerializeField] float stunTime;


    [SerializeField] float pattern1Cooltime;
    [SerializeField] float pattern2Cooltime;
    [SerializeField] float pattern3Cooltime;
    [SerializeField] float pattern4Cooltime;

    [SerializeField] float currentPattern1Cooltime;
    [SerializeField] float currentPattern2Cooltime;
    [SerializeField] float currentPattern3Cooltime;
    [SerializeField] float currentPattern4Cooltime;


    [SerializeField] float pattern2Distance;





    Coroutine crCurrentPattern;

    public override void Start()
    {
        this.currentPattern1Cooltime = this.pattern1Cooltime;
        this.currentPattern2Cooltime = this.pattern2Cooltime;
        this.currentPattern3Cooltime = this.pattern3Cooltime;
        this.currentPattern4Cooltime = this.pattern4Cooltime;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            this.crCurrentPattern = StartCoroutine(Ambush());
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            this.crCurrentPattern = StartCoroutine(Crossscratch());
        }
    }
#endif

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (this.isStarted)
        {
            UpdatePatternCooltime();
            if (this.crCurrentPattern == null)
            {
                switch (GetPattern())
                {
                    case 1:
                        this.crCurrentPattern = StartCoroutine(Crossscratch());
                        break;
                    case 2:
                        this.crCurrentPattern = StartCoroutine(Dash());
                        break;
                    case 3:
                        this.crCurrentPattern = StartCoroutine(Ambush());
                        break;
                    case 4:
                        this.crCurrentPattern = StartCoroutine(AmbushPhase2());
                        break;
                    case 0:
                        this.crCurrentPattern = StartCoroutine(Scratch());
                        break;
                    case -1:
                        MoveToPosition(InGameManager.instance.GetPlayerController().transform.position, this.walkSpeed);
                        break;
                }

            }
        }
    }


    public void MoveToPosition(Vector3 _position, float _speed)
    {
        this.transform.position = Vector2.MoveTowards(
                                              this.transform.position,
                                              _position,
                                              _speed * Time.fixedDeltaTime);
    }

    public void ChargeToPosition(Vector3 _position, float _speed)
    {
        this.coll2d.enabled = true;
        this.transform.position = Vector2.MoveTowards(
                                              this.transform.position,
                                              _position,
                                              _speed * Time.fixedDeltaTime);
    }



    int GetPattern()
    {
        if (this.currentPattern3Cooltime == 0f)
        {
            this.currentPattern3Cooltime = this.pattern3Cooltime;
            return 3;
        }
        else if (this.currentPattern2Cooltime == 0f && Vector3.Distance(this.transform.position, InGameManager.instance.GetPlayerController().transform.position) > this.pattern2Distance)
        {
            this.currentPattern2Cooltime = this.pattern2Cooltime;
            return 2;
        }
        else if (this.currentPattern4Cooltime == 0f && this.isPattern2)
        {
            this.currentPattern4Cooltime = this.pattern4Cooltime;
            return 4;
        }
        else if (this.currentPattern1Cooltime == 0f)
        {
            var t_player = Physics2D.OverlapBox(
                     this.transform.position + this.basicAttackOffset,
                     this.crossScratchArea,
                     this.playerLayerMask);
            if(t_player != null)
            {
                this.currentPattern1Cooltime = this.pattern1Cooltime;
                return 1;
            }
            else
            {
                return -1;
            }

        }
        else
        {
            var t_player = Physics2D.OverlapBox(
                      this.transform.position + this.basicAttackOffset,
                      this.basicScratchArea,
                      this.playerLayerMask);
            if (t_player != null)
            {
                return 0;
            }
            else
                return -1;
  
        }
    }

    void UpdatePatternCooltime()
    {
        this.currentPattern1Cooltime = Mathf.MoveTowards(this.currentPattern1Cooltime, 0f, Time.fixedDeltaTime);
        this.currentPattern2Cooltime = Mathf.MoveTowards(this.currentPattern2Cooltime, 0f, Time.fixedDeltaTime);
        this.currentPattern3Cooltime = Mathf.MoveTowards(this.currentPattern3Cooltime, 0f, Time.fixedDeltaTime);
        if (this.isPattern2)
            this.currentPattern4Cooltime = Mathf.MoveTowards(this.currentPattern4Cooltime, 0f, Time.fixedDeltaTime);
        return;
    }




    IEnumerator Ambush()
    {
        Vector3 t_MovePoint = GetNearestBush();
        bool t_point = GetNearestBushPoint();

        for (int i = 0; i < this.ambushChargeCount; i++)
        {
            this.animator.Play("Dash");
            while (Vector3.Distance(this.transform.position, t_MovePoint) > 0.05f)
            {
                MoveToPosition(t_MovePoint, this.bushMoveSpeed);
                yield return new WaitForFixedUpdate();
            }

            yield return StartCoroutine(SmoothHide());

            if (t_point)
            {
                this.transform.position = this.bushTransform1[Random.Range(0, this.bushTransform1.Length)].position;
                t_MovePoint = this.bushTransform2[Random.Range(0, this.bushTransform2.Length)].position;
            }
            else
            {
                this.transform.position = this.bushTransform2[Random.Range(0, this.bushTransform2.Length)].position;
                t_MovePoint = this.bushTransform1[Random.Range(0, this.bushTransform1.Length)].position;
            }

            this.spriteRenderer.color = Color.white;
            this.isCollisionEnabled = true;

            yield return new WaitForSeconds(0.5f);

            while (Vector3.Distance(this.transform.position, t_MovePoint) > 0.05f)
            {
                ChargeToPosition(t_MovePoint, this.chargeSpeed);
                yield return new WaitForFixedUpdate();
            }
            this.isCollisionEnabled = false;
            t_MovePoint = GetNearestBush();
            t_point = GetNearestBushPoint();
        }

        t_MovePoint = InGameManager.instance.GetPlayerController().transform.position;
        Vector3 t_StartPos = this.transform.position;

        this.animator.Play("Jump");
        InGameManager.instance.PlayEffect("JumpStart", this.transform.position - new Vector3(0, -1, 0));

        float t_JumpTime = 0.6f;
        float t_elapsedTime = 0f;

        while (t_elapsedTime < t_JumpTime)
        {
            t_elapsedTime += Time.fixedDeltaTime;

            // 이동 경로의 진행 비율
            float progress = t_elapsedTime / t_JumpTime;

            // 몬스터의 x, y 이동 (직선 경로)
            Vector3 currentPosition = Vector3.Lerp(t_StartPos, t_MovePoint, progress);

            // 점프 높이 (포물선 형태)
            float height = Mathf.Sin(Mathf.PI * progress) * this.jumpHeight;

            // 최종 위치에 높이 반영 (z는 실제 높이가 아니라 y축으로 처리)
            currentPosition.y += height;

            // 몬스터 위치 갱신
            this.transform.position = currentPosition;
            yield return new WaitForFixedUpdate();
        }

        CameraController.instance.TriggerShake(3f);

        var t_player = Physics2D.OverlapCircle(
                              this.transform.position,
                              2f,
                              this.playerLayerMask);
        if (t_player != null)
        {
            t_player.transform.GetComponent<PlayerCharacterController>().AddHp(-1);
        }

        this.animator.Play("Idle");

        InGameManager.instance.PlayEffect("Jump", this.transform.position - new Vector3(0, -1, 0));


        this.crCurrentPattern = null;
        yield break;
    }
    IEnumerator Scratch()
    {
        this.animator.Play("Scratch");
        yield return new WaitForSeconds(0.2f);
        InGameManager.instance.PlayEffect("Scratch", this.transform.position + (Vector3)this.basicAttackOffset);
        float elapsedTime = 0.0f;
        while (elapsedTime < 0.3f)
        {
            var t_player = Physics2D.OverlapBox(
                      this.transform.position + this.basicAttackOffset,
                      this.crossScratchArea,
                      this.playerLayerMask);
            if (t_player != null)
            {
                if (t_player.transform.GetComponent<PlayerCharacterController>() != null)
                    t_player.transform.GetComponent<PlayerCharacterController>().AddHp(-1);
            }
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.2f);
        this.crCurrentPattern = null;
        yield break;
    }
    IEnumerator Crossscratch()
    {
        yield return new WaitForSeconds(0.1f);
        this.animator.Play("Swing");
        yield return new WaitForSeconds(0.2f);
        InGameManager.instance.PlayEffect("Scratch", this.transform.position + (Vector3)this.basicAttackOffset);
        float elapsedTime = 0.0f;
        while (elapsedTime < 0.4f)
        {
            var t_player = Physics2D.OverlapBox(
                      this.transform.position + this.basicAttackOffset,
                      this.crossScratchArea,
                      this.playerLayerMask);
            if (t_player != null)
            {
                if (t_player.transform.GetComponent<PlayerCharacterController>() != null)
                    t_player.transform.GetComponent<PlayerCharacterController>().AddHp(-1);
            }
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.2f);
        this.crCurrentPattern = null;

        yield break;
    }

    IEnumerator Dash()
    {
        Vector3 t_MovePoint = InGameManager.instance.GetPlayerController().transform.position;

        yield return new WaitForSeconds(0.3f);
        this.isCollisionEnabled = true;
        while (Vector3.Distance(this.transform.position, t_MovePoint) > 0.05f)
        {
            ChargeToPosition(t_MovePoint, this.chargeSpeed);
            yield return new WaitForFixedUpdate();
        }
        this.isCollisionEnabled = false;
        yield return new WaitForSeconds(0.5f);


        this.crCurrentPattern = null;
        yield break;
    }
    IEnumerator AmbushPhase2()
    {
        Vector3 t_MovePoint = GetNearestBush();
        bool t_point = GetNearestBushPoint();


        while (Vector3.Distance(this.transform.position, t_MovePoint) > 0.05f)
        {
            MoveToPosition(t_MovePoint, this.bushMoveSpeed);
            yield return new WaitForFixedUpdate();
        }

        yield return StartCoroutine(SmoothHide());

        if (t_point)
        {
            this.transform.position = this.bushTransform1[Random.Range(0, this.bushTransform1.Length)].position;
            t_MovePoint = this.bushTransform2[Random.Range(0, this.bushTransform2.Length)].position;
        }
        else
        {
            this.transform.position = this.bushTransform2[Random.Range(0, this.bushTransform2.Length)].position;
            t_MovePoint = this.bushTransform1[Random.Range(0, this.bushTransform1.Length)].position;
        }

        this.spriteRenderer.color = Color.white;

        t_MovePoint = InGameManager.instance.GetPlayerController().transform.position;
        Vector3 t_StartPos = this.transform.position;

        this.animator.Play("JumpReady");
        InGameManager.instance.PlayEffect("JumpStart", this.transform.position - new Vector3(0, -1, 0));

        float t_JumpTime = 0.6f;
        float t_elapsedTime = 0f;

        while (t_elapsedTime < t_JumpTime)
        {
            t_elapsedTime += Time.fixedDeltaTime;

            // 이동 경로의 진행 비율
            float progress = t_elapsedTime / t_JumpTime;

            // 몬스터의 x, y 이동 (직선 경로)
            Vector3 currentPosition = Vector3.Lerp(t_StartPos, t_MovePoint, progress);

            // 점프 높이 (포물선 형태)
            float height = Mathf.Sin(Mathf.PI * progress) * this.jumpHeight;

            // 최종 위치에 높이 반영 (z는 실제 높이가 아니라 y축으로 처리)
            currentPosition.y += height;

            // 몬스터 위치 갱신
            this.transform.position = currentPosition;
            yield return new WaitForFixedUpdate();
        }

        CameraController.instance.TriggerShake(3f);

        var t_player = Physics2D.OverlapCircle(
                              this.transform.position,
                              2f,
                              this.playerLayerMask);
        if (t_player != null)
        {
            t_player.transform.GetComponent<PlayerCharacterController>().AddHp(-1);
        }

        this.animator.Play("Idle");

        InGameManager.instance.PlayEffect("Jump", this.transform.position - new Vector3(0, -1, 0));


        this.crCurrentPattern = null;
        yield break;
    }

    Vector3 GetNearestBush()
    {
        Vector3 t_bushPos = Vector3.zero;
        float t_distance = 100000f;

        foreach (Transform t in this.bushTransform1)
        {
            if (Vector3.Distance(this.transform.position, t.position) < t_distance)
            {
                t_distance = Vector3.Distance(this.transform.position, t.position);
                t_bushPos = t.position;
            }
        }

        foreach (Transform t in this.bushTransform2)
        {
            if (Vector3.Distance(this.transform.position, t.position) < t_distance)
            {
                t_distance = Vector3.Distance(this.transform.position, t.position);
                t_bushPos = t.position;
            }
        }

        return t_bushPos;
    }
    bool GetNearestBushPoint()
    {
        Vector3 t_bushPos = Vector3.zero;
        float t_distance = 100000f;
        bool t_point = false;

        foreach (Transform t in this.bushTransform1)
        {
            if (Vector3.Distance(this.transform.position, t.position) < t_distance)
            {
                t_distance = Vector3.Distance(this.transform.position, t.position);
                t_bushPos = t.position;
                t_point = true;
            }
        }

        foreach (Transform t in this.bushTransform2)
        {
            if (Vector3.Distance(this.transform.position, t.position) < t_distance)
            {
                t_distance = Vector3.Distance(this.transform.position, t.position);
                t_bushPos = t.position;
                t_point = false;
            }
        }

        return t_point;
    }


    IEnumerator SmoothHide()
    {
        while (this.spriteRenderer.color.a != 0)
        {
            Color t_color = this.spriteRenderer.color;
            t_color.a = Mathf.MoveTowards(t_color.a, 0, Time.fixedDeltaTime);
            this.spriteRenderer.color = t_color;
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }


    public void SetWalkArrow()
    {
        if (this.transform.position.x > InGameManager.instance.GetPlayerController().transform.position.x)
        {
            if (this.currentMoveArrow)
            {
                this.currentMoveArrow = false;
                this.spriteRenderer.flipX = false;
                this.basicAttackOffset = -this.basicAttackOffset;
            }
        }
        else
        {
            if (!this.currentMoveArrow)
            {
                this.currentMoveArrow = true;
                this.spriteRenderer.flipX = true;
                this.basicAttackOffset = -this.basicAttackOffset;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (this.isCollisionEnabled)
                InGameManager.instance.GetPlayerController().AddHp(-1);
        }
        if (collision.tag == "BossTrap")
        {
            this.state = BossStates.Stun;
            this.crCurrentPattern = StartCoroutine(Stun());
            this.animator.Play("Stun");
            Destroy(currentTrapObj);
        }
        return;
    }
    IEnumerator Stun()
    {
        this.state = BossStates.Stun;
        this.isCanInteract = true;

        yield return new WaitForSeconds(this.stunTime);
        this.state = BossStates.EndPattern;
        this.isCanInteract = false;
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(this.transform.position + this.basicAttackOffset, this.crossScratchArea);

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(this.transform.position + this.basicAttackOffset, this.basicScratchArea);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, this.pattern2Distance);
    }


}
