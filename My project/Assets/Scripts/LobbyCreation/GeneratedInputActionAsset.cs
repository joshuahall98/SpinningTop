using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GeneratedInputActionAsset : MonoBehaviour, IGeneratedInputActionAsset
{
    public IInputActionCollection2 CreateNewGeneratedInputActionAsset()
    {
        var controls = new Controls();

        return controls;
    }
}
