using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting: MonoBehaviour
{
    [Header("UI")]
    public Slider volumeSlider;      // 씬의 Slider

    [Header("Audio")]
    public AudioSource audioSource;  // 볼륨 조절할 AudioSource

    void Start()
    {
        // 시작 시 Slider와 AudioSource 초기값 동기화
        volumeSlider.value = audioSource.volume;

        // 슬라이더 값이 바뀔 때 AudioSource.volume에 직접 대입
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float value)
    {
        Debug.Log($"[VolumeSetting] SetVolume called with {value}");
        audioSource.volume = value;
    }

}
