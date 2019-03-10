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
    public DayList dayList;
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
        currentDayScenarios = dayList.days[0].scenarios;
        if (currentDayScenarios != null){
            p1_currentScenario = currentDayScenarios[0];
            p2_currentScenario = currentDayScenarios[0];
        }      
    }

    private void Update() {
        if (currentDay <= dayList.days.Count){
            if ((p1_currentScenario == null) && (p2_currentScenario == null)){ //if both are done (scenario becomes null when all have been used for the day)
                UIManager.ui.DisplayResultPanels();
            } else if ((p1_usedScenarios.Count < currentDayScenarios.Count) && (p2_currentScenario == null)){ //if only p2 is done
                Debug.Log("Wait for player 1 to finish.");
                UIManager.ui.DisplayScenario(p1_currentScenario);
            } else if ((p2_usedScenarios.Count < currentDayScenarios.Count) && (p1_currentScenario == null)){
                Debug.Log("Wait for player 2 to finish.");
                UIManager.ui.DisplayScenario(p2_currentScenario);
            } else{
                Debug.Log("fuck");
                UIManager.ui.DisplayScenario(p2_currentScenario);
                //UIManager.ui.DisplayScenario(p1_currentScenario);
            }
            //DisplayScore();
        }
    }

    public void Player1Clicked(Button b){
        foreach (Option option in p1_currentScenario.p1_situation.options)
        {
            if (option.text == b.GetComponentInChildren<Text>().text){
                p1_script.AddChosenOption(option);
            }
        }
        p1_usedScenarios.Add(p1_currentScenario);
        p1_currentScenario = GetRandomScenario();
        if (p1_currentScenario != null){
            Debug.Log($"current scenario for p1: {p1_currentScenario.Title}");
        }
    }

    public void Player2Clicked(Button b){
        foreach (Option option in p2_currentScenario.p2_situation.options)
        {
            if (option.text == b.GetComponentInChildren<Text>().text){
                p2_script.AddChosenOption(option);
            }
        }
        p2_usedScenarios.Add(p2_currentScenario);
        p2_currentScenario = GetRandomScenario();
        if (p2_currentScenario != null){
            Debug.Log($"current scenario for p2: {p2_currentScenario.Title}");
        }
    }

    public void NextDay(){
        currentDay++;
        currentDayScenarios = dayList.days[currentDay].scenarios;  
    }
    public Scenario GetRandomScenario(){

        int r = Random.Range(0, currentDayScenarios.Count);
        Scenario tempScenario = currentDayScenarios[r];

        /*if (p == p1_script){
            if (p1_usedScenarios.Count != currentDayScenarios.Count){
                foreach (Scenario usedScenario in p1_usedScenarios)
                {
                    if (tempScenario == usedScenario){
                        return GetRandomScenario(p1_script);
                    }
                }
            } else
            {
                return null;
            }
        }
        else if (p == p2_script){
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
        } */

        return tempScenario;
    }

}