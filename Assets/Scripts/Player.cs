using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public int reputation, money;
    public float score;
    public Skill skill;

    public void DisplayStats() //Show player's stats
    {
        //Use UI
    }

    public void IncMoney(int amount)
    {
        if (skill == Skill.MoneyMan)
            this.money += (amount + 10);
        else
            this.money += amount;
    }

    public void DecMoney(int amount)
    {
        if (skill == Skill.MoneyMan)
            this.money -= (amount - 5);
        else
            this.money -= amount;
    }

    public void IncReputation(int amount)
    {
        if (skill == Skill.SweetTalker)
            this.reputation += (amount + 10);
        else
            this.reputation += amount;
    }

    public void DecReputation(int amount)
    {
        if (skill == Skill.SweetTalker)
            this.reputation -= (amount - 10);
        else
            this.reputation -= amount;
    }
}
