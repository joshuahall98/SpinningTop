using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour, ICollisionDataProvider
{
    [SerializeField] PlayerKnockback playerKnockback;

    [SerializeField] CollisionProxy playerCollision;

    [Header("Tags")]
    [SerializeField] TagScriptableObject playerTag;

    private void Start()
    {
        playerCollision.OnCollisionEnter3D_Action += CollisionEnter;
    }

    private void CollisionEnter(Collision collision)
    {
        CollisionManager.instance.HandleCollisionEnter(gameObject, collision.gameObject);
    }

    public CollisionData GetCollisionData()
    {
        return new CollisionData(transform.position);
    }
}
