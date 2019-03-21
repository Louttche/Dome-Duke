using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{    
    //public int energy, money;
    public float currentPopulation, initialPopulation;
    public Skill skill;
    public Dictionary<Scenario, Option> chosenOptions;

    void Start()
    {
        /*if (skill == Skill.DeepSleeper)
            this.energy = 100;
        else
            this.energy = 50;

        if (skill == Skill.MoneyMan)
            this.money = 100;
        else
            this.money = 50; */
        this.initialPopulation = 100;
        this.currentPopulation = initialPopulation;
        this.chosenOptions = new Dictionary<Scenario, Option>();
    }

    /*public void IncMoney(int amount)
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
        if (skill == Skill.DeepSleeper)
            this.energy += (amount + 10);
        else
            this.energy += amount;
    }
 
    public void DecEnergy(int amount)
    {
        if (skill == Skill.DeepSleeper)
            this.energy -= (amount - 10);
        else
            this.energy -= amount;
    } */
}