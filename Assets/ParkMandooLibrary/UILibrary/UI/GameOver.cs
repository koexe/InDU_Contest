using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : PopUpUI
{
    public Image targetImage;  // 알파값을 조정할 이미지
    public float fadeDuration = 1.5f;  // 페이드 지속 시간
    public override void Initialization(string _custom = "")
    {
        base.Initialization(_custom);
        InGameManager.instance.state = InGameManager.GameState.Pause;
        StartAlphaTransition();
    }


    public override void DeleteUI()
    {
        base.DeleteUI();
        InGameManager.instance.state = InGameManager.GameState.InProgress;
    }
    public void StartAlphaTransition()
    {
        StartCoroutine(FadeInAndOut());
    }

    // 알파값을 50 -> 255 -> 0으로 조정하는 코루틴
    IEnumerator FadeInAndOut()
    {
        // 알파값을 50에서 255로
        yield return StartCoroutine(FadeAlpha(50f / 255f, 1f, fadeDuration));

        // 알파값을 255에서 0으로
        yield return StartCoroutine(FadeAlpha(1f, 0f, fadeDuration));
        DeleteUI();

    }

    // 알파값을 fromAlpha에서 toAlpha로 fadeTime 동안 변화시키는 코루틴
    IEnumerator FadeAlpha(float fromAlpha, float toAlpha, float fadeTime)
    {
        Color color = targetImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(fromAlpha, toAlpha, elapsedTime / fadeTime);
            color.a = newAlpha;
            targetImage.color = color;

            yield return null;  // 다음 프레임까지 대기
        }

        // 마지막 알파값 정확히 설정
        color.a = toAlpha;
        targetImage.color = color;
    }

}
