using UnityEngine;
using LootLocker.Requests;

public class Leaderboard_Push : MonoBehaviour
{
    public int ID;
    public string playerName;
    public int result;
    private void Start()
    {
        LootLockerSDKManager.StartSession("NotPlayer", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success");
            } else
            {
                Debug.Log("Failed");
            }
        });
    }

    public void SubmitScore()
    {
        if (playerName.Length < 6)
        {
            playerName += "000000";
        }
        LootLockerSDKManager.SubmitScore(playerName, result, ID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Failed");
            }
        });
    }
}
