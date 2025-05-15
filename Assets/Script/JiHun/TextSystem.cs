using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StringPeiceBundle
{
    public StringPeiceBundle(string str)
    {
        peices = str.Split(' ');
    }
    public string[] peices = null;
}
public class TextShower
{
    public TextShower(Text text, string stringPeice)
    {
        this.text = text;
        this.stringPeice = stringPeice;
        fixPosition = new Vector2(this.text.rectTransform.anchoredPosition.x, this.text.rectTransform.anchoredPosition.y);
    }

    public void MoveEffect()
    {
        sinValue += Time.deltaTime * UnityEngine.Random.Range(1, 4);
        float y = fixPosition.y + Mathf.Sin(sinValue) * 2.5f;

        // RectTransform을 통해 위치 조정
        text.rectTransform.anchoredPosition = new Vector2(text.rectTransform.anchoredPosition.x, y);
    }

    public Text GetText() { return text; }
    public string GetStringPeice() { return stringPeice; }

    private Text text = null;
    private string stringPeice = null;

    private Vector2 fixPosition = Vector2.zero;
    private float sinValue = 0.0f;
}

public class TextSystem : MonoBehaviour
{
    private void Update()
    {
        if (isRun == false)
            return;

        foreach (TextShower textShower in textShowers)
            textShower.MoveEffect();

        if (isSafeHome == false)
            return;

        currentProcessTime -= Time.deltaTime;

        if (currentProcessTime <= 0)
        {
            TextShower textShower = null;
            
            if(selectedArraySavePoint != selectedArrayTop)
            {
                int textShowerIndex = prevSelectedIndexArray[selectedArraySavePoint++];
                textShower = textShowers[textShowerIndex];
            }
            else
            {
                int textShowerIndex = randomSet.First();
                randomSet.Remove(textShowerIndex);

                textShower = textShowers[textShowerIndex];

                prevSelectedIndexArray[selectedArrayTop++] = textShowerIndex;
                selectedArraySavePoint += 1;
            }
            Text text = textShower.GetText();

            string stringPeice = textShower.GetStringPeice();
            text.color = Color.white;
            StartCoroutine(TextFader.FadeInText(text, stringPeice, fadeDuration));

            currentProcessTime = UnityEngine.Random.Range(2, generateTime);
        }

        if (randomSet.Count == 0)
            isRun = false;

    }

    public void SetRenderString(string renderString)
    {
        currentProcessTime = generateTime;

        this.renderString = renderString;
        this.stringPeiceBundle = new StringPeiceBundle(this.renderString);

        string[] peices = stringPeiceBundle.peices;

        textShowers.Clear();
        for (int i = 0; i < peices.Length; i++)
            textShowers.Add(new TextShower(textMesh[i], peices[i]));


        randomSet.Clear();
        while (randomSet.Count < textShowers.Count)
            randomSet.Add(UnityEngine.Random.Range(0, textShowers.Count));

        prevSelectedIndexArray = new int[textShowers.Count];

        selectedArrayTop = 0;
        selectedArraySavePoint = selectedArrayTop;

        isRun = true;
    }
    public void IsSafeHome(bool isSafeHome)
    {
        this.isSafeHome = isSafeHome;
    }
    public void HidePrevText()
    {
        if (selectedArraySavePoint <= 0)
            return;

        int prevIndex = prevSelectedIndexArray[--selectedArraySavePoint];
        textShowers[prevIndex].GetText().text = "";
    }

    public void ShouldRun(bool run)
    {
        isRun = run;
    }

    public Text[] textMesh = new Text[36];
    private List<TextShower> textShowers = new List<TextShower>();

    // 텍스트 뽑는 방식
    private int[] prevSelectedIndexArray = null;
    private int selectedArrayTop = 0;
    private int selectedArraySavePoint = 0;

    private string renderString = null;
    private StringPeiceBundle stringPeiceBundle = null;

    public float generateTime;
    private float currentProcessTime = 0.0f;
    private HashSet<int> randomSet = new HashSet<int>();

    public float fadeDuration;

    private bool isSafeHome = true;

    private bool isRun = false;
}
