using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TextFader
{
    // text색깔은 미리 정해서
    public static IEnumerator FadeInText(Text text, string str, float fadeDuration)
    {
        text.text = str;
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

    public static IEnumerator FadeOutText(Text text, string str, float fadeDuration)
    {
        text.text = str;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f); // 투명하게 초기화
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeDuration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha); // 점점 밝게
            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0.0f);
    }
}
public class NightSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (MorningManager.Instance == null)
            date = 0;
        else
            date = MorningManager.Instance.date;

        Color src = fadeInBackGround.GetComponent<SpriteRenderer>().color;
        fadeInBackGround.GetComponent<SpriteRenderer>().color = new Color(src.r, src.g, src.b, 0.0f);

        dateShowerText.text = date.ToString() + "일차 밤";
        dateShowerText.color = Color.white;
        StartCoroutine(TextFader.FadeOutText(dateShowerText, dateShowerText.text, 3.0f));

        if (100 < ratioOfStrongMac[date])
            ratioOfStrongMac[date] = 100;
        else if (ratioOfStrongMac[date] < 0)
            ratioOfStrongMac[date] = 0;

        string renderString = renderStrings[date];
        textSystem.SetRenderString(renderString);

        userHome.myConditionIsSafe = textSystem.IsSafeHome;
        userHome.collisionWithStrongMac = textSystem.HidePrevText;

        ChangeToDream();

        realSponeTime = sponeTime[date] / 2.0f;

    }
    
    // Update is called once per frame
    void Update()
    {
        nightTime -= Time.deltaTime;
 

        if (nightTime <= 0)
        {
            nightTime = 0.0f;
            StartCoroutine(TurnOverToMorning());
        }
        timerText.text = nightTime.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isSleep)
                ChangeToWakeUp();
            else
                if (player.IsInDoor())
                    ChangeToDream();    // 이걸 저기 위에서도 쓰고있어서 무조건 이렇게
        }

        if (numberOfGenerateMac[date] <= sumOfMac)
            return;

        // 꿈 상태일 때만 맥 생성
        if (isSleep == false)
            return;

        sumTime += Time.deltaTime;
        if (sumTime > realSponeTime)
        {
            sumTime = 0.0f;
            realSponeTime = sponeTime[date];

            SpownMac(isSleep);
            sumOfMac += 1;
        }

    }

    private IEnumerator TurnOverToMorning()
    {
        yield return StartCoroutine(ImageFader.FadeInImage(fadeInBackGround.GetComponent<SpriteRenderer>(), 3.0f));
        SceneManager.LoadScene("Morning");
    }

    private void SpownMac(bool sleep)
    {
        int sponerSize = sponers.Length;
        int randomSponerIndex = Random.Range(0, sponerSize);
        Transform sponerTransform = sponers[randomSponerIndex].transform;

        Vector3 pos = new Vector3(Random.Range(3.8f, 9.0f), Random.Range(3.0f, 5.5f), 0.0f);
        int pm = Random.Range(0, 2);
        pos.x = pm == 0 ? pos.x : -pos.x;
        pm = Random.Range(0, 2);
        pos.y = pm == 0 ? pos.y : -pos.y;

        GameObject newObject = Instantiate(macPrefab, pos, Quaternion.identity);
        Mac mac = newObject.GetComponent<Mac>();

        int random = Random.Range(1, 101);
        if (random <= ratioOfStrongMac[date])
        {
            mac.InitalizeMacEvent(MacIsDie, userHome.IsCollisionWithStrongMac);
            mac.GetComponent<SpriteRenderer>().sprite = strongMacSprite;
        }
        else
            mac.InitalizeMacEvent(MacIsDie, userHome.IsCollisionWithMac);
        mac.RenderEnable(!sleep);
        mac.isSleep = sleep;
        macObjects.Add(mac);
    }

    private void MacIsDie(Mac mac)
    {
        macObjects.Remove(mac);
        countOfDieMac += 1;
    }
    private void ChangeToDream()
    {
        userHome.GetComponent<SpriteRenderer>().enabled = false;
        wakeUp.SetActive(false);
        sleep.SetActive(true);
        isSleep = true;

        // 이걸 옵저버 패턴으로 해도될듯?
        foreach (Mac mac in macObjects)
        {
            mac.RenderEnable(!isSleep);
            mac.isSleep = isSleep;
        }

        textSystem.ShouldRun(isSleep);

        for (int i = 0; i < countOfDieMac; i++)
            SpownMac(isSleep);
    }

    private void ChangeToWakeUp()
    {
        countOfDieMac = 0;
        userHome.GetComponent<SpriteRenderer>().enabled = true;
        wakeUp.SetActive(true);
        sleep.SetActive(false);
        isSleep = false;

        player.transform.position = userHome.transform.position - Vector3.up;

        foreach (Mac mac in macObjects)
        {
            mac.RenderEnable(!isSleep);
            mac.isSleep = isSleep;
        }
        textSystem.HidePrevText();
        textSystem.ShouldRun(isSleep);
    }

    public Home userHome;
    public Player player;
    public TextSystem textSystem;

    public GameObject macPrefab;
    public GameObject[] sponers;

    private List<Mac> macObjects = new List<Mac>();

    public GameObject wakeUp;
    public GameObject sleep;
    private bool isSleep = false;

    public Text timerText;
    private float sumTime = 0.0f;

    public Sprite strongMacSprite;

    public string[] renderStrings;
    public int[] numberOfGenerateMac;
    public int[] ratioOfStrongMac;
    public float[] sponeTime;
    private float realSponeTime;
    public float nightTime;

    private int date = 0;

    public Text dateShowerText;

    private int sumOfMac = 0;

    public GameObject fadeInBackGround;

    private int countOfDieMac = 0;
}
