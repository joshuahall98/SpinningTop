using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public interface ILobbyPlayerInputSetup
{
    /// <summary>
    /// Passes on the required information to setup UI controls for lobby players.
    /// </summary>
    public void SetupPlayerUIControls(InputActionAsset inputActions, InputSystemUIInputModule inputSystemUIInputModule);
}
