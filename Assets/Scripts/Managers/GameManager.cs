using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*  PRISONER'S DILEMMA
    (p1,p2) | c = cooperative | d = defect

     - cc = (+5,+5)
     - cd = (-5,+10)
     - dc = (+10,-5)
     - dd = (-10,-10)
     */

public class GameManager : MonoBehaviour
{
    public GameObject player1, player2;
    [HideInInspector]
    public Player p1_script, p2_script;

    [HideInInspector]
    public static GameManager gm;
    public Scenario currentScenario;

    private void Awake()
    {
        gm = this;

        //Get player script component to easily change their values
        p1_script = player1.GetComponent<Player>();
        p2_script = player2.GetComponent<Player>();
    }

    private void Start() {
        currentScenario = DayManager.dm.dayList.days[0].scenarios[0];
    }

    private void Update() {
        if (DayManager.dm.currentDay <= DayManager.dm.dayList.days.Count){
            if (!DayManager.dm.endofDay()){
                
            }
            else {
                //GameManager.gm.SetScore();
                //DisplayScore();
                //currentDay++;
                //LoadScenariosForDay(currentDay);
            }
        }
    }
}