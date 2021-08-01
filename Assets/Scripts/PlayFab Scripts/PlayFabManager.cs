using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{

    public List<PlayerLeaderboardEntry> latestMaximumMassLeaderboard;
    public List<PlayerLeaderboardEntry> latestTimeSurvivedLeaderboard;
    public List<PlayerLeaderboardEntry> latestTotalMassConsumedLeaderboard;

    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

   void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);

    }

    void OnSuccess(LoginResult result){
        Debug.Log("Successful Login/Account Create");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error from Playfab: " + error.GenerateErrorReport());
    }

    public void SendAllScores(double MaxMass, double TimeSurvived)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "MaximumMass",
                    Value = (int) (MaxMass * 100),
                },
                new StatisticUpdate
                {
                    StatisticName = "Time Survived",
                    Value = (int) (TimeSurvived * 100),
                },
                new StatisticUpdate
                {
                    StatisticName = "Total Mass Consumed",
                    Value = (int) (MaxMass * 100),
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError);
    }


    void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard Updated Succesfully");
    }


    public void GetLeaderBoardMaximumMass()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "MaximumMass",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGetMaximumMass, OnError);
    }
    
    public void GetLeaderBoardTimeSurvived()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Time Survived",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGetTimeSurvived, OnError);
    }
    
    public void GetLeaderBoardTotalMassConsumed()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Total Mass Consumed",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGetTotalMassConsumed, OnError);
    }

    public void OnLeaderBoardGetMaximumMass( GetLeaderboardResult result)
    {
        latestMaximumMassLeaderboard.Clear();
        foreach(PlayerLeaderboardEntry item in result.Leaderboard)
        {
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
            latestMaximumMassLeaderboard.Add(item);
        }
    }
    public void OnLeaderBoardGetTimeSurvived( GetLeaderboardResult result)
    {
        latestTimeSurvivedLeaderboard.Clear();
        foreach(PlayerLeaderboardEntry item in result.Leaderboard)
        {
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
            latestTimeSurvivedLeaderboard.Add(item);
        }
    }
    public void OnLeaderBoardGetTotalMassConsumed( GetLeaderboardResult result)
    {
        latestTotalMassConsumedLeaderboard.Clear();
        foreach(PlayerLeaderboardEntry item in result.Leaderboard)
        {
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
            latestTotalMassConsumedLeaderboard.Add(item);
        }
    }
}
