using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Npc : MonoBehaviour
{
    public NpcUi textUi;
    private Vector3 originalScale;
    public VillageBackgroundManager villageManager;
    private bool isChoose = false;
    void Start()
    {
        isChoose = false;
        originalScale = transform.localScale;
    }
    public void ResetChoose()
    {
        isChoose = false;
    }
    public void Choose()
    {
        isChoose = true;
    }
    void OnMouseDown()
    {
        transform.localScale = originalScale;
        if (!isChoose)
        {
            villageManager.ZoomToTarget(transform, () => 
            { 
                textUi.gameObject.SetActive(true); 
                textUi.SetNpc(this);
            });
        }
        isChoose = true;
    }
    void OnMouseEnter()
    {
        if(!isChoose)
        {
            transform.localScale = originalScale * 1.2f;
        }
    }

    void OnMouseExit()
    {
        if(!isChoose)
        {
            transform.localScale = originalScale;
        }
    }
}