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
        StartCoroutine(ShortDelay(inputActions, inputSystemUIInputModule)); //this short delay is added so that any buttons pressed to add player to intefere with submit action
    }

    IEnumerator ShortDelay(IInputActionCollection2 inputActions, InputSystemUIInputModule inputSystemUIInputModule)
    {
        yield return new WaitForSeconds(0.1f);

        inputSystemUIInputModule.submit = InputActionReference.Create(inputActions.FindAction(submit.name));
        inputSystemUIInputModule.move = InputActionReference.Create(inputActions.FindAction(move.name));
    }
}
