using System.Collections.Generic;
using UnityEngine;

public class MorningManager : MonoBehaviour
{
    public GameObject map;
    public List<GameObject> villages;
    public List<GameObject> uis;
    void Start()
    {
        map.SetActive(true);
        foreach (GameObject v in villages)
        {
            v.SetActive(false);
        }
        foreach (GameObject ui in uis)
        {
            ui.SetActive(false);
        }
    }
}
