using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class NightSystem : MonoBehaviour
{
    private void ChangeSystem()
    {
        if(isSleep)
        {
            wakeUp.SetActive(true);
            sleep.SetActive(false);
            isSleep = false;
        }
        else
        {
            wakeUp.SetActive(false);
            sleep.SetActive(true);
            isSleep = true;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sponeTime = 5.0f;
        wakeUp.SetActive(false);
        sleep.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        sumTime += Time.deltaTime;
        if(sumTime > sponeTime)
        {
            sumTime = 0.0f;
            int sponerSize = sponers.Count;
            int randomSponerIndex = Random.Range(0, sponerSize);
            Transform sponerTransform = sponers[randomSponerIndex].transform;
            GameObject newObject = Instantiate(macPrefab, sponerTransform);
            newObject.transform.SetParent(wakeUp.transform);
        }
        if (Input.GetKeyDown(KeyCode.Space))
            ChangeSystem();
    }

    public GameObject macPrefab = null;
    public List<GameObject> sponers = new List<GameObject>();

    public GameObject wakeUp = null;
    public GameObject sleep = null;
    private bool isSleep = true;

    private float sponeTime = 0.0f;
    private float sumTime = 0.0f;
}
