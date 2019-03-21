using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Scenario
{
    public string Title;
    public Situation p1_situation, p2_situation;
    
    [HideInInspector]
    public string p1_result, p2_result;

    //Constructor to create a waiting state when a player is done before the other
    public Scenario(string title){
        this.Title = title;
        this.p1_situation = new Situation("Wait for player 2");
        this.p2_situation = new Situation("Wait for player 1");
    }

    public void SetScenarioResult(){
        foreach (KeyValuePair<Scenario, Option> p1_option in GameManager.gm.p1_script.chosenOptions)
        {
            if (p1_option.Key == this){
                foreach (KeyValuePair<Scenario, Option> p2_option in GameManager.gm.p2_script.chosenOptions)
                {   //Set this scenario's results based on both player's answers
                    if (p2_option.Key == this){
                        if (p1_option.Value.dilemma == Option.Dilemma.Cooperative){
                            if (p2_option.Value.dilemma == Option.Dilemma.Cooperative){
                                GameManager.gm.p1_script.currentPopulation += 5;
                                GameManager.gm.p2_script.currentPopulation += 5;
                                p1_result = p1_situation.cc_Result;
                                p2_result = p2_situation.cc_Result;
                            }
                            else if (p2_option.Value.dilemma == Option.Dilemma.Defect){
                                GameManager.gm.p1_script.currentPopulation -= 5;
                                GameManager.gm.p2_script.currentPopulation += 10;
                                p1_result = p1_situation.cd_Result;
                                p2_result = p2_situation.cd_Result;
                            }
                        }
                        else if (p1_option.Value.dilemma == Option.Dilemma.Defect){
                            if (p2_option.Value.dilemma == Option.Dilemma.Cooperative){
                                GameManager.gm.p1_script.currentPopulation += 10;
                                GameManager.gm.p2_script.currentPopulation -= 5;
                                p1_result = p1_situation.dc_Result;
                                p2_result = p2_situation.dc_Result;
                            }
                            else if (p2_option.Value.dilemma == Option.Dilemma.Defect){
                                GameManager.gm.p1_script.currentPopulation -= 10;
                                GameManager.gm.p2_script.currentPopulation -= 10;
                                p1_result = p1_situation.dd_Result;
                                p2_result = p2_situation.dd_Result;
                            }
                        }
                    }
                }
            }
        }            
    }
}