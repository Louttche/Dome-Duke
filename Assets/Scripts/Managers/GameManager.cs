using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

/*  PRISONER'S DILEMMA
    (p1,p2) | c = cooperative | d = defect

     - cc = (+5,+5)
     - cd = (-5,+10)
     - dc = (+10,-5)
     - dd = (-10,-10)
     */

public class GameManager : MonoBehaviour
{
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

         // Get file from local files
        string path = Path.Combine(Application.streamingAssetsPath, "days.json");
        // Read the file and get the JSON
        string json = File.ReadAllText(path);
        data = JsonConvert.DeserializeObject<DayList>(json);
        Debug.Log(data.days.First().scenarios[0].p1_situation.cc_Result);
        r1 = Random.Range(0, 2);
        r2 = Random.Range(0, 2);
        currentDayScenarios = data.days[currentDay-1].scenarios;
        if (currentDayScenarios != null){
            p1_currentScenario = GetRandomScenario(p1_script);
            //Debug.Log($"player 1 scenario: {p1_currentScenario.Title}");
            p2_currentScenario = GetRandomScenario(p2_script);
        }

        UIManager.ui.showDay(currentDay);
    }

    private void Update() {
        if (EndofDay == false){
            //if both players are done
            if ((p1_usedScenarios.Count == currentDayScenarios.Count) && (p2_usedScenarios.Count == currentDayScenarios.Count)){
                foreach (Scenario s in currentDayScenarios)
                {
                    s.SetScenarioResult();
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

    public void Player1Clicked(Button b){
        r1 = Random.Range(0, 2);
        foreach (Option option in p1_currentScenario.p1_situation.options)
        {
            if (option.text == b.GetComponentInChildren<Text>().text){
                if (option.dilemma == Option.Dilemma.Cooperative)
                    StartCoroutine(UIManager.ui.CharacterSpriteChange(p1_script, "Angel"));
                else
                    StartCoroutine(UIManager.ui.CharacterSpriteChange(p1_script, "Devil"));
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
        r2 = Random.Range(0, 2);
        foreach (Option option in p2_currentScenario.p2_situation.options)
        {
            if (option.text == b.GetComponentInChildren<Text>().text){
                if (option.dilemma == Option.Dilemma.Cooperative)
                    StartCoroutine(UIManager.ui.CharacterSpriteChange(p2_script, "Angel"));
                else
                    StartCoroutine(UIManager.ui.CharacterSpriteChange(p2_script, "Devil"));
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
                //Gameover();
            }
        }
    }

    public void EndDay(){
        if (++currentDay <= data.days.Count){
            p1_usedScenarios.Clear();
            p2_usedScenarios.Clear();
        
            currentDayScenarios = data.days[currentDay-1].scenarios;
            if (currentDayScenarios != null){
                p1_currentScenario = GetRandomScenario(p1_script);
                p2_currentScenario = GetRandomScenario(p2_script);
            }
            UIManager.ui.p1_scenarioBar.sprite = UIManager.ui.scenarioBarSprites[0];
            UIManager.ui.p2_scenarioBar.sprite = UIManager.ui.scenarioBarSprites[0];
            UIManager.ui.showDay(currentDay);
        }
        else{
            Gameover();
        }

        EndofDay = false;
    }
    
    private void Gameover(){
        if (kingdomHealth == 0){
            Debug.Log("Your conflict put the entire kingdom at great risk! Your father is disappointed and neither of you becomes the next King.");
        } else{
            if (p1_script.currentPopulation > p2_script.currentPopulation){
                Debug.Log("Congratulation P1! You proved yourself to your Father and became the next King!");
            } else if (p1_script.currentPopulation < p2_script.currentPopulation)
                Debug.Log("Congratulation P2! You proved yourself to your Father and became the next King!");
            else{
                Debug.Log("Congratulation P1 and P2! You both proved yourselves to the King and ruled the kingdom together!");
            }   
        }
        SceneManager.LoadScene("MenuScene");
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