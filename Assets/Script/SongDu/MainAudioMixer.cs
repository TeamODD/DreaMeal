using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainAudioMixer : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _volumeSlider;

    private const float defualtVolume = 100;

    private void Awake()
    {
        //볼륨 슬라이더에게 기능 할당
        _volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void Start()
    {
        //저장된 값 불러오기
        _audioMixer.SetFloat("Master", GetAudioMixVolume(PlayerPrefs.GetFloat("Volume", 1f)));
        //볼륨 슬라이더 값 적용
        _volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
    }

    private float GetAudioMixVolume(float volume)
    {
        //슬라이더에 사용할 수 있도록 로그 함수로 수치 변환
        return Mathf.Log10(volume) * 20;
    }

    private void SetVolume(float value)
    {
        //오디오 믹서 값 조절 및 플레이어 프리펩스 저장
        _audioMixer.SetFloat("Master", GetAudioMixVolume(value));
        PlayerPrefs.SetFloat("Volume", _volumeSlider.value);
        PlayerPrefs.Save();
    }
}