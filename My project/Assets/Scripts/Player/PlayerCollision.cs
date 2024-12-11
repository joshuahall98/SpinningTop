using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour, ICollisionDataProvider
{
    [SerializeField] CollisionProxy playerCollision;
    [SerializeField] PlayerMovement playerMovement;

    Vector3 velocityBeforeCollision;

    private void Start()
    {
        playerCollision.OnCollisionEnter3D_Action += CollisionEnter;
    }
    private void FixedUpdate()
    {
        // Store the velocity just before collision (in the physics update)
        velocityBeforeCollision = playerMovement.CurrentVelocity;
    }

    private void CollisionEnter(Collision collision)
    {
        // Handle the collision
        CollisionManager.instance.HandleCollisionEnter(gameObject, collision.gameObject);
    }

    public CollisionData GetCollisionData()
    {
        return new CollisionData(transform.position, velocityBeforeCollision.magnitude);
    }
}
