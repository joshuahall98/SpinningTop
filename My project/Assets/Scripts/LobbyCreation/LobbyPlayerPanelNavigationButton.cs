using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LobbyPlayerPanelNavigationButton : MonoBehaviour
{

    private enum Direction
    {
        Next,
        Previous
    }

    [SerializeField] LobbyPlayerPanel lobbyPlayerPanel;

    [SerializeField] LobbyPlayerPanel.Row row;

    [SerializeField] Direction direction;

    private void Start()
    {
        var btn = transform.GetComponent<Button>();
        btn.onClick.AddListener(NextSelection);
    }

    private void NextSelection()
    {
        if(direction == Direction.Next)
        {
            lobbyPlayerPanel.NextSelection(1, row);
        }
        else
        {
            lobbyPlayerPanel.NextSelection(-1, row);
        }
    }
}
