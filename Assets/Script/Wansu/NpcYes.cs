using System.Runtime.CompilerServices;
using UnityEngine;

public class NpcYes : MonoBehaviour
{
    public GameObject noBtn;

    public void OnYesClicked()
    {
        gameObject.SetActive(false);
        noBtn.SetActive(false);
    }
}