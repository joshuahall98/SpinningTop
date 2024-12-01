using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersistenceComponent : MonoBehaviour
{
    private void Awake()
    {
        // Ensure this object persists between scenes
        DontDestroyOnLoad(gameObject);
    }
}
