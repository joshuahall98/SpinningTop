using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionData
{
    public Vector3 Position { get; }
    public float Velocity { get; }
    public int Weight { get; }
    public float Damage { get; }
    public float Defence { get; }

    public CollisionData(Vector3 position, float velocity, int weight = 0, float damage = 0, float defense = 0)
    {
        Position = position;
        Velocity = velocity;
        Weight = weight;
        Damage = damage;
        Defence = defense;
    }
}
