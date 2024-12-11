using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

//TODO: Make player 1 kill lobby on exit
//TODO: Create the players

public class LocalMultiplayerLobby : MonoBehaviour
{

    public static LocalMultiplayerLobby Instance;

    [SerializeField] int maxPlayers = 2;
    //[SerializeField] InputActionAsset inputActionAsset;
    [SerializeField] GameObject generatedInputActionAssetObj;
    private IGeneratedInputActionAsset GeneratedInputActionAsset => generatedInputActionAssetObj.GetComponent<IGeneratedInputActionAsset>();
    [SerializeField] SceneReferenceScriptableObject gameSceneReference;

    [Header("Input Bindings")]
    [SerializeField] string joinActionGamepad = "<Gamepad>/<button>";
    [SerializeField] string joinActionKeyboard = "<Keyboard>/<button>";
    [SerializeField] string joinActionMouse = "<Mouse>/<button>";
    [SerializeField] string leaveActionGamepad = "<Gamepad>/buttonEast";
    [SerializeField] string leaveActionKeyboard = "<Keyboard>/escape";

    [SerializeField] string startGameActionGamepad = "<Gamepad>/buttonWest";
    [SerializeField] string startGameActionKeyboard = "<Keyboard>/space";

    public event Action<IInputActionCollection2> UserCreated;
    public event Action<int> UserDeleted;
    public event Action AllUsersDeleted;

    InputAction joinAction;
    InputAction leaveAction;
    InputAction startGameAction;

    int joinedCount;
    Dictionary<int, bool> playersAreReady = new();

    void Awake()
    {

        Instance = this;

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

        if (!UserDeviceMappingUtil.TryCreateUser(device, GeneratedInputActionAsset, out var newUserInputActions)) return;

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

        var userIndex = InputUser.FindUserPairedToDevice(device).Value.index;
        var userID = InputUser.FindUserPairedToDevice(device).Value.id;

        if (!UserDeviceMappingUtil.TryDeleteUser(device)) return;

        UserDeleted?.Invoke(userIndex);

        playersAreReady.Remove((int)userID);

        joinedCount--;
    }

    /// <summary>
    /// This method starts the game, it can only be activated by the user that created the lobby or mouse and keyboard.
    /// </summary>
    private void StartGame(InputAction.CallbackContext context)
    {
        if (joinedCount != playersAreReady.Count) return;

        var device = context.control.device;

        var userToRemove = InputUser.FindUserPairedToDevice(device).Value;

        if (userToRemove.index == 0 || device is Mouse || device is Keyboard)
        {
            EndJoining();

            SceneManager.LoadScene(gameSceneReference.GetSceneName());
        }    
    }

    public void PlayerIsReady(bool isReady, int playerID)
    {
        if(playersAreReady.ContainsKey(playerID))
        {
            playersAreReady[playerID] = isReady;
        }
        else
        {
            playersAreReady.Add(playerID, isReady);
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
