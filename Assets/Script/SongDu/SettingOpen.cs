using UnityEngine;
using UnityEngine.UI;

public class SettingOpen : MonoBehaviour
{
    public Button SettingButton;
    public Button ExitButton;
    public GameObject SettingPenal;
    public Button EndButton;


    void Start()
    {
        SettingButton.onClick.AddListener(OnSettingButton);
        ExitButton.onClick.AddListener(OnExitButton);
        EndButton.onClick.AddListener(OnEndbutton);
    }

    private void OnSettingButton()
    {
        SettingPenal.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnExitButton() 
    {
        SettingPenal.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnEndbutton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); 
#endif

    }
}
