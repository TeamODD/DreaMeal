using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NpcUi : MonoBehaviour
{
    public Text text;
    public List<Sprite> correctImg;
    public List<Sprite> incorrectImg;
    public Image storyImage;
    public GameObject yesBtn;
    public GameObject noBtn;
    public GameObject nextBtn;
    public GameObject endBtn;
    public List<GameObject> chooses;
    public VillageBackgroundManager villageManager;
    private Npc npc;
    public string[] correctStroy;
    public string[] incorrectStroy;
    public string[] dialogues;
    private int currentIndex = 0;
    private int storyIndex = 0;
    public int charPerSeconds = 15;
    private bool isTyping = false;
    private bool skipTyping = false;
    private bool isCorrect;
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
        nextBtn.SetActive(false);
        endBtn.SetActive(false);
        StartCoroutine(TypeDialog(dialogues[currentIndex], new List<GameObject> { yesBtn, noBtn }));
    }
    public IEnumerator TypeDialog(string str, List<GameObject> btns = null)
    {
        isTyping = true;
        skipTyping = false;
        text.text = "";
        foreach (var character in str.ToCharArray())
        {
            if (skipTyping)
            {
                text.text = str;
                break;
            }
            text.text += character;
            yield return new WaitForSeconds(1f / charPerSeconds);
        }
        isTyping = false;
        if (btns != null)
        {
            foreach (GameObject btn in btns)
            {
                btn.SetActive(true);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                skipTyping = true;
            }
        }
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
        StartCoroutine(TypeDialog(dialogues[currentIndex], chooses));
    }
    public void OnNoClicked()
    {
        villageManager.ZoomOutCamera();
        npc.ResetChoose();
        gameObject.SetActive(false);
    }
    public void OnNextClicked()
    {
        storyIndex++;
        if (storyIndex < correctStroy.Length)
        {
            if (storyIndex == correctStroy.Length - 1)
            {
                nextBtn.SetActive(false);
                if (isCorrect)
                {
                    storyImage.sprite = correctImg[storyIndex];
                    StartCoroutine(TypeDialog(correctStroy[storyIndex], new List<GameObject> { endBtn }));
                }
                else
                {
                    storyImage.sprite = incorrectImg[storyIndex];
                    StartCoroutine(TypeDialog(incorrectStroy[storyIndex], new List<GameObject> { endBtn }));
                }
            }
            else
            {
                if (isCorrect)
                {
                    storyImage.sprite = correctImg[storyIndex];
                    StartCoroutine(TypeDialog(correctStroy[storyIndex]));
                }
                else
                {
                    storyImage.sprite = incorrectImg[storyIndex];
                    StartCoroutine(TypeDialog(incorrectStroy[storyIndex]));
                } 
            }
        }
    }
    public void OnCorrectClicked()
    {
        foreach (GameObject choose in chooses)
        {
            choose.SetActive(false);
        }
        isCorrect = true;
        storyImage.gameObject.SetActive(true);
        storyIndex = 0;
        storyImage.sprite = correctImg[storyIndex];
        StartCoroutine(TypeDialog(correctStroy[storyIndex], new List<GameObject> { nextBtn }));
    }
    public void OnIncorrectClicked()
    {
        foreach (GameObject choose in chooses)
        {
            choose.SetActive(false);
        }
        isCorrect = false;
        storyImage.gameObject.SetActive(true);
        storyIndex = 0;
        storyImage.sprite = incorrectImg[storyIndex];
        StartCoroutine(TypeDialog(incorrectStroy[storyIndex], new List<GameObject> { nextBtn }));
    }
    public void OnEndClicked()
    {
        villageManager.ZoomOutCamera();
        npc.ResetChoose();
        npc.Finish();
        gameObject.SetActive(false);
    }
}