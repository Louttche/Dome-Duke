using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayQuestion : MonoBehaviour
{
    public Text questiontxt;
    public string parentName;
    void Start()
    {
        questiontxt = this.GetComponentInChildren<Text>();
        parentName = this.transform.parent.name;
    }

    void Update()
    {
        if (this.transform.parent.name == "Player 1"){
            questiontxt.text = "player 1 question";
        }
        else{
            questiontxt.text = "player 2 question";
        }
    }
}
