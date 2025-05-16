using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class OpeningEndingSystem : MonoBehaviour
{
    public static OpeningEndingSystem Instance;
    public GameObject opening;
    public GameObject ending;
    public GameObject openingBGM;
    public GameObject endingBGM;
    public GameObject allEndingBGM;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (MorningManager.Instance == null)
        {
            openingBGM.SetActive(true);
            opening.SetActive(true);
        }
        else
        {
            if (MorningManager.Instance.correctCount == 4)
            {
                allEndingBGM.SetActive(true);
            }
            else
            {
                endingBGM.SetActive(true);
            }
            ending.SetActive(true);
        }
    }
}
