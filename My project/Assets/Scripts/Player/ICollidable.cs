using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ICollidable
{
    void CollisionEnter(Vector3 collisionObjPosition);
}
