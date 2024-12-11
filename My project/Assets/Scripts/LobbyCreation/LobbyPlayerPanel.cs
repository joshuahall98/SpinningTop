using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerStats
{
    public int Speed;
    public int Weight;
   
    public PlayerStats(int speed, int weight)
    {
        Speed = speed;
        Weight = weight;
    }
}

public class LobbyPlayerPanel : MonoBehaviour
{

    public enum Row
    {
        Top,
        Middle,
        Bottom
    }

    [Header("Base Stats")]
    [SerializeField] int baseSpeed = 15;
    [SerializeField] int baseWeight = 1;

    [Header("Selection Panel")]
    [SerializeField] TextMeshProUGUI playerIDTxt;
    [SerializeField] Button readyUpBtn;
    [SerializeField] TMP_Text readyUpTxt;
    [SerializeField] TMP_Text topSelection;
    [SerializeField] TMP_Text midSelection;
    [SerializeField] TMP_Text bottomSelection;

    [Header("Stats Panel")]
    [SerializeField] List<TopStatsScriptableObject> topStats;
    int currentTopStat;
    [SerializeField] List<MidStatsScriptableObject> midStats;
    int currentMidStat;
    [SerializeField] List<BottomStatsScriptableObject> bottomStats;
    int currentBottomStat;

    [SerializeField] Slider speedSlider;
    [SerializeField] Slider weightSlider;

    int currentSpeed;
    int currentWeight;

    bool ready = false;
    int playerID;
    private bool isDefaultNavigation = true;

    Navigation defaultReadyUpBtnNavigation;

    private void Start()
    {

        defaultReadyUpBtnNavigation = readyUpBtn.navigation;

        readyUpBtn.onClick.AddListener(ReadyUp);

        NextSelection(0, Row.Top);
        NextSelection(0, Row.Middle);
        NextSelection(0, Row.Bottom);

        UpdateStats();
    }

    public void SetPlayerID(int playerID)
    {
        playerIDTxt.text = "Player " + playerID;
        this.playerID = playerID;
    }

    public Button GetReadyUpBtn()
    {
        return readyUpBtn;
    }

    private void ReadyUp()
    {
        if(isDefaultNavigation)
        {
            readyUpBtn.navigation = new Navigation { mode = Navigation.Mode.None };
            readyUpTxt.text = "Unready";
        }
        else
        {
            readyUpBtn.navigation = defaultReadyUpBtnNavigation;
            readyUpTxt.text = "Ready Up";
        }

        LocalMultiplayerLobby.Instance.PlayerIsReady(isDefaultNavigation, playerID);

        isDefaultNavigation = !isDefaultNavigation;

        PlayerStatsManager.instance.UpdatePlayerStats(new PlayerStats(currentSpeed, currentWeight), playerID);
        
    }

    private void UpdateStats()
    {
        currentSpeed = baseSpeed + bottomStats[currentBottomStat].speed;
        speedSlider.value = currentSpeed;

        currentWeight = baseWeight + topStats[currentTopStat].weight + midStats[currentMidStat].weight + bottomStats[currentBottomStat].weight;
        weightSlider.value = currentWeight;
    }

    public void NextSelection(int direction, Row row)
    {
        switch (row)
        {
            case Row.Top:
                currentTopStat = SetSelection(direction, currentTopStat, topStats.Count - 1);
                topSelection.text = topStats[currentTopStat].name;
                break;
            case Row.Middle:
                currentMidStat = SetSelection(direction, currentMidStat, midStats.Count - 1);
                midSelection.text = midStats[currentMidStat].name;
                break;
            case Row.Bottom:
                currentBottomStat = SetSelection(direction, currentBottomStat, bottomStats.Count - 1);
                bottomSelection.text = bottomStats[currentBottomStat].name;
                break;
        }

        UpdateStats();
    }

    private int SetSelection(int direction, int currentStat, int statsCount)
    {
        currentStat += direction;

        if (currentStat < 0)
        {
            return statsCount;
        }
        else if(currentStat > statsCount)
        {
            return 0;
        }

        return currentStat;
        
    }
}
