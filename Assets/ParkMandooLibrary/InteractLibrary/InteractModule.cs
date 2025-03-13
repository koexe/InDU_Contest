using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractModule : MonoBehaviour
{
    [Header("현재 상호작용중인 오브젝트")]
    List<NPCController> nowInteractNPC = new List<NPCController>();
    [SerializeField] float currentInteractTime;
    [SerializeField] float currentMaxInteractTime;
    [SerializeField] bool isNowInteract;


    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] SpriteRenderer lindRendererBg;

    private void Start()
    {
        Initialization();
        return;
    }
    public void Initialization()
    {
        SettingKeyboard();
        return;
    }

    private void Update()
    {
        UpdateInteractTime();
        UpdateGauge();
    }
    void SettingKeyboard()
    {
        IngameInputManager.instance.AddKeyboardAction(KeyCode.Space, () => UpdateInteract());
    }

    public void AddNowInteractNPC(NPCController npc)
    {
        if (!this.nowInteractNPC.Contains(npc))
            this.nowInteractNPC.Add(npc);
    }
    public void RemoveNotInteractNPC(NPCController npc)
    {
        if (this.nowInteractNPC.Contains(npc))
            this.nowInteractNPC.Remove(npc);
    }
    #region 상호작용
    public bool HoldInteract(float _maxWaitTime)
    {
        this.currentMaxInteractTime = _maxWaitTime;
        this.isNowInteract = true;

        if (this.currentInteractTime != 0 && this.lindRendererBg.enabled == false)
            this.lindRendererBg.enabled = true;

        if (this.currentInteractTime != _maxWaitTime)
        {
            this.currentInteractTime = Mathf.MoveTowards(this.currentInteractTime, _maxWaitTime, Time.fixedDeltaTime);

            return false;
        }
        else
        {
            this.currentInteractTime = 0f;
            return true;
        }

    }
    void UpdateInteractTime()
    {
        if (!this.isNowInteract)
        {
            this.currentInteractTime = Mathf.MoveTowards(this.currentInteractTime, 0, Time.fixedDeltaTime);
        }
        if (this.currentInteractTime == 0 && this.lindRendererBg.enabled == true)
            this.lindRendererBg.enabled = false;
        this.isNowInteract = false;
    }

    void UpdateInteract()
    {
        if (InGameManager.instance.state != InGameManager.GameState.InProgress) return;

        if (this.nowInteractNPC.Count != 0)
        {
            foreach (var npc in this.nowInteractNPC)
                npc.InteractWait();
        }
    }
    // 게이지 업데이트 메서드
    public void UpdateGauge()
    {
        if (this.currentInteractTime == 0 && !this.isNowInteract)
        {
            this.lineRenderer.gameObject.SetActive(false);
            return;
        }
        else
            this.lineRenderer.gameObject.SetActive(true);


        // 현재 값 비율 계산
        float gaugeLength = Mathf.Clamp(this.currentInteractTime / this.currentMaxInteractTime, 0, 1);

        // 시작점과 끝점 설정 (게이지의 크기 조절)
        lineRenderer.SetPosition(0, this.lineRenderer.transform.position);
        lineRenderer.SetPosition(1, this.lineRenderer.transform.position + new Vector3(gaugeLength, 0, 0));
    }

    // 게이지 값 변경 예시
    #endregion
}
