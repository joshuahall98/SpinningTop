using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MidStats", menuName = "ScriptableObject/Stats/MidStats")]
public class MidStatsScriptableObject : ScriptableObject
{
    public float defence;
    public float stamina;
    public float balance;
    public int weight;
}
