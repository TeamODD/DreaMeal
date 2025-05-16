using UnityEngine;

public class Village : MonoBehaviour
{
    public GameObject village;
    public GameObject map;
    private Vector3 originalScale;
    private bool isChoose = false;
    public void ResetChoose()
    {
        isChoose = false;
    }
    public void Choose()
    {
        isChoose = true;
    }
    void Start()
    {
        originalScale = transform.localScale;
    }
    void OnMouseDown()
    {
        if (!isChoose)
        {
            transform.localScale = originalScale;
            village.SetActive(true);
            map.SetActive(false);
        }
        isChoose = true;
    }
    void OnMouseEnter()
    {
        if (!isChoose)
        {
            transform.localScale = originalScale * 1.1f;
        }
    }

    void OnMouseExit()
    {
        if (!isChoose)
        {
            transform.localScale = originalScale;
        }
    }
}