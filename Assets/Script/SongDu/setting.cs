using UnityEngine;
using UnityEngine.UI;

public class setting : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;     // MainMenuPanel ������Ʈ
    public GameObject optionPanel;     // SettingsPanel ������Ʈ

    [Header("Settings UI")]
    public Button continueButton;        // ContinueButton ������Ʈ
    public GameObject AudioSlider;// AudioSlider ������Ʈ (�����̴� + �� ��)

    void Awake()
    {
        // ���� �� ���� �г��� ���α�
        optionPanel.SetActive(false);
        continueButton.gameObject.SetActive(false);
        AudioSlider.SetActive(true);
    }

    // "����" ��ư OnClick() �� ����
    public void OnSettingsClicked()
    {
        // ���� �޴��� �����
        mainPanel.SetActive(false);

        // ���� �г� �����ֱ�
        optionPanel.SetActive(true);

        // ���� �ɼ�(����ϱ�, ����� �����̴�)�� Ȱ��ȭ
        continueButton.gameObject.SetActive(true);
        AudioSlider.SetActive(true);
    }

    // "����ϱ�" ��ư OnClick() �� ����
    public void OnContinueClicked()
    {
        // ���� �г� ����� ���� �޴� ����
        optionPanel.SetActive(true);
        mainPanel.SetActive(true);
    }
}
