using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayOptions : MonoBehaviour
{
    public Button option1_btn, option2_btn;
    [HideInInspector]
    public Option option1_script, option2_script;
    public Text option1_txt, option2_txt;    
    void Start()
    {
        option1_btn = this.transform.Find("Option_1").GetComponent<Button>();
        option2_btn = this.transform.Find("Option_2").GetComponent<Button>();
        option1_txt = this.transform.Find("Option_1").GetComponentInChildren<Text>();
        option2_txt = this.transform.Find("Option_2").GetComponentInChildren<Text>();
        option1_script = this.transform.Find("Option_1").GetComponent<Option>();
        option2_script = this.transform.Find("Option_2").GetComponent<Option>();

    }

    public void SetOption(){
        //option1_script.dilemma = Option.Dilemma.Cooperative;
    }
    /*public void SetOptionToRandomButton() //Randomly set the dilemma on the buttons so its not always on the same one
    {
        int r = Random.Range(1, 3);
        if (r == 1){
            option1.dilemma = Option.Dilemma.Cooperative;
            option2.dilemma = Option.Dilemma.Defect;
        }
        else {
            option1.dilemma = Option.Dilemma.Defect;
            option2.dilemma = Option.Dilemma.Cooperative;
        }
    } */

    void Update()
    {/*
        if (this.transform.parent.GetComponent<DisplayQuestion>().parentName == "Player 1")
        {
            option1_txt.text = "player 1 option 1";
            option2_txt.text = "player 1 option 2";
        }
        else if (this.transform.parent.GetComponent<DisplayQuestion>().parentName == "Player 2")
        {
            option1_txt.text = "player 2 option 1";
            option2_txt.text = "player 2 option 2";
        }*/
    }
}
