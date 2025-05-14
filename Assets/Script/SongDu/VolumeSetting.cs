using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting: MonoBehaviour
{
    [Header("UI")]
    public Slider volumeSlider;      // ���� Slider

    [Header("Audio")]
    public AudioSource audioSource;  // ���� ������ AudioSource

    void Start()
    {
        // ���� �� Slider�� AudioSource �ʱⰪ ����ȭ
        volumeSlider.value = audioSource.volume;

        // �����̴� ���� �ٲ� �� AudioSource.volume�� ���� ����
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float value)
    {
        Debug.Log($"[VolumeSetting] SetVolume called with {value}");
        audioSource.volume = value;
    }

}
