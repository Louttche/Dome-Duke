using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager ui;
    
    public Slider overallScore; //both player's population difference

    public Text p1_happiness, p2_happiness;
    //Questions
    public Text p1_questiontxt, p2_questiontxt;

    //Options
    public Button p1_option1_btn, p1_option2_btn, p2_option1_btn, p2_option2_btn;
    private List<Button> p1_option_buttons = new List<Button>(), p2_option_buttons = new List<Button>();
    private Animator p1_showresultsAnimator, p2_showresultsAnimator;

    public GameObject p1_resultsPanel, p2_resultsPanel;

    private Queue<string> p1_scenarioResults, p2_scenarioResults;

    private void Awake() {
        ui = this;
    }
    private void Start() {

        p1_option_buttons.Add(p1_option1_btn);
        p1_option_buttons.Add(p1_option2_btn);
        p2_option_buttons.Add(p2_option1_btn);
        p2_option_buttons.Add(p2_option2_btn);

        p1_showresultsAnimator = p1_resultsPanel.GetComponent<Animator>();
        p2_showresultsAnimator = p2_resultsPanel.GetComponent<Animator>();

        p1_scenarioResults = new Queue<string>();
        p2_scenarioResults = new Queue<string>();

        //Add the button listeners on the player's options
        foreach (Button b1 in p1_option_buttons)
        {
            foreach (Button b2 in p2_option_buttons)
            {
                b1.onClick.AddListener(() => GameManager.gm.Player1Clicked(b1));
                b2.onClick.AddListener(() => GameManager.gm.Player2Clicked(b2));
            }
        }
    }

    private void Update() {
        p1_happiness.text = GameManager.gm.p1_script.currentPopulation.ToString();
        p2_happiness.text = GameManager.gm.p2_script.currentPopulation.ToString();
    }

    public void DisplayScenario(Player p, int r){
        if (p == GameManager.gm.p1_script){ // if for player 1
            p1_questiontxt.text = GameManager.gm.p1_currentScenario.p1_situation.question;
            if ((p1_option1_btn.IsActive()) && (p1_option2_btn.IsActive())){               
                if (r == 0) {
                    p1_option1_btn.GetComponentInChildren<Text>().text = GameManager.gm.p1_currentScenario.p1_situation.options[r].text;
                    p1_option2_btn.GetComponentInChildren<Text>().text = GameManager.gm.p1_currentScenario.p1_situation.options[r+1].text;
                } else if (r == 1)
                {
                    p1_option1_btn.GetComponentInChildren<Text>().text = GameManager.gm.p1_currentScenario.p1_situation.options[r].text;
                    p1_option2_btn.GetComponentInChildren<Text>().text = GameManager.gm.p1_currentScenario.p1_situation.options[r-1].text;
                }
                
            }           
        }
        else if (p == GameManager.gm.p2_script){
            p2_questiontxt.text = GameManager.gm.p2_currentScenario.p2_situation.question;
            if ((p2_option1_btn.IsActive()) && (p2_option2_btn.IsActive())){
                 if (r == 0) {
                    p2_option1_btn.GetComponentInChildren<Text>().text = GameManager.gm.p2_currentScenario.p2_situation.options[r].text;
                    p2_option2_btn.GetComponentInChildren<Text>().text = GameManager.gm.p2_currentScenario.p2_situation.options[r+1].text;
                } else if (r == 1)
                {
                    p2_option1_btn.GetComponentInChildren<Text>().text = GameManager.gm.p2_currentScenario.p2_situation.options[r].text;
                    p2_option2_btn.GetComponentInChildren<Text>().text = GameManager.gm.p2_currentScenario.p2_situation.options[r-1].text;
                }
            }
        }
    }

    public void DisplayResultPanels(){
        if ((p1_showresultsAnimator != null) && (p2_showresultsAnimator != null))
        {
            List<string> p1_results = new List<string>(), p2_results = new List<string>();
            foreach (Scenario s in GameManager.gm.currentDayScenarios)
            {
                p1_results.Add(s.p1_result);
                p2_results.Add(s.p2_result);
            }
            StartDialogue(GameManager.gm.p1_script, p1_results);
            StartDialogue(GameManager.gm.p2_script, p2_results);
        }
    }

    public void TogglePlayerUI(Player p, bool b){
        if (p == GameManager.gm.p1_script){ // if for player 1
            p1_option1_btn.gameObject.SetActive(b);
            p1_option2_btn.gameObject.SetActive(b);
            p1_questiontxt.transform.parent.gameObject.SetActive(b);
            p1_happiness.transform.parent.gameObject.SetActive(b);
        }
        else if (p == GameManager.gm.p2_script){
            p2_option1_btn.gameObject.SetActive(b);
            p2_option2_btn.gameObject.SetActive(b);
            p2_questiontxt.transform.parent.gameObject.SetActive(b);
            p2_happiness.transform.parent.gameObject.SetActive(b);
        }
    }
    //Show results one by one (Implement 'Dialogue' Manager)

    public void StartDialogue(Player p, List<string> results)
    {
        if (p.name == "Player 1"){
            p1_scenarioResults.Clear();
            p1_showresultsAnimator.SetBool("Show", true);
            p1_resultsPanel.GetComponent<Button>().interactable = true;
            foreach (string result in results)
            {
                p1_scenarioResults.Enqueue(result);
            }
        }
        else if (p.name == "Player 2"){
            p2_scenarioResults.Clear();
            p2_showresultsAnimator.SetBool("Show", true);
            p2_resultsPanel.GetComponent<Button>().interactable = true;
            foreach (string result in results)
            {
                p2_scenarioResults.Enqueue(result);
            }
        }
        DisplayNextResult(p);
    }

    public void DisplayNextResult(Player p)
    {
        string result = "";
        if (p.name == "Player 1"){
            if (p1_scenarioResults.Count == 0)
            {
                EndDialogue(p);
                return;
            }

            result = p1_scenarioResults.Dequeue();
        } else if (p.name == "Player 2"){
            if (p2_scenarioResults.Count == 0)
            {
                EndDialogue(p);
                return;
            }

            result = p2_scenarioResults.Dequeue();
        }

        //StopCoroutine(TypeResult)
        StartCoroutine(TypeResult(p, result));
    }

    IEnumerator TypeResult(Player p, string result)
    {
        if (p.name == "Player 1"){
            p1_resultsPanel.GetComponentInChildren<Text>().text = "";
            foreach (char letter in result.ToCharArray())
            {
                p1_resultsPanel.GetComponentInChildren<Text>().text += letter;
                yield return null;
            }
        } else if (p.name == "Player 2"){
            p2_resultsPanel.GetComponentInChildren<Text>().text = "";
            foreach (char letter in result.ToCharArray())
            {
                p2_resultsPanel.GetComponentInChildren<Text>().text += letter;
                yield return null;
            }
        }
    }

    public void EndDialogue(Player p){
        if ((p1_showresultsAnimator != null) && (p2_showresultsAnimator != null))
        {
            if (p.name == "Player 1"){
                p1_showresultsAnimator.SetBool("Show", false);
                p1_resultsPanel.GetComponent<Button>().interactable = false;
            }               
            else if (p.name == "Player 2"){
                p2_showresultsAnimator.SetBool("Show", false);
                p2_resultsPanel.GetComponent<Button>().interactable = false;
            }
            
            if ((p1_showresultsAnimator.GetBool("Show") == false) && (p2_showresultsAnimator.GetBool("Show") == false))
                GameManager.gm.EndDay();
        }
    }

    public void SetOverallPopulation(){
        float playerPopulationDifference = GameManager.gm.p1_script.currentPopulation - GameManager.gm.p2_script.currentPopulation;
        if (playerPopulationDifference == 0)
            overallScore.value = 0.5f;
        else if (playerPopulationDifference > 0){ //Player 1 has the lead
            overallScore.value = 0.5f - UIManager.ui.Map(playerPopulationDifference, 0, GameManager.gm.p1_script.initialPopulation, 0, 1);
        } else if (playerPopulationDifference < 0) //Player 2 has the lead
        {
            overallScore.value = 0.5f + UIManager.ui.Map(Mathf.Abs(playerPopulationDifference), 0, GameManager.gm.p2_script.initialPopulation, 0, 1);
        }
    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
