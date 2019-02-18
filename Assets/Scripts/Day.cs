using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int dayCounter = 1;
    public DayState dayState = DayState.Morning;

    public void GoToNextDay()
    {
        dayCounter++;
    }

    public void ChangeDayState()
    {
        switch (dayState)
        {
            case DayState.Morning:
                dayState = DayState.Afternoon;
                break;
            case DayState.Afternoon:
                dayState = DayState.Evening;
                break;
            case DayState.Evening:
                dayState = DayState.EndOfDay;
                break;
            case DayState.EndOfDay:
                dayState = DayState.Morning;
                break;
            default:
                break;
        }
    }
}
