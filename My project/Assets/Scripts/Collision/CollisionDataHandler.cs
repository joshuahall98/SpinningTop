using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollisionDataHandler 
{

    public static float CalculateKnockbackForce(CollisionData collisionData, int objWeight)
    {

        float weightRatio = (collisionData.Weight - objWeight) / 10f;
        return (collisionData.Velocity / 2f) * (weightRatio + 1f);
    }
}
