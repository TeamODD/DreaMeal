using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class OpeningEndingSystem : MonoBehaviour
{
    public GameObject opening;
    public GameObject ending;
    
    void Start()
    {
        if (MorningManager.Instance == null)
        {
            opening.SetActive(true);
        }
        else
        {
            ending.SetActive(true);
        }
    }
}
