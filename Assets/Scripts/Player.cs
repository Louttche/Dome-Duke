using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{    
    public int energy, money;
    public float score;
    public Skill skill;
    public List<Option> chosenOptions;

    public void AddOption(Option option){
        chosenOptions.Add(option);
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

    public void IncEnergy(int amount)
    {
        if (skill == Skill.SweetTalker)
            this.energy += (amount + 10);
        else
            this.energy += amount;
    }
 
    public void DecEnergy(int amount)
    {
        if (skill == Skill.SweetTalker)
            this.energy -= (amount - 10);
        else
            this.energy -= amount;
    }
}
