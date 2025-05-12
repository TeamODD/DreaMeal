using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResetMorningMap : MonoBehaviour
{
    public GameObject map;
    public List<GameObject> villages;
    public List<GameObject> uis;
    public StoryManager stm;
    public GameObject resetUi;
    public Image background;
    public float duration = 0.5f;
    public IEnumerator FadeIn(Image img)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            img.color = new Color(0, 0, 0, 1 - t / duration);
            yield return null;
        }
        img.color = new Color(0, 0, 0, 0);
        resetUi.SetActive(false);
    }
    void Start()
    {
        map.SetActive(true);
        foreach (GameObject v in villages)
        {
            v.SetActive(true);
        }
        if (MorningManager.Instance.NpcNames != null)
        {
            foreach (string name in MorningManager.Instance.NpcNames)
            {
                GameObject npc = GameObject.Find(name);
                if (npc == null) continue;
                npc.SetActive(false);
            }
        }
        foreach (GameObject ui in uis)
        {
            ui.SetActive(false);
        }
        foreach (GameObject v in villages)
        {
            v.SetActive(false);
        }
        stm.gameObject.SetActive(false);
        StartCoroutine(FadeIn(background));
        MorningManager.Instance.NextDay();
    }
}
