using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager instance;

    Dictionary<int, PlayerStats> playerStatsList = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    public void UpdatePlayerStats(PlayerStats stats, int playerID)
    {
        if (playerStatsList.ContainsKey(playerID))
        {
            playerStatsList[playerID] = stats;
        }
        else
        {
            playerStatsList.Add(playerID, stats);
        }
    }

    public PlayerStats GetPlayerStats(int playerID)
    {
        return playerStatsList[playerID];
    }
}
