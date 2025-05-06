using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

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
        if (hp > 0)
            hp -= 1;
        else
        {
            dieEvent(this);
            Destroy(gameObject);
        }
    }
    public void MoveToHome()
    {
        if (collisionToHome)
            return;
        if (isSleep == false)
            return;

        Vector3 toGoingDirection = (home.transform.position - transform.position);
        toGoingDirection.Normalize();

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
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collisionToHome && collisionToHome)
        {
            collisionToHome = false;
            collisionWithHomeEvent(false);
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
}
