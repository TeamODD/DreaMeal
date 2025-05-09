using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    public int charPerSeconds = 15;
    private bool isTyping = false;
    private bool skipTyping = false;
    public IEnumerator TypeDialog(Text text, string str, List<GameObject> btns = null)
    {
        isTyping = true;
        skipTyping = false;
        text.text = "";
        foreach (var character in str.ToCharArray())
        {
            if (skipTyping)
            {
                text.text = str;
                break;
            }
            text.text += character;
            yield return new WaitForSeconds(1f / charPerSeconds);
        }
        isTyping = false;
        if (btns != null)
        {
            foreach (GameObject btn in btns)
            {
                btn.SetActive(true);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                skipTyping = true;
            }
        }
    }
}
