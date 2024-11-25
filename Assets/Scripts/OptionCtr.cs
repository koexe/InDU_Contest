using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCtr : PopUpUI 
{
    public Slider volumeSlider;  // 볼륨 조절 슬라이더
    public Button applyButton;   // 적용 버튼

    private void Start()
    {
        // 슬라이더의 초기값 설정
        volumeSlider.value = AudioManager.instance.bgmSource.volume;

        // 버튼 클릭 이벤트에 적용 함수 등록
        applyButton.onClick.AddListener(ApplyVolume);
    }

    // 적용 버튼 클릭 시 호출되는 함수
    public void ApplyVolume()
    {
        float volume = volumeSlider.value;

        // AudioManager의 BGM과 SE 볼륨을 슬라이더 값으로 설정
        AudioManager.instance.SetVolume(volume);
        this.DeleteUI();
    }
}
