using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class MainMenu : MonoBehaviour
{
    public void OnGameStart()
    {
        SceneManager.LoadScene("JiHunScene");
    }

    public void OnSetting()
    {
        
    }
    public void OnFinish()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서는 Play 모드 종료
#else
        Application.Quit(); // 빌드된 게임에서는 완전히 종료
#endif
    }

}
