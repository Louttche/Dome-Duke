using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager ui;

    //Questions
    public Text p1_questiontxt, p2_questiontxt;

    //Options
    public Button p1_option1_btn, p1_option2_btn, p2_option1_btn, p2_option2_btn;
    public List<Button> p1_option_buttons = new List<Button>(), p2_option_buttons = new List<Button>();
    private Animator p1_showresultsAnimator, p2_showresultsAnimator;

    public GameObject p1_resultsPanel, p2_resultsPanel;

    private Queue<string> scenarioResults;

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

        scenarioResults = new Queue<string>();

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

    public void DisplayScenario(Scenario s){
        if (s == GameManager.gm.p1_currentScenario){ // if for player 1
            p1_questiontxt.text = s.p1_situation.question;
            p1_option1_btn.GetComponentInChildren<Text>().text = s.p1_situation.options[0].text;
            p1_option2_btn.GetComponentInChildren<Text>().text = s.p1_situation.options[1].text;
        } else if (s == GameManager.gm.p2_currentScenario){ // for player 2
            p2_questiontxt.text = s.p2_situation.question;
            p2_option1_btn.GetComponentInChildren<Text>().text = s.p2_situation.options[0].text;
            p2_option2_btn.GetComponentInChildren<Text>().text = s.p2_situation.options[1].text;
        }
    }

    public void DisplayResultPanels(){
        p1_questiontxt.transform.parent.gameObject.SetActive(false);
        p2_questiontxt.transform.parent.gameObject.SetActive(false);
        foreach (Button b in p1_option_buttons)
        {
            b.gameObject.SetActive(false);
        }
        foreach (Button b in p2_option_buttons)
        {
            b.gameObject.SetActive(false);
        }

        if ((p1_showresultsAnimator != null) && (p2_showresultsAnimator != null))
        {
            p1_resultsPanel.GetComponentInChildren<Text>().text = GameManager.gm.p1_currentScenario.p1_situation.GetResult();
            p1_showresultsAnimator.SetBool("Show", true);
            p2_resultsPanel.GetComponentInChildren<Text>().text = GameManager.gm.p2_currentScenario.p2_situation.GetResult();
            p2_showresultsAnimator.SetBool("Show", true);
            //StartDialogue(string[])
        }
    }

    //Show results one by one (Implement 'Dialogue' Manager)

    /*public void StartDialogueForPlayer1(string[] results)
    {
        scenarioResults.Clear();

        foreach (string result in results)
        {
            scenarioResults.Enqueue(result);           
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (scenarioResults.Count == 0)
        {
            if ()
            EndDialogue();
            return;
        }

        string sentence = scenarioResults.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue(Player p){
        if ((p1_showresultsAnimator != null) && (p2_showresultsAnimator != null))
        {
            if (p.name == "Player 1")
                p1_showresultsAnimator.SetBool("Show", false);
            else if (p.name == "Player 2")
                p2_showresultsAnimator.SetBool("Show", false);
        }
    } */
}
