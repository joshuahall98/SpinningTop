using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{

    public event Action KnockBackReset;

    private bool isKnockBacked;

    public bool IsKnockBacked => isKnockBacked;

    public void SetKnockback(bool value)
    {
        KnockBackReset?.Invoke();
        isKnockBacked = value;
        
    }
}
