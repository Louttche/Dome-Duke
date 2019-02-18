using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    public string ScenarioName;
    public string text;
    public bool choice;
    private int countdown;

    public IEnumerator PlayScenario()
    {
        Debug.Log("Playing Scenario");
        yield return new WaitForSeconds(5);
    }

}
