using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class MainMenu : MonoBehaviour
{
    public void OnGameStart()
    {
        if (MorningManager.Instance != null)
        {
            Destroy(MorningManager.Instance);
        }
        SceneManager.LoadScene("OpeningEnding");
    }

    public void OnSetting()
    {
        
    }
    public void OnFinish()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ����� Play ��� ����
#else
        Application.Quit(); // ����� ���ӿ����� ������ ����
#endif
    }

}
