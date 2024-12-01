using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class LobbyPlayerInputSetup : MonoBehaviour, ILobbyPlayerInputSetup
{
    [SerializeField] InputActionReference submit;
    [SerializeField] InputActionReference move;

    public void SetupPlayerUIControls(IInputActionCollection2 inputActions, InputSystemUIInputModule inputSystemUIInputModule)
    {
        inputSystemUIInputModule.submit = InputActionReference.Create(inputActions.FindAction(submit.name));
        inputSystemUIInputModule.move = InputActionReference.Create(inputActions.FindAction(move.name));

    }
}
