using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnGameStart()
    {
        Debug.Log("Game Start ��ư Ŭ����");
        SceneManager.LoadScene("GameScene");
    }

    public void OnSetting()
    {
        Debug.Log("Setting ��ư Ŭ����");
        
    }

    public void OnFinish()
    {
        Debug.Log("Finish ��ư Ŭ����");
        Application.Quit(); 
    }
}
