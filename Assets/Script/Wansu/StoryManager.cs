using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DailyStory
{
    public List<Sprite> correctImages;
    public List<Sprite> incorrectImages;
    public string[] correctTexts;
    public string[] incorrectTexts;
}
public class StoryManager : MonoBehaviour
{
    public TypingEffect te;
    public Text text;
    public DailyStory[] stories;
    public Npc[] correctNpc;
    private List<Sprite> nowStoryImages;
    private string[] nowStoryTexts;
    private int storyIndex = 0;
    private int currentDay;
    private bool isCorrect = false;
    public Image storyImage;
    public GameObject nextBtn;
    public GameObject endBtn;
    private Npc npc;
    void Awake()
    {
        storyIndex = 0;
        currentDay = MorningManager.Instance.date;
        nextBtn.SetActive(false);
        endBtn.SetActive(false);
    }
    void OnEnable()
    {
        storyIndex = 0;
        nextBtn.SetActive(false);
        endBtn.SetActive(false);
    }
    public void OnNpcChoice(Npc chooseNpc, bool isChoiceCorrect, NpcUi ui)
    {
        ui.gameObject.SetActive(false);
        npc = chooseNpc;
        if (isChoiceCorrect)
        {
            if (correctNpc[currentDay - 1] == chooseNpc)
            { // 정답일 때
                isCorrect = true;
                nowStoryImages = stories[currentDay - 1].correctImages;
                nowStoryTexts = stories[currentDay - 1].correctTexts;
                MorningManager.Instance.correctCount++;
            }
            else
            { // 오답일 때
                isCorrect = false;
                nowStoryImages = stories[currentDay - 1].incorrectImages;
                nowStoryTexts = stories[currentDay - 1].incorrectTexts;
            }
        }
        else
        { // 오답일 때
            isCorrect = false;
            nowStoryImages = stories[currentDay - 1].incorrectImages;
            nowStoryTexts = stories[currentDay - 1].incorrectTexts;
        }
        storyImage.gameObject.SetActive(true);
        storyIndex = 0;
        storyImage.sprite = nowStoryImages[storyIndex];
        StartCoroutine(te.TypeDialog(text, nowStoryTexts[storyIndex], new List<GameObject> { nextBtn })); 
    }
    public void OnNextClicked()
    {
        storyIndex++;
        if (storyIndex < nowStoryTexts.Length)
        {
            if (storyIndex == nowStoryTexts.Length - 1)
            {
                nextBtn.SetActive(false);
                storyImage.sprite = nowStoryImages[storyIndex];
                StartCoroutine(te.TypeDialog(text, nowStoryTexts[storyIndex], new List<GameObject> { endBtn }));
            }
            else
            {
                storyImage.sprite = nowStoryImages[storyIndex];
                StartCoroutine(te.TypeDialog(text, nowStoryTexts[storyIndex]));
            }
        }
    }
    public void OnEndClicked()
    {
        npc.villageManager.ZoomOutCamera();
        npc.textUi.gameObject.SetActive(false);
        npc.ResetChoose();
        gameObject.SetActive(false);
    }
}
