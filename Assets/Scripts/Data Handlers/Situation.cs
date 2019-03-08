using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Situation
{
    public string question;
    public List<Option> options;

    public string cc_Result, cd_Result, dc_Result, dd_Result;

    public string GetResult(){
        foreach (Option p1_option in GameManager.gm.p1_script.chosenOptions)
        {
            foreach (Option p2_option in GameManager.gm.p2_script.chosenOptions)
            {
                if (p1_option.dilemma == Option.Dilemma.Cooperative){
                    if (p2_option.dilemma == Option.Dilemma.Cooperative){
                        GameManager.gm.p1_script.score += 5;
                        //Show on UI
                        GameManager.gm.p2_script.score += 5;
                        //Show on UI
                        return cc_Result;
                    }
                    else if (p2_option.dilemma == Option.Dilemma.Defect){
                        GameManager.gm.p1_script.score -= 5;
                        GameManager.gm.p2_script.score += 10;
                        return cd_Result;
                    }
                }
                else if (p1_option.dilemma == Option.Dilemma.Defect){
                    if (p2_option.dilemma == Option.Dilemma.Cooperative){
                        GameManager.gm.p1_script.score += 10;
                        GameManager.gm.p2_script.score -= 5;
                        return dc_Result;
                    }
                    else if (p2_option.dilemma == Option.Dilemma.Defect){
                        GameManager.gm.p1_script.score -= 10;
                        GameManager.gm.p2_script.score -= 10;
                        return dd_Result;
                    }
                }
            }
        }
        return null;
    }
}
