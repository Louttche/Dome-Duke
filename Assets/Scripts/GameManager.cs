using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalScore = 0;
    public string playerTurn;
    public List<Scenario> scenarios;
    public Player player1, player2;

    private void Start()
    {
        //LoadScenarios() (from text file probs)
        player1 = new Player();
        player1.playerName = "Tom";
        player2 = new Player();
        player2.playerName = "Bob";

        playerTurn = player1.playerName; //set player 1 to start first
    }

    public void GameOver()
    {
        //Display gameover scene
    }

    public void EndTurn()
    {
        //Check if it's gameover
        //if not switch sides
    }

    private void SwitchTurns()
    {
        if (playerTurn.Equals(player1.playerName))
            playerTurn = player2.playerName;
        else
            playerTurn = player1.playerName;
    }
}
