using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    public TextShower(MonoBehaviour monoBehaviour, Text text, string stringPeice, float showTime)
    {
        this.monoBehaviour = monoBehaviour;
        this.text = text;
        this.stringPeice = stringPeice;
        this.showTime = showTime;
    }
    public void Update()
    {
        if (isRun == false)
            return;

        showTime -= Time.deltaTime;
        if (showTime <= 0)
        {
            monoBehaviour.StartCoroutine(FadeInText());
            showTime = 0.0f;
            isRun = false;
        }
    }
    public void PrintShowTime()
    {
        Debug.Log(showTime);
    }
    public bool IsRun() { return isRun; }
    public float GetShowTime() { return showTime; }
    private IEnumerator FadeInText()
    {
        text.text = stringPeice;
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
    private MonoBehaviour monoBehaviour = null;
    private Text text = null;
    private string stringPeice = null;
    private float showTime = 0.0f;
    private float fadeDuration = 3.0f;
    private bool isRun = true;
}

public class TextSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        generateTimeMulSet = new HashSet<int>();
        while (generateTimeMulSet.Count < textMesh.Length)
            generateTimeMulSet.Add(Random.Range(0, textMesh.Length) + 1);


        stringPeiceBundles = new List<StringPeiceBundle>(renderStrings.Length);
        foreach (string str in renderStrings)
        {
            stringPeiceBundles.Add(new StringPeiceBundle(str));
        }

        int renderString = 2;

        string[] peices = stringPeiceBundles[renderString].peices;
        textShowers = new List<TextShower>(peices.Length);
        for (int i = 0; i < peices.Length; i++)
        {
            int setElement = generateTimeMulSet.First();
            generateTimeMulSet.Remove(setElement);
            if (setElement == 0)
            {
                Debug.Log(setElement);
            }
            textShowers.Add(new TextShower(this, textMesh[i], peices[i], generateTime * setElement));
        }
    }

    private void Update()
    {
        if (isSafeHome == false)
            return;

        GetMin();

        foreach (TextShower textShower in textShowers)
            textShower.Update();
    }

    private void GetMin()
    {
        int minIdx = 0;
        for (int i = 0; i < textShowers.Count; i++) 
        {
            if (textShowers[i].IsRun() == false)
                continue;
            if (textShowers[i].GetShowTime() < textShowers[minIdx].GetShowTime())
                minIdx = i;
        }
        generateTimeText.text = textShowers[minIdx].GetShowTime().ToString();
    }
    public void IsSafeHome(bool isSafeHome)
    {
        this.isSafeHome = isSafeHome;
    }

    public Text[] textMesh;
    private List<TextShower> textShowers = null;

    public string[] renderStrings;
    private List<StringPeiceBundle> stringPeiceBundles = null;

    public float generateTime;
    private HashSet<int> generateTimeMulSet = null;

    public Text generateTimeText;
    private bool isSafeHome = true;
}
