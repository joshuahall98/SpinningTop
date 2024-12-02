using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionProxy : MonoBehaviour
{
    public Action<Collision> OnCollisionEnter3D_Action;
    public Action<Collision> OnCollisionStay3D_Action;
    public Action<Collision> OnCollisionExit3D_Action;

    public Action<Collider> OnTriggerEnter3D_Action;
    public Action<Collider> OnTriggerStay3D_Action;
    public Action<Collider> OnTriggerExit3D_Action;


    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEnter3D_Action?.Invoke(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        OnCollisionStay3D_Action?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        OnCollisionExit3D_Action?.Invoke(collision);
    }

    private void OnTriggerEnter(Collider collider)
    {
        OnTriggerEnter3D_Action?.Invoke(collider);
    }

    private void OnTriggerStay(Collider collider)
    {
        OnTriggerStay3D_Action?.Invoke(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        OnTriggerExit3D_Action?.Invoke(collider);
    }
}
