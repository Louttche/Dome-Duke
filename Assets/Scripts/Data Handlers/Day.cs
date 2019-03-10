using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Day
{
    public List<Scenario> scenarios;
    [HideInInspector]
    public string[] p1_scenarioResults, p2_scenarioResults;

    /*public string[] GetP1ScenarioResults(){
        if (scenarios != null){
            p1_scenarioResults = new string[this.scenarios.Count];
            p2_scenarioResults = new string[this.scenarios.Count];

            foreach (Scenario s in scenarios)
            {
                for (int i = 0; i < this.scenarios.Count; i++){
                    p1_scenarioResults[i] = s.p1_situation.GetResult();
                    p2_scenarioResults[i] = s.p2_situation.GetResult();
                }
            }
        }
    } */
}