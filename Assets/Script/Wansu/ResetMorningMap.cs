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
    public Image blackImg;
    public Image background;
    public float duration = 3f;
    public float blackDuration = 1f;
    public Text text;
    public IEnumerator FadeOut(Image img, Text text)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            img.color = new Color(1, 1, 1, 1 - t / duration);
            text.color = new Color(0, 0, 0, 1 - t / duration);
            yield return null;
        }
        img.color = new Color(1, 1, 1, 0);
        text.color = new Color(0, 0, 0, 0);
        resetUi.SetActive(false);
    }
    public IEnumerator FadeIn(Image img)
    {
        for (float t = 0; t < blackDuration; t += Time.deltaTime)
        {
            img.color = new Color(0, 0, 0, 1 - t / blackDuration);
            yield return null;
        }
        img.color = new Color(0, 0, 0, 0);
        blackImg.gameObject.SetActive(false);
        StartCoroutine(FadeOut(background, text));
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
        resetUi.SetActive(true);
        MorningManager.Instance.NextDay();
        text.text = MorningManager.Instance.date + "일차 낮";
        StartCoroutine(FadeIn(blackImg));
    }
}
