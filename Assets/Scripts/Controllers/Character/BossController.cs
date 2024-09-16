using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : NPCController
{
    enum BossState
    {
        Ready,
        Running,
        Stun,
        Die
    }


    [Header("컴포넌트")]
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("보스 패턴 목록")]
    [SerializeField] List<BossPattern> bossPatterns = new List<BossPattern>();

    [SerializeField] List<BossPattern> bossPatternSetting1 = new List<BossPattern>();
    [SerializeField] List<BossPattern> bossPatternSetting2 = new List<BossPattern>();


    [SerializeField] BossPattern BasicBossPattern;
    [SerializeField] BossPattern currentPattern = null;

    [Header("보스 능력치")]
    [SerializeField] float speed;
    [SerializeField] BossState state;
    [SerializeField] float stunTime;
    [SerializeField] int Hp;
    [SerializeField] float spawnTrapTime;
    [SerializeField] float currentTrapTime;

    Coroutine basicAttackCr;
    public bool isInPattern() => this.basicAttackCr == null ? true : false;

    public bool isCollisionEnabled;

    [SerializeField] public List<GameObject> bushs;
    [SerializeField] GameObject trapPrefab;
    [SerializeField] GameObject currentTrapObj;

    public override void Start()
    {
        base.Start();
        this.bossPatterns = this.bossPatternSetting1;
        this.currentPattern = Instantiate(BasicBossPattern);
        this.currentPattern.Initialization(this);
        this.currentTrapTime = this.spawnTrapTime;
        this.state = BossState.Running;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (this.state == BossState.Running)
        {
            this.currentPattern.PatternProcess();
            SpawnTrap();
        }

    }
    public void MoveToPlayer()
    {
        this.transform.position = Vector2.MoveTowards(
                                                      this.transform.position,
                                                      InGameManager.instance.GetPlayerController().transform.position,
                                                      this.speed * Time.fixedDeltaTime);
        return;
    }
    public void MoveToPosition(Vector3 _position, float _speed)
    {
        this.transform.position = Vector2.MoveTowards(
                                              this.transform.position,
                                              _position,
                                              _speed * Time.fixedDeltaTime);
    }
    public void SelectNewPattern(bool _isBasicAttack)
    {
        if (_isBasicAttack)
        {
            int index = Random.Range(0, this.bossPatterns.Count);
            this.currentPattern = Instantiate(this.bossPatterns[index]);
            this.currentPattern.Initialization(this);
            return;
        }
        else
        {
            this.currentPattern = Instantiate(this.BasicBossPattern);
            this.currentPattern.Initialization(this);
            return;
        }
    }
    public void BasicAttack()
    {
        Debug.Log("기본공격");
        if (this.basicAttackCr == null)
            this.basicAttackCr = StartCoroutine(StopMove(3f));
    }
    IEnumerator StopMove(float _time)
    {
        float t_speed = this.speed;
        this.speed = 0f;
        yield return new WaitForSeconds(_time);
        this.speed = t_speed;
        this.basicAttackCr = null;
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
            Debug.Log("asdf");
            this.state = BossState.Stun;
            StartCoroutine(Stun());
            Destroy(currentTrapObj);
        }
        return;
    }
    IEnumerator Stun()
    {
        this.state = BossState.Stun;
        this.isCanInteract = true;

        yield return new WaitForSeconds(this.stunTime);
        Destroy(this.currentPattern);
        this.currentPattern = Instantiate(this.BasicBossPattern);
        this.currentPattern.Initialization(this);
        this.state = BossState.Running;
        this.isCanInteract = false;
    }
    public override void InteractAction()
    {
        base.InteractAction();
        this.Hp -= 1;
        if (this.Hp == 2)
            this.bossPatterns = this.bossPatternSetting2;

        Destroy(this.currentPattern);
        this.currentPattern = Instantiate(this.BasicBossPattern);
        this.currentPattern.Initialization(this);
        this.state = BossState.Running;
        this.isCanInteract = false;
        return;
    }
    public void StartHide()
    {
        StartCoroutine(SmoothHide());
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
    }
    public void StartShow()
    {
        StartCoroutine(SmoothShow());
    }
    IEnumerator SmoothShow()
    {
        while (this.spriteRenderer.color.a != 1)
        {
            Color t_color = this.spriteRenderer.color;
            t_color.a = Mathf.MoveTowards(t_color.a, 1, Time.fixedDeltaTime);
            this.spriteRenderer.color = t_color;
            yield return new WaitForFixedUpdate();
        }
    }

    void SpawnTrap()
    {
        if (this.currentTrapObj != null) return;

        if (this.currentTrapTime != 0)
        {
            this.currentTrapTime = Mathf.MoveTowards(this.currentTrapTime, 0, Time.fixedDeltaTime);
        }
        else
        {
            this.currentTrapTime = this.spawnTrapTime;
            var obj = Instantiate(this.trapPrefab);
            float randomX = Random.Range(-InGameManager.instance.GetMapOptions().GetMapSize().x / 2, InGameManager.instance.GetMapOptions().GetMapSize().x / 2);
            float randomY = Random.Range(-InGameManager.instance.GetMapOptions().GetMapSize().y / 2, InGameManager.instance.GetMapOptions().GetMapSize().y / 2);
            obj.transform.position = new Vector3(randomX, randomY, 0);
            this.currentTrapObj = obj;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position, Vector3.one);
    }
}
