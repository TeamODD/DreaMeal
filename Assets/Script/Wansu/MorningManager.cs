using System.Collections.Generic;
using UnityEngine;

public class MorningManager : MonoBehaviour
{
    public static MorningManager Instance;
    public GameObject map;
    public List<GameObject> villages;
    public List<GameObject> uis;
    public StoryManager stm;
    public int date = 0;
    public int correctCount = 0;
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
        stm.gameObject.SetActive(false);
        NextDay();
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void NextDay()
    {
        date++;
    }
}
