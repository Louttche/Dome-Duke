using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/*  PRISONER'S DILEMMA
    (p1,p2) | c = cooperative | d = defect

     - cc = (+5,+5)
     - cd = (-5,+10)
     - dc = (+10,-5)
     - dd = (-10,-10)
     */

public class GameManager : MonoBehaviour
{
    public Dictionary<int, Scenario> scenarios_dictionary = new Dictionary<int, Scenario>();
    public ScenarioObjectList player1_scenarios, player2_scenarios;
    private int maxDays = 3;
    public int currentDay = 1;
    private float dayTimer = 10; //seconds for each day
    public GameObject player1, player2;
    [HideInInspector]
    public Player p1_script, p2_script;
    public static GameManager gm;
    public DisplayQuestion player1_question, player2_question;
    public DisplayOptions player1_options, player2_options;

    //Temporary text file stuff
    public TextAsset TextFile;
    protected FileInfo theSourceFile = null;
    protected StreamReader reader = null;

    private void Awake()
    {
        //LoadScenarios();
        gm = this;
    }

    void Start()
    {
        //Get player script component to easily change their values
        p1_script = player1.GetComponent<Player>();
        p2_script = player2.GetComponent<Player>();

        //player1_scenarios = JsonUtility.FromJson<ScenarioObjectList>(@"Assets\Resources\p1_scenarios");

        string json = File.ReadAllText(@"Assets\Resources\p1_scenarios.json");
        player1_scenarios = JsonUtility.FromJson<ScenarioObjectList>(json);

        Debug.LogFormat($"{player1_scenarios.p1_scenarioList[1]}");
        /*for (int i = 1; i <= 15; i++)
        {
            scenarios_dictionary.Add(i,player1_scenarios.scenarioList[i]);
        } 

        foreach (KeyValuePair<int,Scenario> entry in scenarios_dictionary)
        {
            Debug.LogFormat($"{entry.Key} : {entry.Value}");
        } */
    }

    void Update()
    {
        if (currentDay <= maxDays){
            if (!endofDay()){
                //Show 5 questions
            }
            else {
                SetScore();
                //DisplayScore();
                currentDay++;
            }
        }            
    }

    public void SetScore(){
        foreach (Option p1_o in p1_script.chosenOptions)
        {
            foreach (Option p2_o in p2_script.chosenOptions)
            {
                if (p1_o.dilemma == Option.Dilemma.Cooperative)
                {
                    if (p2_o.dilemma == Option.Dilemma.Cooperative){
                        p1_script.score += 5;
                        p2_script.score += 5;                     
                    } else if (p2_o.dilemma == Option.Dilemma.Defect){
                        p1_script.score -= 5;
                        p2_script.score += 10;
                    }
                } else if (p1_o.dilemma == Option.Dilemma.Defect){
                    if (p2_o.dilemma == Option.Dilemma.Cooperative){
                        p1_script.score += 10;
                        p2_script.score -= 5;                     
                    } else if (p2_o.dilemma == Option.Dilemma.Defect){
                        p1_script.score -= 10;
                        p2_script.score -= 10;
                    }
                }
            }
        }
    }
    public bool endofDay()
    {
        //Debug.LogFormat($"seconds: {dayTimer}");
        if (dayTimer >= 0.0f){
            dayTimer -= Time.deltaTime;
            return false;
        }
        else{
            dayTimer = 10;
            return true;
        }
    }

    /*protected void LoadScenarios()
    {
        theSourceFile = new FileInfo("Assets/Resources/" + TextFile.name + ".txt");
        reader = theSourceFile.OpenText();

        string AllText = reader.ReadToEnd();
        string[] scenariosText= AllText.Split(';');

        foreach (string sText in scenariosText)
        {
            string[] lines = sText.Split('\n');
            Scenario s = new Scenario(lines[0]);

            for (int l = 1; l < lines.Length; l++)
            {//add code to check which type the choice is
                s.AddChoice(lines[l], Choice.Dilemma.Cooperative);
            }

            scenarios.Add(s);
        }
    }*/
}
