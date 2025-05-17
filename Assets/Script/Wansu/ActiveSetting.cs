using System.Data;
using UnityEngine;

public class ActiveSetting : MonoBehaviour
{
    public Npc[] npcs;
    public Village[] maps;
    void OnEnable()
    {
        if (npcs != null)
        {
            foreach (Npc npc in npcs)
            {
                if (npc.gameObject.activeSelf != false)
                {
                    npc.Choose();
                }
            }
        }
        if (maps != null)
        {
            foreach (Village map in maps)
            {
                if (map.gameObject.activeSelf != false)
                {
                    map.Choose();
                }
            }
        }
    }
    void OnDisable()
    {
        if (npcs != null)
        {
            foreach (Npc npc in npcs)
            {
                if (npc.gameObject.activeSelf != false)
                {
                    npc.ResetChoose();
                }
            }
        }
        if (maps != null)
        {
            foreach (Village map in maps)
            {
                if (map.gameObject.activeSelf != false)
                {
                    map.ResetChoose();
                }
            }
        }
    }
}
