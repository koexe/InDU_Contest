using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField]Animator animator;
    // Update is called once per frame
    private void Update()
    {
        // 현재 애니메이터 상태 정보 가져오기
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 애니메이션이 끝났는지 확인 (normalizedTime >= 1일 때 애니메이션이 끝난 상태)
        if (stateInfo.normalizedTime >= 1f && !animator.IsInTransition(0))
        {
            Destroy(gameObject);  // 애니메이션이 끝나면 오브젝트 제거
        }
    }
}
