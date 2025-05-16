using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class Ending : MonoBehaviour
{
    private bool flag;
    public float duration = 2f;
    public float bossDuration = 0.5f;
    public Image blackImg;
    public Image textView;
    public TypingEffect te;
    public GradationAnimate gradationAnimate;
    public Text text;
    public List<Sprite> endingImages;
    public string[] endingTexts;
    public List<Sprite> allCorrectEndingImages;
    public string[] allCorrectEndingTexts;
    public List<Sprite> nowImages;
    public string[] nowTexts;
    private int storyIndex = 0;
    public Image storyImage;
    public GameObject endBtn;
    public GameObject nextBtn;
    public Image boss;
    public List<Sprite> bossImg;
    public List<Text> texts;
    private List<Vector2> basePosition = new List<Vector2>();
    private bool startWord = false;
    private float[] sinValues;
    public float newTime = 0f;
    private float nowTime = 0f;
    private int nowIndx = 0;
    void OnEnable()
    {
        StartCoroutine(StartStory(blackImg));
        nowTime = newTime;
        storyIndex = 0;
        sinValues = new float[texts.Count];
        startWord = false;
        text.gameObject.SetActive(true);
        nextBtn.SetActive(false);
        endBtn.SetActive(false);
        boss.gameObject.SetActive(false);
        foreach (Text text in texts)
        {
            basePosition.Add(new Vector2(text.rectTransform.anchoredPosition.x, text.rectTransform.anchoredPosition.y));
            text.gameObject.SetActive(false);
        }
        if (MorningManager.Instance.correctCount == 4)
            {
                flag = true;
                boss.sprite = bossImg[1];
                nowImages = allCorrectEndingImages;
                nowTexts = allCorrectEndingTexts;
            }
            else
            {
                flag = false;
                boss.sprite = bossImg[0];
                nowImages = endingImages;
                nowTexts = endingTexts;
            }
        string str = nowTexts[storyIndex].Replace("\\n", "\n");
        StartCoroutine(gradationAnimate.FadeImage(textView, null));
        StartCoroutine(gradationAnimate.FadeImage(storyImage, nowImages[storyIndex]));
        StartCoroutine(te.TypeDialog(text, str, new List<GameObject> { nextBtn }));
    }
    void Update()
    {
        if (startWord)
        {
            nowTime -= Time.deltaTime;
            for (int i = 0; i < texts.Count; ++i)
            {
                MoveEffect(i);
            }
            if (nowTime < 0)
            {
                if (nowIndx == texts.Count)
                {
                    if (!endBtn.activeSelf)
                    {
                        endBtn.SetActive(true);
                    }
                    return;
                }
                texts[nowIndx].gameObject.SetActive(true);
                StartCoroutine(FadeInText(texts[nowIndx], 1f));
                ++nowIndx;
                nowTime = newTime;
            }
        }
    }
    public void MoveEffect(int index)
    {
        sinValues[index] += Time.deltaTime * UnityEngine.Random.Range(1, 4);
        float y = basePosition[index].y + Mathf.Sin(sinValues[index]) * 2.5f;

        // RectTransform을 통해 위치 조정
        texts[index].rectTransform.anchoredPosition = new Vector2(texts[index].rectTransform.anchoredPosition.x, y);

    }
    public void OnNextClicked()
    {
        storyIndex++;
        nextBtn.SetActive(false);
        if (storyIndex < nowTexts.Length)
        {
            if (storyIndex == 2)
            {
                StartCoroutine(ViewBoss(boss));
            }
            if (flag && storyIndex == 4)
            {
                boss.gameObject.SetActive(false);
            }
            if (!flag && storyIndex == 7)
            {
                boss.gameObject.SetActive(false);
            }
            string str = nowTexts[storyIndex].Replace("\\n", "\n");
            if (str == "")
            {
                textView.gameObject.SetActive(false);
                if (flag)
                    OpeningEndingSystem.Instance.allEndingBGM.SetActive(false);
            }
            if (storyIndex == nowTexts.Length - 1)
            {
                if (!flag)
                { // 밤 단어 나오는 듯이 구현
                    StartCoroutine(gradationAnimate.FadeImage(storyImage, nowImages[storyIndex]));
                    startWord = true;
                }
                else
                {
                    StartCoroutine(gradationAnimate.FadeImage(storyImage, nowImages[storyIndex]));
                    StartCoroutine(te.TypeDialog(text, str, new List<GameObject> { endBtn }));
                }
            }
            else
            {
                StartCoroutine(gradationAnimate.FadeImage(storyImage, nowImages[storyIndex]));
                StartCoroutine(te.TypeDialog(text, str, new List<GameObject> { nextBtn }));
            }
        }
    }
    public IEnumerator FadeInText(Text text, float fadeDuration)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0); // 투명하게 초기화
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / fadeDuration;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha); // 점점 밝게
            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
    }
    public IEnumerator ViewBoss(Image img)
    {
        img.gameObject.SetActive(true);
        for (float t = 0; t < bossDuration; t += Time.deltaTime)
        {
            img.color = new Color(1, 1, 1, t / bossDuration);
            yield return null;
        }
        img.color = new Color(1, 1, 1, 1);
    }
    public IEnumerator StartStory(Image img)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            img.color = new Color(0, 0, 0, 1 - t / duration);
            yield return null;
        }
        img.color = new Color(0, 0, 0, 0);
        img.gameObject.SetActive(false);
    }
    public IEnumerator EndGame(Image img)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            img.color = new Color(0, 0, 0, t / duration);
            yield return null;
        }
        img.color = new Color(0, 0, 0, 1);
        SceneManager.LoadScene("SongduScenes");
    }
    public void OnEndClicked()
    {
        blackImg.gameObject.SetActive(true);
        StartCoroutine(EndGame(blackImg));
    }
}