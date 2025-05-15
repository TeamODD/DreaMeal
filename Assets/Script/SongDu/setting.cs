using UnityEngine;
using UnityEngine.UI;

public class setting : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;     // MainMenuPanel 오브젝트
    public GameObject optionPanel;     // SettingsPanel 오브젝트

    [Header("Settings UI")]
    public Button continueButton;        // ContinueButton 컴포넌트
    public GameObject AudioSlider;// AudioSlider 오브젝트 (슬라이더 + 라벨 등)

    void Awake()
    {
        // 시작 시 설정 패널은 꺼두기
        optionPanel.SetActive(false);
        continueButton.gameObject.SetActive(false);
        AudioSlider.SetActive(true);
    }

    // "설정" 버튼 OnClick() 에 연결
    public void OnSettingsClicked()
    {
        // 메인 메뉴는 숨기고
        mainPanel.SetActive(false);

        // 설정 패널 보여주기
        optionPanel.SetActive(true);

        // 하위 옵션(계속하기, 오디오 슬라이더)도 활성화
        continueButton.gameObject.SetActive(true);
        AudioSlider.SetActive(true);
    }

    // "계속하기" 버튼 OnClick() 에 연결
    public void OnContinueClicked()
    {
        // 설정 패널 숨기고 메인 메뉴 복귀
        optionPanel.SetActive(true);
        mainPanel.SetActive(true);
    }
}
