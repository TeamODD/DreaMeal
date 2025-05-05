using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public GameObject textUi;
    void OnMouseDown()
    {
        textUi.SetActive(true);
    }
    void OnMouseEnter()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    void OnMouseExit()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}