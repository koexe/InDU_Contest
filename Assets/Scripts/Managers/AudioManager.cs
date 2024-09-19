using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // 싱글턴 인스턴스
    public static AudioManager instance;

    // BGM과 SE 용 오디오 소스
    public AudioSource bgmSource;
    public AudioSource seSource;

    private void Awake()
    {
        // 싱글턴 패턴 구현
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGM 재생 함수
    public void PlayBGM(AudioClip bgmClip, float volume = 1f)
    {
        // 실행 중인 BGM이 있다면 정지
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }

        // 새 BGM을 재생
        if (bgmSource != null && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.volume = volume;
            bgmSource.loop = true; // BGM은 반복 재생 설정
            bgmSource.Play();
        }
    }

    // BGM 정지 함수
    public void StopBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    // SE 재생 함수
    public void PlaySE(AudioClip seClip, float volume = 1f)
    {
        // 실행 중인 SE가 있다면 정지
        if (seSource.isPlaying)
        {
            seSource.Stop();
        }

        // 새 SE를 재생
        if (seSource != null && seClip != null)
        {
            seSource.PlayOneShot(seClip, volume); // PlayOneShot은 중첩 가능
        }
    }

    // SE 정지 함수
    public void StopSE()
    {
        if (seSource != null && seSource.isPlaying)
        {
            seSource.Stop();
        }
    }
}