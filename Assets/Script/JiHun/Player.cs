using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {

    }
    void Attack()
    {
        float attackRange = 0.5f; // 공격 범위
        Vector2 attackPosition = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            Mac macMove = enemy.GetComponent<Mac>();
            if (enemy.CompareTag("Enemy")) // 적 태그 확인
                macMove.Hitted();
            
        }
    }

    public bool IsInDoor() { return isInDoor; }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "UserHome")
            isInDoor = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "UserHome")
            isInDoor = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            direction = -Vector2.right;
            transform.Translate(-Vector2.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
    }

    public float moveSpeed = 2.0f;
    public float rotationSpeed = 5.0f;
    public Vector2 direction = Vector2.right;

    private bool isInDoor = false;
}
