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

    /// <summary>
    /// This method isn't required, but can be used to create player panels (To show a new player has joined the lobby)
    /// </summary>
    public void SetupPlayerPanel(GameObject playerPanel, MultiplayerEventSystem multiplayerEventSystem);
}
