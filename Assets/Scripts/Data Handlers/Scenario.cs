using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Scenario
{
    // Constants for the population increase or decrease.
    private const int bigPopulationDecrease = -10;
    private const int populationDecrease = -5;
    private const int bigPopulationIncrease = 10;
    private const int populationIncrease = 5;

    public string Title;
    public Situation p1_situation, p2_situation;
    public string p1_result, p2_result;

    //Constructor to create a waiting state when a player is done before the other
    public Scenario(string title)
    {
        this.Title = title;
        this.p1_situation = new Situation("Wait for player 2");
        this.p2_situation = new Situation("Wait for player 1");
    }

    public void SetScenarioResult()
    {
        // Get the options chosen by the players for this scenario.
        var p1_option = GameManager.gm.p1_script.chosenOptions.FirstOrDefault(x => x.Key == this);
        var p2_option = GameManager.gm.p1_script.chosenOptions.FirstOrDefault(x => x.Key == this);

        // Get a shortcut to the dilemmas for those options.
        var p1Dilemma = p1_option.Value.dilemma;
        var p2Dilemma = p2_option.Value.dilemma;

        //Set this scenario's results based on both player's answers
        if (p1Dilemma == Option.Dilemma.Cooperative)
        {
            if (p2Dilemma == Option.Dilemma.Cooperative)
            {
                UpdatePlayerValues(1, populationIncrease, p1_situation.cc_Result);
                UpdatePlayerValues(2, populationIncrease, p2_situation.cc_Result);
            }
            else if (p2Dilemma == Option.Dilemma.Defect)
            {
                UpdatePlayerValues(1, populationDecrease, p1_situation.cd_Result);
                UpdatePlayerValues(2, bigPopulationIncrease, p2_situation.cd_Result);
            }
        }
        else if (p1Dilemma == Option.Dilemma.Defect)
        {
            if (p2Dilemma == Option.Dilemma.Cooperative)
            {
                UpdatePlayerValues(1, bigPopulationIncrease, p1_situation.dc_Result);
                UpdatePlayerValues(2, populationDecrease, p2_situation.dc_Result);
            }
            else if (p2Dilemma == Option.Dilemma.Defect)
            {
                UpdatePlayerValues(1, bigPopulationDecrease, p1_situation.dd_Result);
                UpdatePlayerValues(2, bigPopulationDecrease, p2_situation.dd_Result);
            }
        }
    }

    /// <summary>
    /// Updates the live values for the player based on the given values.
    /// </summary>
    /// <param name="player">The int which represents the player number. Currently 1 or 2.</param>
    /// <param name="populationIncrease">
    /// The amount that the population increases. Provide a negative number for a decrease.
    /// </param>
    /// <param name="result">The result wanting to be set for the player.</param>
    private void UpdatePlayerValues(int player, int populationIncrease, string result)
    {
        if (result == null)
        {
            result = string.Empty;
        }

        if (populationIncrease > 0)
        {
            result += "\nPopulation Increased";
        }
        else
        {
            result += "\nPopulation Decreased";
        }

        switch (player)
        {
            case 1:
                p1_result = result;
                GameManager.gm.p1_script.currentPopulation += populationIncrease;
                break;
            case 2:
                p2_result = result;
                GameManager.gm.p2_script.currentPopulation += populationIncrease;
                break;
            default:
                throw new ArgumentException();
        }
    }
}