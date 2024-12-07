using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCollision : MonoBehaviour, ICollisionDataProvider
{
    [SerializeField] float knockbackForce = 5;

    public CollisionData GetCollisionData()
    {
        return new CollisionData(transform.position, knockbackForce);
    }
}
