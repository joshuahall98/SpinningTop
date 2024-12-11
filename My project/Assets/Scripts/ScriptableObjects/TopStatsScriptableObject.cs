using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TopStats", menuName = "ScriptableObject/Stats/TopStats")]
public class TopStatsScriptableObject : ScriptableObject
{
    public float damage;
    public float defence;
    public float balance;
    public int weight;
}
