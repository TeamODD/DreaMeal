using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    public bool isCorrect;
    public void Choiced()
    {
        GetComponentInParent<NpcUi>().OnChoiceButtonClicked(isCorrect);
    }
}