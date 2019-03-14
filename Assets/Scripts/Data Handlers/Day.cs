using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Day
{
    public List<Scenario> scenarios;
    [HideInInspector]
    public string[] p1_scenarioResults, p2_scenarioResults;
}