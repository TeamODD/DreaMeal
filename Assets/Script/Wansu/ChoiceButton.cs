using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    // 정답 : 0 / 오답 : 1, 2
    public int type;
    public void Choiced()
    {
        GetComponentInParent<NpcUi>().OnChoiceButtonClicked(type);
    }
}