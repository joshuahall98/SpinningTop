using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class LobbyPlayerCreation : MonoBehaviour
{
    [SerializeField] LocalMultiplayerLobby localMultiplayerLobby;

    [SerializeField] GameObject lobbyPlayerPrefab;
    [SerializeField] GameObject multiplayerEventSystemPrefab;

    [Header("Lobby UI")]
    [SerializeField] GameObject playerPanelPrefab;
    [SerializeField] GridLayoutGroup playerPanelUIGrid;

    List<GameObject> currentLobbyPlayers = new List<GameObject>();
    List<GameObject> multiplayerEventSystems = new List<GameObject>();
    List<GameObject> playerPanels = new List<GameObject>();

    bool startCalled;

    private void Start()
    {
        SubscribeToEvents();
        startCalled = true;
    }

    void CreatePlayer(IInputActionCollection2 newUserInputActions)
    {
        var newLobbyPlayer = Instantiate(lobbyPlayerPrefab);

        currentLobbyPlayers.Add(newLobbyPlayer);

        var playerPanel = CreatePlayerUI();

        var multiplayerEventSystemObj = Instantiate(multiplayerEventSystemPrefab);
        multiplayerEventSystems.Add(multiplayerEventSystemObj);

        var multiplayerEventSystem = multiplayerEventSystemObj.GetComponent<MultiplayerEventSystem>();
        var inputSystemUIInputModule = multiplayerEventSystemObj.GetComponent<InputSystemUIInputModule>();

        var localPlayerLobbyInputSetup = newLobbyPlayer.GetComponent<ILobbyPlayerInputSetup>();

        localPlayerLobbyInputSetup.SetupPlayerUIControls(newUserInputActions, inputSystemUIInputModule);

        if (playerPanel != null)
        {
            multiplayerEventSystem.playerRoot = playerPanel;
            //multiplayerEventSystem.firstSelectedGameObject = playerPanel.GetComponent<LobbyPlayerPanel>().GetReadyUpBtn().gameObject;
            multiplayerEventSystem.SetSelectedGameObject(playerPanel.GetComponent<LobbyPlayerPanel>().GetReadyUpBtn().gameObject);
        }
    }

    void DeletePlayer(int userIndex)
    {
        DestroyPlayerUI(userIndex);

        var playerToRemove = currentLobbyPlayers[userIndex];
        currentLobbyPlayers.RemoveAt(userIndex);
        Destroy(playerToRemove);

        var multiplayerEventSystemToRemove = multiplayerEventSystems[userIndex];
        multiplayerEventSystems.RemoveAt(userIndex);
        Destroy(multiplayerEventSystemToRemove);
    }

    private GameObject CreatePlayerUI()
    {
        var playerPanel = Instantiate(playerPanelPrefab, playerPanelUIGrid.transform);
        playerPanels.Add(playerPanel);
        playerPanel.GetComponent<LobbyPlayerPanel>().SetPlayerID(playerPanels.Count);
        return playerPanel;
    }

    void DestroyPlayerUI(int playerIndex)
    {
        var playerPanel = playerPanels[playerIndex];
        playerPanels.RemoveAt(playerIndex);
        Destroy(playerPanel);

        for (int i = 0; i < playerPanels.Count; i++)
        {
            playerPanels[i].GetComponent<LobbyPlayerPanel>().SetPlayerID(i + 1);
        }
    }

    private void SubscribeToEvents()
    {
        localMultiplayerLobby.UserCreated += CreatePlayer;
        localMultiplayerLobby.UserDeleted += DeletePlayer;
    }

    private void OnEnable()
    {
        if(startCalled)
        {
            SubscribeToEvents();
        }
    }

    private void OnDisable()
    {
        localMultiplayerLobby.UserCreated -= CreatePlayer;
    }
}
