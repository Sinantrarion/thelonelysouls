using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool secondGame;
    public List<bool> causesList;
    public List<int> reputation;
    public int savedStage;

    public PlayerData (ChatBox_Filler filler)
    {
        secondGame = filler.newGame;
        causesList = filler.causesList;
        reputation = filler.reputation;
        savedStage = filler.savedStage;
    }
}
