using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class Option
{
    public enum Dilemma
    {
        Cooperative,
        Defect
    }
    public string text;
    //public int Mcost, Rcost;
    public Dilemma dilemma;
    private Button option_btn;

    public void OptionClicked(Button b){
            if (b.GetComponentInChildren<Text>().text == this.text){
                if (b.transform.parent.transform.parent.name == "Player 1")
                    GameManager.gm.p1_script.AddChosenOption(this);
                else if (b.transform.parent.transform.parent.name == "Player 2")
                    GameManager.gm.p2_script.AddChosenOption(this); 
            }
    }
}
