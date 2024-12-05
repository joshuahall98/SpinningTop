using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour, ICollidable
{
    [SerializeField] PlayerStateController playerStateController;
    [SerializeField] Rigidbody rb; // Reference to the Rigidbody

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
            Debug.Log("Knockback finished");
        }

        // Apply the knockback velocity to the Rigidbody directly
        rb.velocity = knockbackVelocity;
    }

    public void ApplyKnockback(Vector3 direction, float knockbackForce)
    {
        Debug.Log("Apply knockback");

        // Set the knockback direction and force
        knockbackVelocity = direction.normalized * knockbackForce;

        // Calculate knockback duration based on force
        float knockbackDuration = knockbackForce / 20f;

        // Set the knockback state and start the timer
        playerStateController.SetKnockback(true);
        knockbackTimer = knockbackDuration;

        // Immediately apply the knockback force
        // rb.AddForce(direction * knockbackForce, ForceMode.Impulse); // No longer needed as we use velocity directly
    }

    public void CollisionEnter(CollisionData collisionData)
    {
        // Calculate the knockback direction based on the collision point
        var knockbackDirection = (transform.position - collisionData.Position).normalized;

        // Apply the knockback
        ApplyKnockback(knockbackDirection, collisionData.KnockbackForce);
    }
}
