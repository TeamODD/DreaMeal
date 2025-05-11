using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DailyStory
{
    public List<Sprite> correctImages;
    public List<Sprite> n1IncorrectImages;
    public List<Sprite> n2IncorrectImages;
    public List<Sprite> diffrentNpcImages;
    public string[] correctTexts;
    public string[] n1IncorrectTexts;
    public string[] n2IncorrectTexts;
    public string[] diffrentNpcText;
}
public class StoryManager : MonoBehaviour
{
    public TypingEffect te;
    public GradationAnimate gradationAnimate;
    public Text text;
    public DailyStory[] stories;
    public Npc[] correctNpc;
    private List<Sprite> nowStoryImages;
    private string[] nowStoryTexts;
    private int storyIndex = 0;
    private int currentDay;
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
    public void OnNpcChoice(Npc chooseNpc, NpcUi ui, int type)
    {
        ui.gameObject.SetActive(false);
        npc = chooseNpc;
        if (correctNpc[currentDay - 1] == chooseNpc) // 올바른 npc 선택
        {
            if (type == 1) // 1번 오답
            {
                nowStoryImages = stories[currentDay - 1].n1IncorrectImages;
                nowStoryTexts = stories[currentDay - 1].n1IncorrectTexts;
            }
            else if (type == 2) // 2번 오답
            {
                nowStoryImages = stories[currentDay - 1].n2IncorrectImages;
                nowStoryTexts = stories[currentDay - 1].n2IncorrectTexts;
            }
            else // 정답
            {
                nowStoryImages = stories[currentDay - 1].correctImages;
                nowStoryTexts = stories[currentDay - 1].correctTexts;
                MorningManager.Instance.correctCount++;
            }
        }
        else // 올지 않은 npc 선택
        {
            nowStoryImages = stories[currentDay - 1].diffrentNpcImages;
            nowStoryTexts = stories[currentDay - 1].diffrentNpcText;
        }
        storyImage.gameObject.SetActive(true);
        storyIndex = 0;
        //storyImage.sprite = nowStoryImages[storyIndex];
        StartCoroutine(gradationAnimate.FadeImageWhite(storyImage, nowStoryImages[storyIndex]));
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
                // storyImage.sprite = nowStoryImages[storyIndex];
                StartCoroutine(gradationAnimate.FadeImageWhite(storyImage, nowStoryImages[storyIndex]));
                StartCoroutine(te.TypeDialog(text, nowStoryTexts[storyIndex], new List<GameObject> { endBtn }));
            }
            else
            {
                // storyImage.sprite = nowStoryImages[storyIndex];
                StartCoroutine(gradationAnimate.FadeImageWhite(storyImage, nowStoryImages[storyIndex]));
                StartCoroutine(te.TypeDialog(text, nowStoryTexts[storyIndex]));
            }
        }
    }
    public void OnEndClicked()
    {
        npc.villageManager.ResetCamera();
        npc.textUi.gameObject.SetActive(false);
        npc.ResetChoose();
        storyImage.sprite = null;
        gameObject.SetActive(false);
    }
}