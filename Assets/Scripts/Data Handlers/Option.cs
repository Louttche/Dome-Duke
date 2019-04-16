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
        Cooperative = 0,
        Defect = 1
    }
    public string text;
    //public int Mcost, Rcost;
    public Dilemma dilemma;
}
