using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour, ICollidable
{
    [SerializeField] PlayerStateController playerStateController;
    [SerializeField] Rigidbody rb; // Reference to the Rigidbody
    [SerializeField] float defaultKnocbackValue = 3;

    //THIS NEEDS TO POPULATED BY SPIN TOP DATA
    [SerializeField, Range(0, 10)] int weightData = 10;

    private float knockbackTimer = 0f;
    private Vector3 knockbackVelocity; // Store the knockback velocity

    private void FixedUpdate()
    {
        // If the player is in knockback, apply the knockback effect
        if (playerStateController.IsKnockBacked)
        {
            HandleKnockback();
        }
    }

    private void HandleKnockback()
    {
        // Decrease knockback timer to handle the knockback duration
        knockbackTimer -= Time.fixedDeltaTime;

        if (knockbackTimer <= 0)
        {
            // Reset knockback state once the timer finishes
            playerStateController.SetKnockback(false);
            knockbackVelocity = Vector3.zero; // Reset knockback velocity
        }

        // Apply the knockback velocity to the Rigidbody directly
        rb.velocity = knockbackVelocity;
    }

    public void ApplyKnockback(Vector3 direction, float knockbackForce)
    {
        // Set the knockback direction and force
        knockbackVelocity = direction.normalized * knockbackForce;

        // Calculate knockback duration based on force
        float knockbackDuration = knockbackForce / 20f;

        // Set the knockback state and start the timer
        playerStateController.SetKnockback(true);
        knockbackTimer = knockbackDuration;
    }

    public void CollisionEnter(CollisionData collisionData)
    {
        // Calculate the knockback direction based on the collision point
        var knockbackDirection = (transform.position - collisionData.Position).normalized;

        var knockbackForce = CollisionDataHandler.CalculateKnockbackForce(collisionData, weightData);

        Debug.Log(knockbackForce);

        // Apply the knockback
        ApplyKnockback(knockbackDirection, Mathf.Max(knockbackForce, defaultKnocbackValue));
    }
}
