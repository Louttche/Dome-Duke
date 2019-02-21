using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Button A, B;

    //public string playerTurn;
    public GameObject TotalScoreUI;
    public GameObject player1, player2;
    public Player p1, p2;

    //Temporary text file stuff
    public TextAsset TextFile;
    protected FileInfo theSourceFile = null;
    protected StreamReader reader = null;

    public List<Scenario> scenarios;

    private void Start()
    {
        LoadScenarios();

        //Get player script component to easily change their values
        p1 = player1.GetComponent<Player>();
        p2 = player2.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        //Update values
        TotalScoreUI.GetComponent<Slider>().value = p2.score - p1.score;
    }

    protected void LoadScenarios()
    {
        theSourceFile = new FileInfo("Assets/Resources/" + TextFile.name + ".txt");
        reader = theSourceFile.OpenText();

        string line;
        
    }

    public void GameOver()
    {
        //Display gameover scene
    }

    public void EndTurn()
    {
        Debug.Log("A chosen.");
        //Check if it's gameover
        //if not switch sides
    }

    private void SwitchTurns()
    {/*
        if (playerTurn.Equals(player1.playerName))
            playerTurn = player2.playerName;
        else
            playerTurn = player1.playerName;*/
    }
}
