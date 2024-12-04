using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    public static CollisionManager instance;

    private readonly HashSet<(GameObject, GameObject)> processedCollisions = new HashSet<(GameObject, GameObject)>();

    [Header("Tags")]
    [SerializeField] TagScriptableObject floor;

    private void Awake()
    {
        instance = this;
    }

    public void HandleCollisionEnter(GameObject obj1, GameObject obj2)
    {

        if(obj1.HasTag(floor) || obj2.HasTag(floor))
        {
            return;
        }

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

        // Apply collisoon
        obj1.GetComponent<ICollidable>()?.CollisionEnter(obj2.transform.position);
        obj2.GetComponent<ICollidable>()?.CollisionEnter(obj1.transform.position);

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
        yield return new WaitForSeconds(0.1f);

        processedCollisions.Remove(pair);
    }

}
