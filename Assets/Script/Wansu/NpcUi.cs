using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcUi : MonoBehaviour
{
    public Text text;
    public GameObject yesBtn;
    public GameObject noBtn;
    public List<GameObject> chooses;
    public VillageBackgroundManager villageManager;
    public StoryManager stm;
    private Npc npc;
    public TypingEffect te;
    public string[] dialogues;
    private int currentIndex = 0;
    private float[] yPositions = { 150f, 80f, 10f };

    public void SetNpc(Npc npcObj)
    {
        npc = npcObj;
    }
    void OnEnable()
    {
        currentIndex = 0;
        yesBtn.SetActive(false);
        noBtn.SetActive(false);
        StartCoroutine(te.TypeDialog(text, dialogues[currentIndex], new List<GameObject> { yesBtn, noBtn }));
    }
    public void OnYesClicked()
    {
        yesBtn.SetActive(false);
        noBtn.SetActive(false);
        currentIndex++;
        for (int i = 0; i < 3; ++i)
        {
            RectTransform rt = chooses[i].GetComponent<RectTransform>();
            Vector2 pos = rt.anchoredPosition;
            pos.y = yPositions[i];
            rt.anchoredPosition = pos;
        }
        StartCoroutine(te.TypeDialog(text, dialogues[currentIndex], chooses));
    }
    public void OnNoClicked()
    {
        villageManager.ZoomOutCamera();
        npc.ResetChoose();
        gameObject.SetActive(false);
    }
    
    public void OnChoiceButtonClicked(bool isCorrect)
    {
        stm.gameObject.SetActive(true);
        foreach (GameObject choose in chooses) 
        { 
            choose.SetActive(false); 
        }
        stm.OnNpcChoice(npc, isCorrect, this);
    }
}