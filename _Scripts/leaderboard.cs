using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
public class leaderboard : MonoBehaviour
{
    public int leaderBoardID = 21300;
    public string leaderBoardKey = "iitLB";
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Login());
    }

    public void fetchScores()
    {
        StartCoroutine(FetchScores());
    }

    IEnumerator Login()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("done login");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("erro in starting session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done = false);
    }

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(PlayerPrefs.GetString("PlayerName", ""), (response) =>
        {

            if (response.success)
            {
                Debug.Log("succes player name");
            }
            else
            {
                Debug.Log("failed player name");
            }
        });
    }
    public IEnumerator SubmitScore(int score)
    {
       
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID", "0");
        LootLockerSDKManager.SubmitScore(playerID, score, leaderBoardKey, (response) =>
             {
                 if (response.success)
                 {
                     Debug.Log("success score");
                     done = true;
                 }
                 else
                 {
                     Debug.Log("failed score");
                     done = true;
                 }
             });
        yield return new WaitWhile(() => done = false);
    }
    public IEnumerator FetchScores()
    {
        SetPlayerName();
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderBoardKey,10,0, (response) =>
        {
            if (response.success)
            {
                string tempNames = "Names\n";
                string tempScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                for(int i = 0; i<members.Length; i++)
                {
                    tempNames += members[i].rank + " . ";
                    if(members[i].player.name != "")
                    {
                        tempNames += members[i].player.name;
                    }
                    else
                    {
                        tempNames += members[i].player.id;
                    }
                    tempScores += members[i].score + "\n";
                    tempNames += "\n";
                }
                Debug.Log("success fetch");
                done = true;
                playerNames.text = tempNames;
                playerScores.text = tempScores;
            }
            else
            {
                Debug.Log("failed score");
                done = true;
            }
        });
        yield return new WaitWhile(() => done = false);
    }
}
