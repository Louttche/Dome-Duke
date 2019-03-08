using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    //Questions
    public Text p1_questiontxt, p2_questiontxt;

    //Options
    public Button p1_option1_btn, p1_option2_btn, p2_option1_btn, p2_option2_btn;
    public List<Button> option_buttons = new List<Button>();
    private Option option;
    private Animator p1_showresultsAnimator, p2_showresultsAnimator;

    public GameObject p1_resultsPanel, p2_resultsPanel;
    private UnityAction OptionClicked;

    private void Start() {
        option_buttons.Add(p1_option1_btn);
        option_buttons.Add(p1_option2_btn);
        option_buttons.Add(p2_option1_btn);
        option_buttons.Add(p2_option2_btn);
        
        p1_showresultsAnimator = p1_resultsPanel.GetComponent<Animator>();
        p2_showresultsAnimator = p2_resultsPanel.GetComponent<Animator>();
    }

    private void Update() {
        if (DayManager.dm.dayList.days != null){
            DisplayOptionsForScenario(GameManager.gm.currentScenario);
            DisplayQuestionForScenario(GameManager.gm.currentScenario);
        }

        /* -Testing the result panels-
        if (Input.GetKeyUp(KeyCode.Alpha1)){
            if (p1_showresults != null){
                bool show = p1_showresults.GetBool("Show");
                p1_showresults.SetBool("Show", !show);
            }
        } else if (Input.GetKeyUp(KeyCode.Alpha2)){
            if (p2_showresults != null){
                bool show = p2_showresults.GetBool("Show");
                p2_showresults.SetBool("Show", !show);
            }
        } */
    }

    void DisplayOptionsForScenario(Scenario s){
        p1_option1_btn.GetComponentInChildren<Text>().text = s.p1_situation.options[0].text;
        p1_option2_btn.GetComponentInChildren<Text>().text = s.p1_situation.options[1].text;
        p2_option1_btn.GetComponentInChildren<Text>().text = s.p2_situation.options[0].text;
        p2_option2_btn.GetComponentInChildren<Text>().text = s.p2_situation.options[1].text;
    }
    void DisplayQuestionForScenario(Scenario s){
        p1_questiontxt.text = s.p1_situation.question;
        p2_questiontxt.text = s.p2_situation.question;
    }

    public void DisplayResultPanels(){
        if ((p1_showresultsAnimator != null) && (p2_showresultsAnimator != null))
        {
            p1_showresultsAnimator.GetComponentInChildren<Text>().text = GameManager.gm.currentScenario.p1_situation.GetResult();
            p1_showresultsAnimator.SetBool("Show", true);
            p2_showresultsAnimator.GetComponentInChildren<Text>().text = GameManager.gm.currentScenario.p2_situation.GetResult();
            p2_showresultsAnimator.SetBool("Show", true);
        }
    }

    public void HideResultPanel(Player p){
        if ((p1_showresultsAnimator != null) && (p2_showresultsAnimator != null))
        {
            if (p.name == "Player 1")
                p1_showresultsAnimator.SetBool("Show", false);
            else if (p.name == "Player 2")
                p2_showresultsAnimator.SetBool("Show", false);
        }
    }

    //Show results one by one (Implement 'Dialogue' Manager)
}
