using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject endUi;
    public GameObject map;
    public TypingEffect te;
    public GradationAnimate gradationAnimate;
    public Text text;
    public DailyStory[] stories;
    public Npc[] correctNpc;
    private List<Sprite> nowStoryImages;
    private string[] nowStoryTexts;
    private int storyIndex = 0;
    private int currentDay;
    private bool isCorrect;
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
                isCorrect = false;
            }
            else if (type == 2) // 2번 오답
            {
                nowStoryImages = stories[currentDay - 1].n2IncorrectImages;
                nowStoryTexts = stories[currentDay - 1].n2IncorrectTexts;
                isCorrect = false;
            }
            else // 정답
            {
                nowStoryImages = stories[currentDay - 1].correctImages;
                nowStoryTexts = stories[currentDay - 1].correctTexts;
                MorningManager.Instance.correctCount++;
                isCorrect = true;
            }
        }
        else // 올지 않은 npc 선택
        {
            nowStoryImages = stories[currentDay - 1].diffrentNpcImages;
            nowStoryTexts = stories[currentDay - 1].diffrentNpcText;
            isCorrect = false;
        }
        if (!isCorrect)
        {
            MorningManager.Instance.NpcNames.Add(correctNpc[currentDay - 1].name);
        }
        storyImage.gameObject.SetActive(true);
        storyIndex = 0;
        string str = nowStoryTexts[storyIndex].Replace("\\n", "\n");
        StartCoroutine(gradationAnimate.FadeImageWhite(storyImage, nowStoryImages[storyIndex]));
        StartCoroutine(te.TypeDialog(text, str, new List<GameObject> { nextBtn })); 
    }
    public void OnNextClicked()
    {
        storyIndex++;
        nextBtn.SetActive(false);
        if (storyIndex < nowStoryTexts.Length)
        {
            string str = nowStoryTexts[storyIndex].Replace("\\n", "\n");
            if (storyIndex == nowStoryTexts.Length - 1)
            {
                StartCoroutine(gradationAnimate.FadeImageWhite(storyImage, nowStoryImages[storyIndex]));
                StartCoroutine(te.TypeDialog(text, str, new List<GameObject> { endBtn }));
            }
            else
            {
                StartCoroutine(gradationAnimate.FadeImageWhite(storyImage, nowStoryImages[storyIndex]));
                StartCoroutine(te.TypeDialog(text, str,  new List<GameObject> { nextBtn }));
            }
        }
    }
    public void OnEndClicked()
    {
        npc.villageManager.ResetCamera();
        npc.textUi.gameObject.SetActive(false);
        npc.ResetChoose();
        storyImage.sprite = null;
        map.SetActive(true);
        endUi.SetActive(true);
        gameObject.SetActive(false);
        SceneManager.LoadScene("JiHunScene");
    }
}