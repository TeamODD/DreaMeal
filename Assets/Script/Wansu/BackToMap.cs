using UnityEngine;

public class BackToMap : MonoBehaviour
{
    public VillageBackgroundManager villageManager;
    public void Back()
    {
        villageManager.village.SetActive(false);
        villageManager.map.SetActive(true);
    }
}
