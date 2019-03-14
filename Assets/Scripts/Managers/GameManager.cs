using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*  PRISONER'S DILEMMA
    (p1,p2) | c = cooperative | d = defect

     - cc = (+5,+5)
     - cd = (-5,+10)
     - dc = (+10,-5)
     - dd = (-10,-10)
     */

public class GameManager : MonoBehaviour
{
    private bool tempGameOver = false;
    public DayList data;
    public GameObject player1, player2;

    [HideInInspector]
    public Player p1_script, p2_script;

    [HideInInspector]
    public static GameManager gm;
    [HideInInspector]
    public int currentDay = 1;
    [HideInInspector]
    public List<Scenario> currentDayScenarios;
    public Scenario p1_currentScenario, p2_currentScenario;

    public List<Scenario> p1_usedScenarios, p2_usedScenarios;

    private void Awake()
    {
        gm = this;

        //Get player script component to easily change their values
        p1_script = player1.GetComponent<Player>();
        p2_script = player2.GetComponent<Player>();
    }

    private void Start() {
        currentDayScenarios = data.days[0].scenarios;
        if (currentDayScenarios != null){
            p1_currentScenario = GetRandomScenario(p1_script);
            //Debug.Log($"player 1 scenario: {p1_currentScenario.Title}");
            p2_currentScenario = GetRandomScenario(p2_script);
        }      
    }

    private void Update() {
        //if (currentDay <= data.days.Count){
        if (tempGameOver == false){
            if ((p1_usedScenarios.Count == currentDayScenarios.Count) && (p2_usedScenarios.Count == currentDayScenarios.Count)){ //if both are done (scenario becomes null when all have been used for the day)
                foreach (Scenario s in currentDayScenarios)
                {
                    s.SetScenarioResult();
                }
                UIManager.ui.DisplayResultPanels();
                tempGameOver = true;
                //EndDay();
            } else {
                UIManager.ui.DisplayScenario(p1_script);
                UIManager.ui.DisplayScenario(p2_script);
            }
            //DisplayScore();
        }        
        //else {
            //Gameover();
        //}
    }

    public void Player1Clicked(Button b){
        foreach (Option option in p1_currentScenario.p1_situation.options)
        {
            if (option.text == b.GetComponentInChildren<Text>().text){
                p1_script.chosenOptions.Add(p1_currentScenario, option);
                p1_usedScenarios.Add(p1_currentScenario);
                p1_currentScenario = GetRandomScenario(p1_script);
                if (p1_currentScenario == null){
                    UIManager.ui.ToggleButtons(p1_script, false);
                    p1_currentScenario = new Scenario("Done");
                }
            }
        }
    }

    public void Player2Clicked(Button b){
        foreach (Option option in p2_currentScenario.p2_situation.options)
        {
            if (option.text == b.GetComponentInChildren<Text>().text){
                p2_script.chosenOptions.Add(p2_currentScenario, option);
                p2_usedScenarios.Add(p2_currentScenario);
                p2_currentScenario = GetRandomScenario(p2_script);
                if (p2_currentScenario == null){
                    UIManager.ui.ToggleButtons(p2_script, false);
                    p2_currentScenario = new Scenario("Done");
                }
            }
        }      
    }

    public void EndDay(){
        currentDay++;
        currentDayScenarios = data.days[currentDay].scenarios;
        p1_currentScenario = GetRandomScenario(p1_script);
        p2_currentScenario = GetRandomScenario(p2_script);
    }
    
    public Scenario GetRandomScenario(Player p){

        int r = Random.Range(0, currentDayScenarios.Count);
        Scenario tempScenario = currentDayScenarios[r];

        if ((p == p1_script) && (p1_usedScenarios.Count > 0)){ //if its player 1
            if (p1_usedScenarios.Count < currentDayScenarios.Count){               
                foreach (Scenario usedScenario in p1_usedScenarios)
                {
                    if (tempScenario == usedScenario){
                        return GetRandomScenario(p1_script);
                    }
                }
            } else //if they are done with the day's questions
            {
                return null;
            }
        }
        else if ((p == p2_script) && (p2_usedScenarios.Count > 0)){
            if (p2_usedScenarios.Count != currentDayScenarios.Count){
                foreach (Scenario usedScenario in p2_usedScenarios)
                {
                    if (tempScenario == usedScenario){
                        return GetRandomScenario(p2_script);
                    }
                }    
            } else
            {
                return null;
            }
        }

        return tempScenario;
    }

}