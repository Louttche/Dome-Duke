using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Situation
{
    [TextArea(3,5)]
    public string question;
    public List<Option> options;

    public string cc_Result, cd_Result, dc_Result, dd_Result;

    //Constructor to create a waiting state when a player is done before the other
    public Situation(string question){
        this.question = question;
        this.options = new List<Option>();
    }
}
