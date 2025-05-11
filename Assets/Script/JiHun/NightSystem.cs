using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NightSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (100 < ratioOfStrongMac)
            ratioOfStrongMac = 100;
        else if (ratioOfStrongMac < 0)
            ratioOfStrongMac = 0;

        textSystem.SetRenderString("�ȳ� ���� �������̾� ���� ������ �����ֶ� ^^ �˰�¡?");

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
            SceneManager.LoadScene("Ư����");

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
                mac.InitalizeMacEvent(MacIsDie, userHome.IsCollisionWithStrongMac);
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
}
