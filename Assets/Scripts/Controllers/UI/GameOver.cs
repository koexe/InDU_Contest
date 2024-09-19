using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : PopUpUI
{
    public Image targetImage;  // ���İ��� ������ �̹���
    public float fadeDuration = 1.5f;  // ���̵� ���� �ð�
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

    // ���İ��� 50 -> 255 -> 0���� �����ϴ� �ڷ�ƾ
    IEnumerator FadeInAndOut()
    {
        // ���İ��� 50���� 255��
        yield return StartCoroutine(FadeAlpha(50f / 255f, 1f, fadeDuration));

        // ���İ��� 255���� 0����
        yield return StartCoroutine(FadeAlpha(1f, 0f, fadeDuration));
        DeleteUI();

    }

    // ���İ��� fromAlpha���� toAlpha�� fadeTime ���� ��ȭ��Ű�� �ڷ�ƾ
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

            yield return null;  // ���� �����ӱ��� ���
        }

        // ������ ���İ� ��Ȯ�� ����
        color.a = toAlpha;
        targetImage.color = color;
    }

}
