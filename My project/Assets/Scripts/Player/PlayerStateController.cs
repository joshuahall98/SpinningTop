using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    private bool isKnockBacked;

    public bool IsKnockBacked => isKnockBacked;

    public void SetKnockback(bool value)
    {
        isKnockBacked = value;
    }
}
