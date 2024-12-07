using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDataHandler : MonoBehaviour, ICollisionDataProvider
{
    [SerializeField] Rigidbody rb;

    Vector3 mostRecentVelocity;

    public CollisionData GetCollisionData()
    {
        return new CollisionData(transform.position, mostRecentVelocity.magnitude);
    }

    public void UpdateVelocityData(Vector3 velocity)
    {
        mostRecentVelocity = velocity;
    }
}
