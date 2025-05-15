using System;
using UnityEngine;
using System.Collections;

public class ImageFader
{
    // text������ �̸� ���ؼ�
    public static IEnumerator FadeOutImage(SpriteRenderer renderer, float fadeDuration)
    {
        Color color = renderer.color;
        float startAlpha = 1f; // ������ ����
        float endAlpha = 0f;   // ������ ������

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = newAlpha;
            renderer.color = color;
            yield return null; // ���� �����ӱ��� ���
        }

        // ������ �ܰ迡�� ������ ������ ����
        color.a = endAlpha;
        renderer.color = color;
    }
    public static IEnumerator FadeInImage(SpriteRenderer renderer, float fadeDuration)
    {
        Color color = renderer.color;
        float startAlpha = 0f; // ������ ����
        float endAlpha = 1f;   // ������ ������

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = newAlpha;
            renderer.color = color;
            yield return null; // ���� �����ӱ��� ���
        }

        // ������ �ܰ迡�� ������ ������ ����
        color.a = endAlpha;
        renderer.color = color;
    }
}
public class Mac : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToHome();
        if (isRender)
            spriteRenderer.enabled = true;
        else
            spriteRenderer.enabled = false;
    }
    public void Hitted()
    {
        Debug.Log("Hitted");
        hp -= 1;
        if(hp <=0)
        {
            isDestroy = true;
            StartCoroutine(FadeAndDestroy());
        }
    }
    private IEnumerator FadeAndDestroy()
    {
        yield return StartCoroutine(ImageFader.FadeOutImage(spriteRenderer, 1.0f)); // ���̵� �� ����
        Die();
    }

    private void Die()
    {
        Debug.Log("Die");
        dieEvent(this);
        Destroy(gameObject);
    }
    public void MoveToHome()
    {
        if (collisionToHome)
            return;

        if (isSleep == false)
            return;

        Vector3 toGoingDirection = (home.transform.position - transform.position);
        toGoingDirection.Normalize();

        if (toGoingDirection.x > 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else
            transform.localScale = new Vector3(1f, 1f, 1f);

        transform.Translate(toGoingDirection * moveSpeed * Time.deltaTime, Space.World);
    }
    public void RenderEnable(bool beRender)
    {
        isRender = beRender;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "UserHome" && collisionToHome == false)
        {
            collisionToHome = true;
            collisionWithHomeEvent(true);
            isSleep = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collisionToHome && collisionToHome)
        {
            collisionToHome = false;
            collisionWithHomeEvent(false);
            isSleep = false;
        }
    }
    public void InitalizeMacEvent(Action<Mac> dieEvent, Action<bool> collisionWithHomeEvent)
    {
        this.dieEvent = dieEvent;
        this.collisionWithHomeEvent = collisionWithHomeEvent;
    }

    // ���� ���� �΋H���� ���Լ� ȣ���ϰ� �ű⼭ �б�ó���ؼ� ���� �ؽ�Ʈ ȣ��

    public Action<Mac> dieEvent = null;
    public Action<bool> collisionWithHomeEvent = null;

    public GameObject home;
    public float moveSpeed = 0.3f;
    private bool collisionToHome = false;

    public int hp = 4;
    public bool isRender = false;
    public bool isSleep = false;

    private SpriteRenderer spriteRenderer = null;

    public bool isDestroy = false;

}
