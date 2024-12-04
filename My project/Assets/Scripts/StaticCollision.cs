using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCollision : MonoBehaviour, ICollisionDataProvider
{
    public CollisionData GetCollisionData()
    {
        return new CollisionData(transform.position);
    }
}
