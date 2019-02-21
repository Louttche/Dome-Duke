using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario
{
    //public string ScenarioName;
    public string text;
    public List<Choice> choices;
    private int countdown;

    public Scenario(/*string name, */string text)
    {

    }

    public IEnumerator PlayScenario()
    {
        Debug.Log("Playing Scenario");
        yield return new WaitForSeconds(5);
    }

}
