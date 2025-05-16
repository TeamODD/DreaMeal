using System.Data;
using UnityEngine;

public class ActiveSetting : MonoBehaviour
{
    public Npc[] npcs;
    public Village[] maps;
    void OnEnable()
    {
        foreach (Npc npc in npcs)
        {
            if (npc.gameObject.activeSelf != false)
            {
                npc.Choose();
            }
        }
        foreach (Village map in maps)
        {
            if (map.gameObject.activeSelf != false)
            {
                map.Choose();
            }
        }
    }
    void OnDisable()
    {
        foreach (Npc npc in npcs)
        {
            if (npc.gameObject.activeSelf != false)
            {
                npc.ResetChoose();
            }
        }
        foreach (Village map in maps)
        {
            if (map.gameObject.activeSelf != false)
            {
                map.ResetChoose();
            }
        }
    }
}
