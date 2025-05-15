using System;
using UnityEngine;
using System.Collections;

public class ImageFader
{
    // text색깔은 미리 정해서
    public static IEnumerator FadeOutImage(SpriteRenderer renderer, float fadeDuration)
    {
        Color color = renderer.color;
        float startAlpha = 1f; // 완전히 투명
        float endAlpha = 0f;   // 완전한 불투명

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = newAlpha;
            renderer.color = color;
            yield return null; // 다음 프레임까지 대기
        }

        // 마지막 단계에서 완전한 불투명 설정
        color.a = endAlpha;
        renderer.color = color;
    }
    public static IEnumerator FadeInImage(SpriteRenderer renderer, float fadeDuration)
    {
        Color color = renderer.color;
        float startAlpha = 0f; // 완전히 투명
        float endAlpha = 1f;   // 완전한 불투명

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = newAlpha;
            renderer.color = color;
            yield return null; // 다음 프레임까지 대기
        }

        // 마지막 단계에서 완전한 불투명 설정
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
        yield return StartCoroutine(ImageFader.FadeOutImage(spriteRenderer, 1.0f)); // 페이드 인 실행
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

    // 맥이 집에 부딫히면 집함수 호출하고 거기서 분기처리해서 집이 텍스트 호출

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
