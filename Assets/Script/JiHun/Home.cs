using System;
using UnityEngine;

public class Home : MonoBehaviour
{
    public void IsCollisionWithMac(bool isCollision)
    {
        collisionMacCount += isCollision ? 1 : -1;
        
        if (collisionMacCount == 1)
            myConditionIsSafe(false);
        else if(collisionMacCount == 0)
            myConditionIsSafe(true);
    }

    public void IsCollisionWithStrongMac(bool isCollision)
    {
        IsCollisionWithMac(isCollision);
        collisionWithStrongMac();
    }

    public Action<bool> myConditionIsSafe;
    public Action collisionWithStrongMac;

    private int collisionMacCount = 0;
}
