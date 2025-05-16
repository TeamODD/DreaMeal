using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Rull : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject RullPanel;

    [Header("Rull UI")]
    public Button continue2Button;
    public TMP_Text MyText;        // 
    

    void Awake()
    {
        MyText.gameObject.SetActive(false);
        continue2Button.gameObject.SetActive(false);
    }


    public void OnRullClicked()
    {

        mainPanel.SetActive(false);

        RullPanel.SetActive(true); 

        MyText.gameObject.SetActive(true);
        continue2Button.gameObject.SetActive(true);
    }

    public void OnContinueClicked()
    {

        mainPanel.SetActive(true);

        RullPanel.SetActive(false);

        MyText.gameObject.SetActive(false);         
        continue2Button.gameObject.SetActive(false); 
    }
}
