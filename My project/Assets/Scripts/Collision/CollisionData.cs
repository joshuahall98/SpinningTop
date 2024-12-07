using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionData
{
    public Vector3 Position { get; }
    public float KnockbackForce { get; }
    public float Damage { get; }
    public float Defence { get; }

    public CollisionData(Vector3 position, float knockbackForce, float damage = 0, float defense = 0)
    {
        Position = position;
        KnockbackForce = knockbackForce;
        Damage = damage;
        Defence = defense;
    }
}
