using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] PlayerKnockback playerKnockback;

    [SerializeField] CollisionProxy playerCollision;

    [Header("Tags")]
    [SerializeField] TagScriptableObject playerTag;

    private void Start()
    {
        playerCollision.OnCollisionEnter3D_Action += KnockBackOnCollisionEnter;
    }

    private void KnockBackOnCollisionEnter(Collision collision)
    {
        if(TagExtensions.HasTag(collision.gameObject, playerTag))
        {
            Vector3 knockbackDirection = (transform.position - collision.transform.position).normalized;

            // Apply knockback to this player
            playerKnockback.ApplyKnockback(knockbackDirection);

            // Optionally, apply knockback to the other player
            collision.gameObject.GetComponent<PlayerKnockback>()?.ApplyKnockback(-knockbackDirection);
        }
    }
}
