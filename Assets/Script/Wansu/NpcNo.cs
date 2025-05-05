using UnityEngine;

public class NpcNo : MonoBehaviour
{
    public GameObject npcUi;
    public void OnNoClicked()
    {
        npcUi.SetActive(false);
    }
}
