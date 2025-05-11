using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GradationAnimate : MonoBehaviour
{
    public float duration = 0.5f;
    private bool isFading = false;
    private bool skipFade = false;

    public IEnumerator FadeImage(Image img, Sprite newSprite)
    {
        if (img.sprite != newSprite)
        {
            isFading = true;
            skipFade = false;
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                if (skipFade) break;
                img.color = new Color(1 - t / duration, 1 - t / duration, 1 - t / duration);
                yield return null;
            }
            img.sprite = newSprite;
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                if (skipFade) break;
                img.color = new Color(t / duration, t / duration, t / duration);
                yield return null;
            }
            img.color = new Color(1, 1, 1, 1f);
            isFading = false;
        }
    }
    public IEnumerator FadeImageWhite(Image img, Sprite newSprite)
    {
        if (img.sprite != newSprite)
        {
            isFading = true;
            skipFade = false;
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                if (skipFade) break;
                img.color = new Color(1, 1, 1, 1 - t / duration);
                yield return null;
            }
            img.sprite = newSprite;
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                if (skipFade) break;
                img.color = new Color(1, 1, 1,  t / duration);
                yield return null;
            }
            img.color = new Color(1, 1, 1, 1f);
            isFading = false;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isFading)
            {
                skipFade = true;
            }
        }
    }
}