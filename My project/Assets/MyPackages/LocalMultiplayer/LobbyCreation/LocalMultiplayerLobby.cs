using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;


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

    [Header("Input Bindings")]
    [SerializeField] string joinActionGamepad = "<Gamepad>/<button>";
    [SerializeField] string joinActionKeyboard = "<Keyboard>/<button>";
    [SerializeField] string joinActionMouse = "<Mouse>/<button>";
    [SerializeField] string leaveActionGamepad = "<Gamepad>/buttonEast";
    [SerializeField] string leaveActionKeyboard = "<Keyboard>/escape";

    public event Action<InputActionAsset> UserCreated;
    public event Action<int> UserDeleted;
    public event Action AllUsersDeleted;

    InputAction joinAction;
    InputAction leaveAction;
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

        BeginJoining();
    }

    private void JoinLobby(InputAction.CallbackContext context)
    {

        if (joinedCount >= maxPlayers)
        {
            return;
        }

        var device = context.control.device;

        var tuple = UserDeviceMappingUtil.CreateUser(device, inputActionAsset);

        var userCreated = tuple.Item2;
        
        if (!userCreated)
        {
            return;
        }

        var newUserInputActions = tuple.Item1;

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

        var userIndex = UserDeviceMappingUtil.DeleteUser(device);

        if(userIndex < 0)
        {
            return;
        }

        UserDeleted?.Invoke(userIndex);

        joinedCount--;
    }

    /// <summary>
    /// Call this method to turn on the lobby functionality
    /// </summary>
    public void BeginJoining()
    {
        joinAction.Enable();
        leaveAction.Enable();
    }

    /// <summary>
    /// Call this method to turn off the lobby functionality
    /// </summary>
    public void EndJoining()
    {
        joinAction.Disable();
        leaveAction.Disable();
    }

    void OnDisable()
    {
        EndJoining();
    }
}
