using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Users;

public class PlayerController : MonoBehaviour
{

    [SerializeField] PlayerInputController inputController;
    [SerializeField] PlayerMovement playerMovement;
    
    public void AssignUser(InputUser user)
    {
        inputController.AssignActions(user.actions);

        var playerStats = PlayerStatsManager.instance.GetPlayerStats(((int)user.id));

        playerMovement.SetStats(playerStats.Speed, playerStats.Weight);
    }
}
