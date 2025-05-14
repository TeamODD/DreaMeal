using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnGameStart()
    {
        Debug.Log("Game Start 버튼 클릭됨");
        SceneManager.LoadScene("GameScene");
    }

    public void OnSetting()
    {
        Debug.Log("Setting 버튼 클릭됨");
        
    }

    public void OnFinish()
    {
        Debug.Log("Finish 버튼 클릭됨");
        Application.Quit(); 
    }
}
