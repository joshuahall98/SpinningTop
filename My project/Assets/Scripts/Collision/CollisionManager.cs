using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    public static CollisionManager instance;

    private readonly HashSet<(GameObject, GameObject)> processedCollisions = new HashSet<(GameObject, GameObject)>();

    private void Awake()
    {
        instance = this;
    }

    public void HandleCollisionEnter(GameObject obj1, GameObject obj2)
    {

        var pair = CreatePair(obj1, obj2);

        // Check if the pair has already been processed
        if (processedCollisions.Contains(pair))
        {
            return;
        }

        // Add the pair to the set to mark it as processed
        processedCollisions.Add(pair);

        // Calculate knockback direction
        var knockbackDirection = (obj1.transform.position - obj2.transform.position).normalized;

        // Get collision data
        var obj1CollisionData = obj1.GetComponent<ICollisionDataProvider>()?.GetCollisionData();
        var obj2CollisionData = obj2.GetComponent<ICollisionDataProvider>()?.GetCollisionData();

        if(obj1CollisionData == null || obj2CollisionData == null)
        {
            return;
        }

        // Apply collisoon
        obj1.GetComponent<ICollidable>()?.CollisionEnter(obj2CollisionData);
        obj2.GetComponent<ICollidable>()?.CollisionEnter(obj1CollisionData);

        StartCoroutine(ReleaseHashSet(pair));
    }

    // Helper method to create a consistent, unique pair
    private (GameObject, GameObject) CreatePair(GameObject obj1, GameObject obj2)
    {
        return obj1.GetInstanceID() < obj2.GetInstanceID()
            ? (obj1, obj2)
            : (obj2, obj1);
    }

    IEnumerator ReleaseHashSet((GameObject, GameObject) pair)
    {
        yield return new WaitForEndOfFrame();

        processedCollisions.Remove(pair);
    }

}
