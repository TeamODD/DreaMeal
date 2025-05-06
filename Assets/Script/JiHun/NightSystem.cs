using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NightSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sponeTime = 3.0f;
        userHome.myConditionIsSafe = textSystem.IsSafeHome;
        ChangeToDream();
    }

    // Update is called once per frame
    void Update()
    {
        nightTime -= Time.deltaTime;
        timerText.text = nightTime.ToString();
        if (nightTime <= 0)
            SceneManager.LoadScene("특정씬");

        sumTime += Time.deltaTime;
        if (sumTime > sponeTime)
        {
            sumTime = 0.0f;
            int sponerSize = sponers.Length;
            int randomSponerIndex = Random.Range(0, sponerSize);
            Transform sponerTransform = sponers[randomSponerIndex].transform;

            GameObject newObject = Instantiate(macPrefab, sponerTransform);
            Mac mac = newObject.GetComponent<Mac>();
            mac.InitalizeMacEvent(MacIsDie, userHome.IsCollisionWithMac);
            mac.RenderEnable(!isSleep);
            mac.isSleep = isSleep;
            macObjects.Add(mac);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isSleep)
                ChangeToWakeUp();
            else
                if (player.IsInDoor())
                    ChangeToDream();
        }

    }

    private void MacIsDie(Mac mac)
    {
        macObjects.Remove(mac);
    }
    private void ChangeToDream()
    {
        wakeUp.SetActive(false);
        sleep.SetActive(true);
        isSleep = true;

        // 이걸 옵저버 패턴으로 해도될듯?
        foreach (Mac mac in macObjects)
        {
            mac.RenderEnable(!isSleep);
            mac.isSleep = isSleep;
        }

    }

    private void ChangeToWakeUp()
    {
        wakeUp.SetActive(true);
        sleep.SetActive(false);
        isSleep = false;

        player.transform.position = userHome.transform.position - Vector3.up;

        foreach (Mac mac in macObjects)
        { 
            mac.RenderEnable(!isSleep);
            mac.isSleep = isSleep;
        }
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
    public float nightTime;
    private float sponeTime = 0.0f;
    private float sumTime = 0.0f;
}
