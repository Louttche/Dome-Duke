using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System;

/*  PRISONER'S DILEMMA
    (p1,p2) | c = cooperative | d = defect

     - cc = (+5,+5)
     - cd = (-5,+10)
     - dc = (+10,-5)
     - dd = (-10,-10)
     */

public class GameManager : MonoBehaviour
{
    public enum End
    {
        NA,
        DoubleFailure,
        DoubleWin,
        p1Win,
        p2Win
    }
    public End end = End.NA;
    [HideInInspector]
    public int p1_nrOfEvil = 0, p2_nrOfEvil = 0, p1_nrOfGood = 0, p2_nrOfGood = 0;
    
    public static int maxDayScenarios = 5;
    private static int maxKingdomHealth = 10;
    public int kingdomHealth = maxKingdomHealth;
    private bool EndofDay = false;
    public DayList data;
    public GameObject player1, player2;
    [HideInInspector]
    public Player p1_script, p2_script;
    [HideInInspector]
    public static GameManager gm;
    [HideInInspector]
    public int currentDay = 1, r1, r2, Population = 5;
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
        /* -----JSON----- */
        // Get file from local files
        //string path = Path.Combine(Application.streamingAssetsPath, "days.json");
        // Read the file and get the JSON
        //string json = File.ReadAllText(path);
        //data = JsonConvert.DeserializeObject<DayList>(json);

        r1 = UnityEngine.Random.Range(0, 2);
        r2 = UnityEngine.Random.Range(0, 2);
        InitializeScenarios();

        UIManager.ui.showDay(currentDay);
    }

    private void Update() {
        if (EndofDay == false){
            //if both players are done
            if ((p1_usedScenarios.Count == currentDayScenarios.Count) && (p2_usedScenarios.Count == currentDayScenarios.Count)){
                foreach (Scenario s in currentDayScenarios)
                {
                    s.SetScenarioResult();
                    //Debug.Log($"set result for scenario: {s.Title}");
                }
                UIManager.ui.SetOverallPopulation();
                UIManager.ui.DisplayResultPanels();
                EndofDay = true;
            } else { //If neither is done yet
                UIManager.ui.DisplayScenario(p1_script, r1);
                UIManager.ui.DisplayScenario(p2_script, r2);
            }
        }
    }
    
    private void InitializeScenarios()
    {
        currentDayScenarios = GetScenariosForTheDay(maxDayScenarios);//data.days[currentDay-1].scenarios;
        if (currentDayScenarios != null){
            p1_currentScenario = GetRandomScenario(p1_script);
            p2_currentScenario = GetRandomScenario(p2_script);
        }
    }

    private List<Scenario> GetScenariosForTheDay(int nrOfScenarios){
        //Gets a specific amount of random scenarios from that day
        List<Scenario> scenarioList = data.days[currentDay-1].scenarios;

        //Get only the amount needed if number of scenarios exceed the max per day
        while(scenarioList.Count > maxDayScenarios){
            int r = UnityEngine.Random.Range(0, scenarioList.Count);
            Scenario scenarioToRemove = scenarioList[r];

            scenarioList.Remove(scenarioToRemove);
        }

        //if (scenarioList.Count < maxDayScenarios)
        //   return null;

        return scenarioList;
    }

    public void Player1Clicked(Button b){
        r1 = UnityEngine.Random.Range(0, 2);
        foreach (Option option in p1_currentScenario.p1_situation.options)
        {
            if (option.text == b.GetComponentInChildren<Text>().text){
                if (option.dilemma == Option.Dilemma.Cooperative){
                    UIManager.ui.CharacterSpriteChange(p1_script, "Angel");
                    ++p1_nrOfGood;
                }
                else {
                    UIManager.ui.CharacterSpriteChange(p1_script, "Devil");
                    ++p1_nrOfEvil;
                }
                p1_script.chosenOptions.Add(p1_currentScenario, option);
                p1_usedScenarios.Add(p1_currentScenario);
                UIManager.ui.p1_scenarioBar.sprite = UIManager.ui.scenarioBarSprites[p1_usedScenarios.Count];
                p1_currentScenario = GetRandomScenario(p1_script);
                if (p1_currentScenario == null){
                    UIManager.ui.TogglePlayerUI(p1_script, false);
                    p1_currentScenario = new Scenario("Done");
                }

                SetKingdomHealth(option);
            }
        }
    }

    public void Player2Clicked(Button b){
        r2 = UnityEngine.Random.Range(0, 2);
        foreach (Option option in p2_currentScenario.p2_situation.options)
        {
            if (option.text == b.GetComponentInChildren<Text>().text){
                if (option.dilemma == Option.Dilemma.Cooperative) {
                    UIManager.ui.CharacterSpriteChange(p2_script, "Angel");
                    ++p2_nrOfGood;
                }                
                else {
                    UIManager.ui.CharacterSpriteChange(p2_script, "Devil");
                    ++p2_nrOfEvil;
                }
                p2_script.chosenOptions.Add(p2_currentScenario, option);
                p2_usedScenarios.Add(p2_currentScenario);
                UIManager.ui.p2_scenarioBar.sprite = UIManager.ui.scenarioBarSprites[p2_usedScenarios.Count];
                p2_currentScenario = GetRandomScenario(p2_script);
                if (p2_currentScenario == null){
                    UIManager.ui.TogglePlayerUI(p2_script, false);
                    p2_currentScenario = new Scenario("Done");
                }

                SetKingdomHealth(option);
            }
        }      
    }

    private void SetKingdomHealth(Option option){
        //If 'good' option
        if (option.dilemma == Option.Dilemma.Cooperative){
            if (kingdomHealth < maxKingdomHealth)
                kingdomHealth++;
        }
        //If 'evil' option
        else {
            if (kingdomHealth > 0){
                kingdomHealth--;
            } else{
                DoubleFailureGameover();
            }
        }

    }

    public void EndDay(){
        if (++currentDay <= data.days.Count){
            p1_usedScenarios.Clear();
            p2_usedScenarios.Clear();
            InitializeScenarios();
            UIManager.ui.p1_scenarioBar.sprite = UIManager.ui.scenarioBarSprites[0];
            UIManager.ui.p2_scenarioBar.sprite = UIManager.ui.scenarioBarSprites[0];
            UIManager.ui.showDay(currentDay);
        }
        else
            ClassicGameover();

        EndofDay = false;
    }
    
    private void DoubleFailureGameover(){
        //TODO: Display reason of failure
        end = End.DoubleFailure;
        SceneManager.LoadScene("GameOver");
    }

    private void ClassicGameover(){
            if (p1_script.currentPopulation > p2_script.currentPopulation)
            {
                end = End.p1Win;
            } 
            else if (p1_script.currentPopulation < p2_script.currentPopulation)
            {
                end = End.p1Win;
            }
            else
            {
                end = End.DoubleWin;
            }
        SceneManager.LoadScene("GameOver");
    }

    public Scenario GetRandomScenario(Player p){

        int r = UnityEngine.Random.Range(0, currentDayScenarios.Count);
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