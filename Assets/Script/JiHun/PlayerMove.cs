using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    void Attack()
    {
        float attackRange = 1f; // 공격 범위
        Vector2 attackPosition = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            MacMove macMove = enemy.GetComponent<MacMove>();
            if (enemy.CompareTag("Enemy")) // 적 태그 확인
                macMove.Hitted();
            
        }
    }


    // Update is called once per frame
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
}
