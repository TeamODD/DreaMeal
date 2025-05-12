using System.Collections.Generic;
using UnityEngine;

public class MorningManager : MonoBehaviour
{
    public static MorningManager Instance;
    public List<string> NpcNames;
    public int date = 0;
    public int correctCount = 0;
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
