using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public enum Dilemma
    {
        Cooperative,
        Defect
    }
    [HideInInspector]
    public string text;
    //public int Mcost, Rcost;
    [HideInInspector]
    public Dilemma dilemma;
}