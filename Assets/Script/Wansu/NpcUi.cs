using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NpcUi : MonoBehaviour
{
    public Text text;
    public GameObject yesBtn;
    public GameObject noBtn;
    public string[] dialogues;
    private int currentIndex = 0;
    public int charPerSeconds = 15;
    private Coroutine typingCoroutine;

    void OnEnable()
    {
        currentIndex = 0;
        typingCoroutine = StartCoroutine(TypeDialog(dialogues[currentIndex]));
        yesBtn.SetActive(false);
        noBtn.SetActive(false);
    }
    public IEnumerator TypeDialog(string str)
    {
        text.text = "";
        foreach (var character in str.ToCharArray())
        {
            text.text += character;
            yield return new WaitForSeconds(1f / charPerSeconds);
        }
        yesBtn.SetActive(true);
        noBtn.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(typingCoroutine);
            text.text = dialogues[currentIndex];
            yesBtn.SetActive(true);
            noBtn.SetActive(true);
        }
    }
}