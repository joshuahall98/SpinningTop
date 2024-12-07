using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] PlayerCollisionDataHandler playerCollisionDataHandler;
    [SerializeField] CollisionProxy playerCollision;
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] Rigidbody rb;

    Vector3 velocityBeforeCollision;

    private void Start()
    {
        playerCollision.OnCollisionEnter3D_Action += CollisionEnter;

        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        // Store the velocity just before collision (in the physics update)
        velocityBeforeCollision = playerMovement.CurrentVelocity;
    }


    private void CollisionEnter(Collision collision)
    {
        playerCollisionDataHandler.UpdateVelocityData(velocityBeforeCollision);

        // Handle the collision
        CollisionManager.instance.HandleCollisionEnter(gameObject, collision.gameObject);
    }
}
