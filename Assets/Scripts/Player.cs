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
    public Situation mySituation;

    void Start()
    {        
        if (skill == Skill.DeepSleeper)
            this.energy = 100;
        else
            this.energy = 50;

        if (skill == Skill.MoneyMan)
            this.money = 100;
        else
            this.money = 50;        
    }

    private void Update() {
        if (this.name == "Player 1")
            mySituation = GameManager.gm.p1_currentScenario.p1_situation;
        else if (this.name == "Player 2")
            mySituation = GameManager.gm.p2_currentScenario.p2_situation;
    }
    
    public void AddChosenOption(Option option){
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
    }

    public string GetSituationResult(){
        return mySituation.GetResult();
    }
}