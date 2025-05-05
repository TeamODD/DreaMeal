using UnityEngine;
using UnityEngine.EventSystems;

public class MacMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toGoingDirection = (userHome.transform.position - transform.position);
        toGoingDirection.Normalize();

        transform.Translate(toGoingDirection * moveSpeed * Time.deltaTime, Space.World);
    }
    public void Hitted()
    {
        if (hp > 0)
            hp -= 1;
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject userHome = null;
    public float moveSpeed = 0.3f;

    public int hp = 4;
    
}
