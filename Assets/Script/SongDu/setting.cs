using UnityEngine;
using UnityEngine.UI;

public class setting : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;     // MainMenuPanel 
    public GameObject optionPanel;     // SettingsPanel 


    void Awake()
    {
        
        optionPanel.SetActive(false);
        
       
    }

    
    public void OnSettingsClicked()
    {
        
        mainPanel.SetActive(false);

        
        optionPanel.SetActive(true);

        
        
        
    }

    
    public void OnContinueClicked()
    {
        
        optionPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
