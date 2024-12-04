using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour, ICollidable
{
    [SerializeField] PlayerStateController playerStateController;

    float knockbackDuration; // Duration of knockback effect

    [SerializeField] Rigidbody rb; // Reference to the Rigidbody

    private float knockbackTimer = 0f;

    private Vector3 currentVelocity;

    private void FixedUpdate()
    {
        if (playerStateController.IsKnockBacked)
        {
            HandleKnockback();
        }
        else
        {
            currentVelocity = rb.velocity;
        }
    }

    private void HandleKnockback()
    {
        knockbackTimer -= Time.fixedDeltaTime;

        if (knockbackTimer <= 0)
        {
            // Reset knockback state
            playerStateController.SetKnockback(false);
        }
    }

    public void ApplyKnockback(Vector3 direction, float knockbackForce)
    {
        knockbackDuration = knockbackForce / 20;

        playerStateController.SetKnockback(true);
        knockbackTimer = knockbackDuration;

        // Apply an impulse force for knockback
        Vector3 knockbackForceVector = direction.normalized * knockbackForce;
        rb.AddForce(knockbackForceVector, ForceMode.Impulse);
        
    }

    public void CollisionEnter(CollisionData collisionData)
    {
        // Calculate knockback direction based on collision point
        var knockbackDirection = (transform.position - collisionData.Position).normalized;

        ApplyKnockback(knockbackDirection, collisionData.KnockbackForce);
    }
}
