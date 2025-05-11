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
    }

    public Text GetText() { return text; }
    public string GetStringPeice() { return stringPeice; }

    private Text text = null;
    private string stringPeice = null;
}

public class TextSystem : MonoBehaviour
{
    private void Update()
    {
        if (isRun == false)
            return;

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
            StartCoroutine(FadeInText(text, stringPeice));

            currentProcessTime = generateTime;
        }

        if (randomSet.Count == 0)
            isRun = false;

    }

    private IEnumerator FadeInText(Text text, string stringPeice)
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
            randomSet.Add(Random.Range(0, textShowers.Count));

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

    public Text[] textMesh;
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

    private bool isSafeHome = true;

    public float fadeDuration = 3.0f;

    private bool isRun = false;
}
