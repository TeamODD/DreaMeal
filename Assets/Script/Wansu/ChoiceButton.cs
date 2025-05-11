using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    public Text choiceText;
    public string[] choiceStr;
    // 정답 : 0 / 오답 : 1, 2
    public int type;
    void OnEnable()
    {
        choiceText.text = choiceStr[MorningManager.Instance.date - 1];
    }
    public void Choiced()
    {
        GetComponentInParent<NpcUi>().OnChoiceButtonClicked(type);
    }
}