using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DailyNpcText
{
    public List<string> npcText;
}
public class NpcUi : MonoBehaviour
{
    public Text text;
    public GameObject yesBtn;
    public GameObject noBtn;
    public GameObject npcNextBtn;
    public List<GameObject> chooses;
    public VillageBackgroundManager villageManager;
    public StoryManager stm;
    private Npc npc;
    public TypingEffect te;
    public DailyNpcText[] dailyNpcText;
    public string npcName;
    private string beforeYesButton;
    private int index = 0;
    private List<string> nowText;
    private string yesButton = "어떤 조언을 할 것인가요?";
    private float[] yPositions = { 150f, 80f, 10f };

    void Start()
    {
        beforeYesButton = npcName + "가 재난이 닥칠 사람일까요?";
    }
    public void SetNpc(Npc npcObj)
    {
        npc = npcObj;
    }
    void OnEnable()
    {
        yesBtn.SetActive(false);
        noBtn.SetActive(false);
        npcNextBtn.SetActive(false);
        nowText = dailyNpcText[MorningManager.Instance.date - 1].npcText;
        index = 0;
        StartCoroutine(te.TypeDialog(text, nowText[index], new List<GameObject> { npcNextBtn }));
    }
    public void OnNpcNextClicked()
    {
        index++;
        npcNextBtn.SetActive(false);
        if (index < nowText.Count)
        {
            string str = nowText[index].Replace("\\n", "\n");
            StartCoroutine(te.TypeDialog(text, str, new List<GameObject> { npcNextBtn }));
        }
        if (index == nowText.Count)
        {
            npcNextBtn.SetActive(false);
            StartCoroutine(te.TypeDialog(text, beforeYesButton, new List<GameObject> { yesBtn, noBtn }));
        }
    }
    public void OnYesClicked()
    {
        yesBtn.SetActive(false);
        noBtn.SetActive(false);
        MixRandomChocies();
        for (int i = 0; i < 3; ++i)
        {
            RectTransform rt = chooses[i].GetComponent<RectTransform>();
            Vector2 pos = rt.anchoredPosition;
            pos.y = yPositions[i];
            rt.anchoredPosition = pos;
        }
        StartCoroutine(te.TypeDialog(text, yesButton, chooses));
    }
    public void OnNoClicked()
    {
        villageManager.ZoomOutCamera();
        npc.ResetChoose();
        gameObject.SetActive(false);
    }
    public void MixRandomChocies()
    {
        for (int i = chooses.Count - 1; i > 0; --i)
        {
            int rnd = Random.Range(0, i);
            GameObject temp = chooses[i];
            chooses[i] = chooses[rnd];
            chooses[rnd] = temp;
        }
    }
    public void OnChoiceButtonClicked(int type)
    {
        stm.gameObject.SetActive(true);
        foreach (GameObject choose in chooses) 
        { 
            choose.SetActive(false); 
        }
        stm.OnNpcChoice(npc, this, type);
    }
}