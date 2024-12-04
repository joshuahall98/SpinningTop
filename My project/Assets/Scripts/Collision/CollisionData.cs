using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionData
{
    public Vector3 Position { get; }
    public float KnockbackForce { get; }

    public CollisionData(Vector3 position, float knockbackForce = 10)
    {
        Position = position;
        KnockbackForce = knockbackForce;
    }
}
