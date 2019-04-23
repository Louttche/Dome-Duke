using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager ui;
    
    //Game status UI elements
    public Slider population; //both player's population difference
    public List<Sprite> scenarioBarSprites;
    public Image p1_scenarioBar, p2_scenarioBar;
    public Text p1_population, p2_population; 
    public GameObject p1_kingdomHealth, p2_kingdomHealth;
    
    //Questions
    public Text p1_questiontxt, p2_questiontxt;
    public GameObject endDayPanel;

    //Options
    public Button p1_option1_btn, p1_option2_btn, p2_option1_btn, p2_option2_btn;
    private List<Button> p1_option_buttons = new List<Button>(), p2_option_buttons = new List<Button>();
    
    //Player UI
    public GameObject p1_character, p2_character;
    private Animator p1_showresultsAnimator, p2_showresultsAnimator;
    public GameObject p1_resultsPanel, p2_resultsPanel;
    private Queue<string> p1_scenarioResults, p2_scenarioResults;
    private bool p1_showNextResult, p2_showNextResult;
    public bool fadeIn;

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

        TogglePlayerUI(GameManager.gm.p1_script, false); //For player 1
        TogglePlayerUI(GameManager.gm.p2_script, false); //For player 2
    }

    private IEnumerator FadeToNextDay()
    {
        float endDayPanelAlpha = endDayPanel.GetComponent<Image>().color.a;
        endDayPanel.gameObject.SetActive(true);
        //Fade in
        while(endDayPanelAlpha < 1)
        {
            endDayPanelAlpha += 0.125f;
            endDayPanel.GetComponent<Image>().color = new Color(0, 0, 0, endDayPanelAlpha);
            yield return new WaitForSeconds(0.08f);
        }

        yield return new WaitForSeconds(1);

        foreach (Transform child in endDayPanel.transform)
        {
            child.GetComponent<Text>().text = "";
        }

        TogglePlayerUI(GameManager.gm.p1_script, true); //For player 1
        TogglePlayerUI(GameManager.gm.p2_script, true); //For player 2

        //Fade out
        while(endDayPanelAlpha > 0)
        {
            endDayPanelAlpha -= 0.1f;
            endDayPanel.GetComponent<Image>().color = new Color(0, 0, 0, endDayPanelAlpha);
            yield return new WaitForSeconds(0.08f);
        }

        endDayPanel.gameObject.SetActive(false);
    }
    private void Update() {
        p1_population.text = GameManager.gm.p1_script.currentPopulation.ToString();
        p2_population.text = GameManager.gm.p2_script.currentPopulation.ToString();
        
        SetKingdomHealthUI();
    }

    private void SetKingdomHealthUI()
    {
        if (GameManager.gm.kingdomHealth >= 7){
            p1_kingdomHealth.GetComponentInChildren<Text>().text = "Good";
            p1_kingdomHealth.GetComponentInChildren<Image>().color = Color.green;
            p2_kingdomHealth.GetComponentInChildren<Text>().text = "Good";
            p2_kingdomHealth.GetComponentInChildren<Image>().color = Color.green;
        } else if ((GameManager.gm.kingdomHealth < 7) && (GameManager.gm.kingdomHealth >= 4)){
            p1_kingdomHealth.GetComponentInChildren<Text>().text = "Normal";
            p1_kingdomHealth.GetComponentInChildren<Image>().color = Color.yellow;
            p2_kingdomHealth.GetComponentInChildren<Text>().text = "Normal";
            p2_kingdomHealth.GetComponentInChildren<Image>().color = Color.yellow;
        } else
        {
            p1_kingdomHealth.GetComponentInChildren<Text>().text = "Danger";
            p1_kingdomHealth.GetComponentInChildren<Image>().color = Color.red;
            p2_kingdomHealth.GetComponentInChildren<Text>().text = "Danger";
            p2_kingdomHealth.GetComponentInChildren<Image>().color = Color.red;
        }
    }

    public void showDay(int day){        
        foreach (Transform child in endDayPanel.transform)
        {
            child.GetComponent<Text>().text = "Day " + day;
        }

        endDayPanel.SetActive(true);
        StartCoroutine(UIManager.ui.FadeToNextDay());
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
        //Set the player's avatar to default
        p1_character.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/Player");
        p2_character.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/Player");
        
        //Show results
        if ((p1_showresultsAnimator != null) && (p2_showresultsAnimator != null))
        {
            List<string> p1_currentDayResults = new List<string>(), p2_currentDayResults = new List<string>();
            foreach (Scenario s in GameManager.gm.currentDayScenarios)
            {
                p1_currentDayResults.Add(s.p1_result);
                p2_currentDayResults.Add(s.p2_result);
            }
            StartDialogue(GameManager.gm.p1_script, p1_currentDayResults);
            StartDialogue(GameManager.gm.p2_script, p2_currentDayResults);
        }
    }

    public void TogglePlayerUI(Player p, bool b){
        if (p == GameManager.gm.p1_script){ // if for player 1
            p1_population.transform.parent.gameObject.SetActive(b);
            p1_kingdomHealth.transform.parent.transform.parent.gameObject.SetActive(b);
            p1_option1_btn.gameObject.SetActive(b);
            p1_option2_btn.gameObject.SetActive(b);
            p1_questiontxt.transform.parent.gameObject.SetActive(b);
        }
        else if (p == GameManager.gm.p2_script){
            p2_population.transform.parent.gameObject.SetActive(b);
            p2_kingdomHealth.transform.parent.transform.parent.gameObject.SetActive(b);
            p2_option1_btn.gameObject.SetActive(b);
            p2_option2_btn.gameObject.SetActive(b);
            p2_questiontxt.transform.parent.gameObject.SetActive(b);
        }
    }

    public void StartDialogue(Player p, List<string> results)
    {
        if (p.name == "Player 1"){
            p1_scenarioResults.Clear();
            p1_showresultsAnimator.SetBool("Show", true);
            foreach (string result in results)
            {
                p1_scenarioResults.Enqueue(result);
            }
            p1_showNextResult = true;
        }
        else if (p.name == "Player 2"){
            p2_scenarioResults.Clear();
            p2_showresultsAnimator.SetBool("Show", true);          
            foreach (string result in results)
            {
                p2_scenarioResults.Enqueue(result);
            }
            p2_showNextResult = true;          
        }
        DisplayNextResult(p);
    }

    public void DisplayNextResult(Player p)
    {
        if (p.name == "Player 1"){
            if (p1_scenarioResults.Count == 0)
            {
                EndDialogue(p);
                return;
            } else if (p1_showNextResult == true){
                string result = "";
                result = p1_scenarioResults.Dequeue();
                StartCoroutine(TypeResult(p, result));
            }
        } else if (p.name == "Player 2"){
            if (p2_scenarioResults.Count == 0)
            {
                EndDialogue(p);
                return;
            } else if (p2_showNextResult == true){
                string result = "";
                result = p2_scenarioResults.Dequeue();
                StartCoroutine(TypeResult(p, result));
            }
        }
    }

    IEnumerator TypeResult(Player p, string result)
    {
        if (p.name == "Player 1"){
            p1_showNextResult = false;
            p1_resultsPanel.GetComponentInChildren<Text>().text = "";
            foreach (char letter in result.ToCharArray())
            {
                p1_resultsPanel.GetComponentInChildren<Text>().text += letter;
                yield return null;
            }
            p1_showNextResult = true;
        } else if (p.name == "Player 2"){
            p2_showNextResult = false;
            p2_resultsPanel.GetComponentInChildren<Text>().text = "";
            foreach (char letter in result.ToCharArray())
            {
                p2_resultsPanel.GetComponentInChildren<Text>().text += letter;
                yield return null;
            }
            p2_showNextResult = true;
        }
    }

    public void EndDialogue(Player p){
        if ((p1_showresultsAnimator != null) && (p2_showresultsAnimator != null))
        {
            if (p.name == "Player 1"){
                p1_showresultsAnimator.SetBool("Show", false);
            }               
            else if (p.name == "Player 2"){
                p2_showresultsAnimator.SetBool("Show", false);
            }
            
            if ((p1_showresultsAnimator.GetBool("Show") == false) && (p2_showresultsAnimator.GetBool("Show") == false))
                GameManager.gm.EndDay();
        }
    }

    public void SetOverallPopulation(){
        float playerPopulationDifference = GameManager.gm.p1_script.currentPopulation - GameManager.gm.p2_script.currentPopulation;
        if (playerPopulationDifference == 0)
            population.value = 0.5f;
        else if (playerPopulationDifference > 0){ //Player 1 has the lead
            population.value = 0.5f - Map(playerPopulationDifference, 0, GameManager.gm.p1_script.initialPopulation, 0, 1);
        } else if (playerPopulationDifference < 0) //Player 2 has the lead
        {
            population.value = 0.5f + Map(Mathf.Abs(playerPopulationDifference), 0, GameManager.gm.p2_script.initialPopulation, 0, 1);
        }
    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    public void CharacterSpriteChange(Player p, string spriteName){
        Image currentImage;
        if (p == GameManager.gm.p1_script)
            currentImage = p1_character.GetComponent<Image>();
        else
            currentImage = p2_character.GetComponent<Image>();

        currentImage.sprite = Resources.Load<Sprite>($"Sprites/{spriteName}");
    }
}