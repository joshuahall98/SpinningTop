using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BottomStats", menuName = "ScriptableObject/Stats/BottomStats")]
public class BottomStatsScriptableObject : ScriptableObject
{
    public GameObject view;

    public float stamina;
    public float balance;
    public int speed;
    public int weight;
}
