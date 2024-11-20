using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class LobbyPlayerInputSetup : MonoBehaviour, ILobbyPlayerInputSetup
{
    [SerializeField] InputActionReference submit;
    //[SerializeField] InputActionReference move;

    public void SetupPlayerPanel(GameObject playerPanel, MultiplayerEventSystem multiplayerEventSystem)
    {
        multiplayerEventSystem.playerRoot = playerPanel;
        multiplayerEventSystem.firstSelectedGameObject = playerPanel.GetComponent<LobbyPlayerPanel>().GetReadyUpBtn().gameObject;
    }

    public void SetupPlayerUIControls(InputActionAsset inputActions, InputSystemUIInputModule inputSystemUIInputModule)
    {
        inputSystemUIInputModule.submit = InputActionReference.Create(inputActions.FindAction(submit.name));

    }
}
