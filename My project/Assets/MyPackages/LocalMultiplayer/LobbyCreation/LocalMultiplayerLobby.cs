using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

[Serializable]
public class MultiplayerUI
{
    public MultiplayerEventSystem multiplayerEventSystem;
    public InputSystemUIInputModule inputSystemUIInputModule;
}

//TODO: Make player 1 kill lobby on exit
//TODO: Create the players

public class LocalMultiplayerLobby : MonoBehaviour
{
    [SerializeField] int maxPlayers = 2;
    [SerializeField] InputActionAsset inputActionAsset;
    [SerializeField] int mainMenuBuildIndex;
    [SerializeField] SceneReferenceScriptableObject gameSceneReference;

    [Header("Input Bindings")]
    [SerializeField] string joinActionGamepad = "<Gamepad>/<button>";
    [SerializeField] string joinActionKeyboard = "<Keyboard>/<button>";
    [SerializeField] string joinActionMouse = "<Mouse>/<button>";
    [SerializeField] string leaveActionGamepad = "<Gamepad>/buttonEast";
    [SerializeField] string leaveActionKeyboard = "<Keyboard>/escape";

    [SerializeField] string startGameActionGamepad = "<Gamepad>/buttonWest";
    [SerializeField] string startGameActionKeyboard = "<Keyboard>/space";

    public event Action<InputActionAsset> UserCreated;
    public event Action<int> UserDeleted;
    public event Action AllUsersDeleted;

    InputAction joinAction;
    InputAction leaveAction;
    InputAction startGameAction;

    int joinedCount;

    void Awake()
    {
        // Bind joinAction to any button press.
        joinAction = new InputAction(binding: joinActionGamepad);
        joinAction.AddBinding(joinActionKeyboard);
        joinAction.AddBinding(joinActionMouse);
        joinAction.started += JoinLobby;

        // Bind leaveAction to specific button press.
        leaveAction = new InputAction(binding: leaveActionGamepad);
        leaveAction.AddBinding(leaveActionKeyboard);
        leaveAction.started += LeaveLobby;

        startGameAction = new InputAction(binding: startGameActionGamepad);
        startGameAction.AddBinding(startGameActionKeyboard);
        startGameAction.started += StartGame;

        BeginJoining();
    }

    private void JoinLobby(InputAction.CallbackContext context)
    {

        if (joinedCount >= maxPlayers)
        {
            return;
        }

        var device = context.control.device;

        if (!UserDeviceMappingUtil.TryCreateUser(device, inputActionAsset, out var newUserInputActions)) return;

        UserCreated?.Invoke(newUserInputActions);

        joinedCount++;
    }

    private void LeaveLobby(InputAction.CallbackContext context)
    {
        if (joinedCount <= 0)
        {
            //load main menu scene
            AllUsersDeleted?.Invoke();
            return;
        }

        var device = context.control.device;


        if (!UserDeviceMappingUtil.TryDeleteUser(device, out var userIndex)) return;

        UserDeleted?.Invoke(userIndex);

        joinedCount--;
    }

    /// <summary>
    /// This method starts the game, it can only be activated by the user that created the lobby or mouse and keyboard.
    /// </summary>
    private void StartGame(InputAction.CallbackContext context)
    {
        var device = context.control.device;

        var userToRemove = InputUser.FindUserPairedToDevice(device).Value;

        if (userToRemove.index == 0 || device is Mouse || device is Keyboard)
        {
            EndJoining();

            SceneManager.LoadScene(gameSceneReference.GetSceneName());
        }    
    }

    /// <summary>
    /// Call this method to turn on the lobby functionality
    /// </summary>
    public void BeginJoining()
    {
        joinAction.Enable();
        leaveAction.Enable();
        startGameAction.Enable();
    }

    /// <summary>
    /// Call this method to turn off the lobby functionality
    /// </summary>
    public void EndJoining()
    {
        joinAction.Disable();
        leaveAction.Disable();
        startGameAction.Disable();
    }

    void OnDisable()
    {
        EndJoining();
    }
}
