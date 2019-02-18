using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public int score = 0, happiness = 0, money = 0;
    private Skill skill;

    private void Start()
    {
        //get name
        //get skill
    }

    public void DisplayStats() //Show player's stats
    {
        //Use UI
    }

    public void IncMoney(int amount)
    {
        this.money += amount;
    }

    public void DecMoney(int amount)
    {
        this.money -= amount;
    }

    public void IncHappiness(int amount)
    {
        this.happiness += amount;
    }

    public void DecHappiness(int amount)
    {
        this.happiness -= amount;
    }
}
