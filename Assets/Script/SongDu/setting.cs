using UnityEngine;
using UnityEngine.UI;

public class setting : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;     // MainMenuPanel ������Ʈ
    public GameObject optionPanel;     // SettingsPanel ������Ʈ

    [Header("Settings UI")]
    public Button continueButton;        // ContinueButton ������Ʈ
    public GameObject audioSlider;       // AudioSlider ������Ʈ (�����̴� + �� ��)

    void Awake()
    {
        // ���� �� ���� �г��� ���α�
        optionPanel.SetActive(false);
        continueButton.gameObject.SetActive(false);
        audioSlider.SetActive(false);
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
        audioSlider.SetActive(true);
    }

    // "����ϱ�" ��ư OnClick() �� ����
    public void OnContinueClicked()
    {
        // ���� �г� ����� ���� �޴� ����
        optionPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
