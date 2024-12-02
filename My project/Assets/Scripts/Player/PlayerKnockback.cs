using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{

    [SerializeField] PlayerStateController playerStateController;

    [Header("Knockback Settings")]
    [SerializeField] float knockbackForce = 10f; // The intensity of the knockback
    [SerializeField] float knockbackDuration = 0.5f; // Duration of knockback effect

    private Vector3 knockbackVelocity;
    private float knockbackTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (playerStateController.IsKnockBacked)
        {
            Knockback();
        }
        
    }

    private void Knockback()
    {
        transform.Translate(knockbackVelocity * Time.deltaTime, Space.World);

        // Reduce the knockback timer
        knockbackTimer -= Time.deltaTime;
        if (knockbackTimer <= 0)
        {
            playerStateController.SetKnockback(false); // Reset knockback state
            knockbackVelocity = Vector3.zero;
        }
    }

    public void ApplyKnockback(Vector3 direction)
    {
        playerStateController.SetKnockback(true);
        knockbackTimer = knockbackDuration;

        // Calculate knockback velocity
        knockbackVelocity = direction.normalized * knockbackForce;
    }
}
