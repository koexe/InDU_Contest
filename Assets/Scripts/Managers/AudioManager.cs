using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgmSource;
    public AudioSource seSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
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
            bgmSource.loop = true; 
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    public void PlaySE(AudioClip seClip, float volume = 1f)
    {
        if (seSource.isPlaying)
        {
            seSource.Stop();
        }

        // �� SE�� ���
        if (seSource != null && seClip != null)
        {
            seSource.PlayOneShot(seClip, volume); 
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

    public void SetVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = volume;
        }

        if (seSource != null)
        {
            seSource.volume = volume;
        }
    }
}