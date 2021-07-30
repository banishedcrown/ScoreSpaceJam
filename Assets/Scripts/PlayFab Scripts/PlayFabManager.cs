using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{
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
                    StatisticName = "Totoal Mass Consumed",
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
}
