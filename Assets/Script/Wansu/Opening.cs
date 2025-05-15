using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    public float duration = 2f;
    public Image blackImg;
    public Image textView;
    public TypingEffect te;
    public GradationAnimate gradationAnimate;
    public Text text;
    public List<Sprite> openingImages;
    public string[] openingTexts;
    private int storyIndex = 0;
    public Image storyImage;
    public GameObject startBtn;
    public GameObject nextBtn;
    void OnEnable()
    {
        StartCoroutine(StartStory(blackImg));
        storyIndex = 0;
        nextBtn.SetActive(false);
        startBtn.SetActive(false);
        string str = openingTexts[storyIndex].Replace("\\n", "\n");
        StartCoroutine(gradationAnimate.FadeImage(textView, null));
        StartCoroutine(gradationAnimate.FadeImage(storyImage, openingImages[storyIndex]));
        StartCoroutine(te.TypeDialog(text, str, new List<GameObject> { nextBtn }));
    }
    public void OnNextClicked()
    {
        storyIndex++;
        nextBtn.SetActive(false);
        if (storyIndex < openingTexts.Length)
        {
            string str = openingTexts[storyIndex].Replace("\\n", "\n");
            if (storyIndex == openingTexts.Length - 1)
            {
                StartCoroutine(gradationAnimate.FadeImage(storyImage, openingImages[storyIndex]));
                StartCoroutine(te.TypeDialog(text, str, new List<GameObject> { startBtn }));
            }
            else
            {
                StartCoroutine(gradationAnimate.FadeImage(storyImage, openingImages[storyIndex]));
                StartCoroutine(te.TypeDialog(text, str, new List<GameObject> { nextBtn }));
            }
        }
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

    public IEnumerator StartGame(Image img)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            img.color = new Color(0, 0, 0, t / duration);
            yield return null;
        }
        img.color = new Color(0, 0, 0, 1);
        SceneManager.LoadScene("JiHunScene");
    }
    public void OnStartClicked()
    {
        blackImg.gameObject.SetActive(true);
        StartCoroutine(StartGame(blackImg));
    }
}