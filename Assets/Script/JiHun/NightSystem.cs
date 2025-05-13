using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TextFader
{
    // text������ �̸� ���ؼ�
    public static IEnumerator FadeInText(Text text, string str, float fadeDuration)
    {
        text.text = str;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0); // �����ϰ� �ʱ�ȭ
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / fadeDuration;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha); // ���� ���
            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
    }

    public static IEnumerator FadeOutText(Text text, string str, float fadeDuration)
    {
        text.text = str;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f); // �����ϰ� �ʱ�ȭ
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeDuration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha); // ���� ���
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

        dateShowerText.text = (date + 1).ToString() + "���� ��";
        dateShowerText.color = Color.white;
        StartCoroutine(TextFader.FadeOutText(dateShowerText, dateShowerText.text, 3.0f));

        if (100 < ratioOfStrongMac)
            ratioOfStrongMac = 100;
        else if (ratioOfStrongMac < 0)
            ratioOfStrongMac = 0;

        string renderString = renderStrings[date];
        textSystem.SetRenderString(renderString);

        userHome.myConditionIsSafe = textSystem.IsSafeHome;
        userHome.collisionWithStrongMac = textSystem.HidePrevText;

        ChangeToDream();
    }

    // Update is called once per frame
    void Update()
    {
        nightTime -= Time.deltaTime;
        timerText.text = nightTime.ToString();

        if (nightTime <= 0)
            SceneManager.LoadScene("Morning");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isSleep)
                ChangeToWakeUp();
            else
                if (player.IsInDoor())
                    ChangeToDream();    // �̰� ���� �������� �����־ ������ �̷���
        }

        if (numberOfGenerateMac <= macObjects.Count)
            return;

        // �� ������ ���� �� ����
        if (isSleep == false)
            return;

        sumTime += Time.deltaTime;
        if (sumTime > sponeTime)
        {
            sumTime = 0.0f;
            int sponerSize = sponers.Length;
            int randomSponerIndex = Random.Range(0, sponerSize);
            Transform sponerTransform = sponers[randomSponerIndex].transform;

            GameObject newObject = Instantiate(macPrefab, sponerTransform);
            Mac mac = newObject.GetComponent<Mac>();

            // 4����1Ȯ���� ���Ѹ��� ����
            int random = Random.Range(0, 100);
            if (random <= ratioOfStrongMac)
            {
                mac.InitalizeMacEvent(MacIsDie, userHome.IsCollisionWithStrongMac);
                mac.GetComponent<SpriteRenderer>().sprite = strongMacSprite;
            }
            else
                mac.InitalizeMacEvent(MacIsDie, userHome.IsCollisionWithMac);
            mac.RenderEnable(!isSleep);
            mac.isSleep = isSleep;
            macObjects.Add(mac);

        }

    }
    private void MacIsDie(Mac mac)
    {
        macObjects.Remove(mac);
    }
    private void ChangeToDream()
    {
        userHome.GetComponent<SpriteRenderer>().enabled = false;
        wakeUp.SetActive(false);
        sleep.SetActive(true);
        isSleep = true;

        // �̰� ������ �������� �ص��ɵ�?
        foreach (Mac mac in macObjects)
        {
            mac.RenderEnable(!isSleep);
            mac.isSleep = isSleep;
        }

        textSystem.ShouldRun(isSleep);

    }

    private void ChangeToWakeUp()
    {
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

        textSystem.ShouldRun(isSleep);
    }

    public Home userHome;
    public Player player;
    public TextSystem textSystem;

    public GameObject macPrefab;
    public GameObject[] sponers;

    private List<Mac> macObjects = new List<Mac>();
    public int numberOfGenerateMac;
    public int ratioOfStrongMac;

    public GameObject wakeUp;
    public GameObject sleep;
    private bool isSleep = false;

    public Text timerText;
    public float nightTime;
    public float sponeTime;
    private float sumTime = 0.0f;

    public Sprite strongMacSprite;

    public string[] renderStrings;
    private int date = 0;

    public Text dateShowerText;
}
