using UnityEngine;

public class Village : MonoBehaviour
{
    public GameObject village;
    public GameObject map;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }
    void OnMouseDown()
    {
        transform.localScale = originalScale;
        village.SetActive(true);
        map.SetActive(false);
    }
    void OnMouseEnter()
    {
        transform.localScale = originalScale * 1.1f;
    }

    void OnMouseExit()
    {
        transform.localScale = originalScale;
    }
}