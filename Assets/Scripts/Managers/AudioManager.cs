using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static AudioManager instance;

    // BGM�� SE �� ����� �ҽ�
    public AudioSource bgmSource;
    public AudioSource seSource;

    private void Awake()
    {
        // �̱��� ���� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGM ��� �Լ�
    public void PlayBGM(AudioClip bgmClip, float volume = 1f)
    {
        // ���� ���� BGM�� �ִٸ� ����
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }

        // �� BGM�� ���
        if (bgmSource != null && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.volume = volume;
            bgmSource.loop = true; // BGM�� �ݺ� ��� ����
            bgmSource.Play();
        }
    }

    // BGM ���� �Լ�
    public void StopBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    // SE ��� �Լ�
    public void PlaySE(AudioClip seClip, float volume = 1f)
    {
        // ���� ���� SE�� �ִٸ� ����
        if (seSource.isPlaying)
        {
            seSource.Stop();
        }

        // �� SE�� ���
        if (seSource != null && seClip != null)
        {
            seSource.PlayOneShot(seClip, volume); // PlayOneShot�� ��ø ����
        }
    }

    // SE ���� �Լ�
    public void StopSE()
    {
        if (seSource != null && seSource.isPlaying)
        {
            seSource.Stop();
        }
    }
}