using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StringPeice
{
    public StringPeice(string str)
    {
        peices = str.Split(' ');
    }
    public string[] peices;
}

public class TextSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (string str in renderStrings)
        {
            stringPeices.Add(new StringPeice(str));
        }

        int renderString = 2;
        for (int i = 0; i < stringPeices[renderString].peices.Length; i++)
        {
            textMesh[i].text = stringPeices[renderString].peices[i];
        }
    }

    public Text[] textMesh;

    public string[] renderStrings;

    private List<StringPeice> stringPeices = new List<StringPeice>();
}
