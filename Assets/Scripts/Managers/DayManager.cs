using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DayManager : MonoBehaviour
{
    //protected FileInfo theSourceFile = null;
    //protected StreamReader reader = null;
    public DayList dayList;

    [HideInInspector]
    public static DayManager dm;
    public int currentDay = 1;
    private float dayTimer;
    private float dayTimerInitial  = 10;

    void Awake(){
        dm = this;
    }

    private void Start() {
        //LoadScenariosForDay(1);
        dayTimer = dayTimerInitial;
    }

    public bool endofDay()
    {      
        if (dayTimer >= 0.0f){
            dayTimer -= Time.deltaTime;
            return false;
        }
        else{
            dayTimer = dayTimerInitial;
            return true;
        }
    }

    /*protected void LoadScenariosForDay(int dayNumber)
    {
        try
        {
            theSourceFile = new FileInfo("Assets/Resources/Day" + dayNumber + ".txt");
            reader = theSourceFile.OpenText();

            
            string AllText = reader.ReadToEnd();
            string[] scenariosText= AllText.Split(';');

            foreach (string sText in scenariosText)
            {
                string[] lines = sText.Split('\n');
                
                Scenario temp = new Scenario(null, null);

                temp.p1_situation.question = lines[1];
                temp.p1_situation.cooperativeOption.text = lines[2];
                temp.p1_situation.cooperativeOption.dilemma = Option.Dilemma.Cooperative;
                temp.p1_situation.defectOption.text = lines[3];
                temp.p1_situation.defectOption.dilemma = Option.Dilemma.Defect;
                temp.p2_situation.question = lines[4];
                temp.p2_situation.cooperativeOption.text = lines[5];
                temp.p2_situation.cooperativeOption.dilemma = Option.Dilemma.Cooperative;
                temp.p2_situation.defectOption.text = lines[6];
                temp.p2_situation.defectOption.dilemma = Option.Dilemma.Defect;

                //days[dayNumber-1].scenarios.Add(temp);
            }
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e);
        }
    } */
}