using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

    //[SerializeField] InputActionReference actionButton;

    Controls controls;

    //InputActionMap playerControls;

    //needs further work
    public void AssignActions(IInputActionCollection inputActions)
    {

        controls = (Controls)inputActions;

        Debug.Log(controls);

        /*var inputActionAsset = inputActions as InputActionAsset;

        IInputActionCollection2 convertedInputActions = inputActionAsset;

        convertedInputActions = new Controls();

        controls = convertedInputActions as Controls;

        controls.PlayerControls.ActionButton.ChangeBindingWithPath()

        Debug.Log(controls);

        playerControls = inputActionAsset.FindActionMap(controls.PlayerControls.ToString());

        Debug.Log(playerControls);

        playerControls[actionButton.action.name].performed += ActionButton_performed;

        InputAction test;*/

        EnableAllInputs();

    }


    private void ActionButton_performed(InputAction.CallbackContext context)
    {
        Debug.Log("It is me");
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
