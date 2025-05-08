using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public NpcUi textUi;
    private Vector3 originalScale;
    public VillageBackgroundManager villageManager;
    private bool isChoose = false;
    public bool isFinish = false;
    void Start()
    {
        isChoose = false;
        originalScale = transform.localScale;
    }
    public void ResetChoose()
    {
        isChoose = false;
    }
    public void Finish()
    {
        isFinish = true;
    }
    void OnMouseDown()
    {
        if(!isFinish)
        {
            transform.localScale = originalScale;
            isChoose = true;
            villageManager.ZoomToTarget(transform, () => 
            { 
                textUi.gameObject.SetActive(true); 
                textUi.SetNpc(this);
            });
        }
    }
    void OnMouseEnter()
    {
        if(!isChoose && !isFinish)
        {
            transform.localScale = originalScale * 1.2f;
        }
    }

    void OnMouseExit()
    {
        if(!isChoose && !isFinish)
        {
            transform.localScale = originalScale;
        }
    }
}