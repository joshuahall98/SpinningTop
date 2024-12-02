using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

    Controls controls;


    public event Action<InputAction.CallbackContext> MoveEvent;

    public void AssignActions(IInputActionCollection inputActions)
    {

        controls = (Controls)inputActions;

        controls.PlayerControls.Movement.performed += Movement;
        controls.PlayerControls.Movement.canceled += Movement;

        EnableAllInputs();

    }

    private void Movement(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context);
    }


    public void EnableAllInputs()
    {
        controls.Enable();
    }

    public void DisableAllInputs()
    {
        controls.Disable();
    }

    private void OnEnable()
    {
        if(controls != null)
        {
            EnableAllInputs();
        }
        
    }

    private void OnDisable()
    {
        DisableAllInputs();
    }
}
