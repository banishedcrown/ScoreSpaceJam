using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject RowPrefab;
    public GameObject ContentDestination;

    PlayFabManager pm;
    
    // Start is called before the first frame update
    void Awake()
    {
        pm = GameManager.GetManager().playFab;
    }

    // Update is called once per frame
    private void OnEnable()
    {
        pm = GameManager.GetManager().playFab;

        RefreshLeaderBoard();
    }

    

    public void RefreshLeaderBoard()
    {
        pm.GetLeaderBoardMaximumMass();

        Invoke("UpdateMaximumMass", 0.5f);

    }

    void UpdateMaximumMass()
    {
        foreach (Transform t in ContentDestination.transform)
        {
            Destroy(t.gameObject);
        }

        foreach (PlayerLeaderboardEntry item in pm.latestMaximumMassLeaderboard)
        {
            GameObject g = GameObject.Instantiate(RowPrefab, ContentDestination.transform);

            TMP_Text[] content = g.GetComponentsInChildren<TMP_Text>();

            content[0].text = item.Position.ToString();
            content[1].text = item.PlayFabId.ToString();
            content[2].text = (((float)item.StatValue) / 100f).ToString();

            if(content[1].text.Equals(pm.myID))
            {
                content[0].color = Color.red;
                content[1].color = Color.red;
                content[2].color = Color.red;
            }

        }
    }
}
