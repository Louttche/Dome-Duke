using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject playerStatus, generalStatus, progressBar, scenarioBar, questionBoxes;
    public Text tutorialText;

    public static string playerStatusText = "This is information about your city's resources, so far you only got POPULATION to worry about.\n\nThe amount of POPULATION determines your victory!",
    generalStatusText = "Here you can check the current day and overall HEALTH of both of your cities' Kingdom. While EVIL actions are harmful to the Kingdom, GOOD actions improve its state.\n\nIf the Kingdom falls, then so will both of you!",
    progressBarText = "This bar shows how far ahead the other person is with their city's POPULATION.\n\n The Duke with the biggest POPULATION at the end of the game wins!",
    scenarioBarText = "The bar here displays how many scenarios you have completed and have yet left to complete.",
    questionBoxesText = "A random scenario will appear here, use the boxes to make your GOOD or EVIL choice.";

    private void Start() {
        if (Menu.m.ShowTutorial){
            playerStatus.SetActive(true);
            generalStatus.SetActive(false);
            progressBar.SetActive(false);
            questionBoxes.SetActive(false);
            tutorialText.GetComponentInChildren<Text>().text = playerStatusText;
        }
    }

    //Executes whenever the tutorial button is pressed
    public void ShowNextTutorial(){
        //Goes through each tutorial part in order
        if (playerStatus.activeSelf){
            playerStatus.SetActive(false);
            progressBar.SetActive(true);
            tutorialText.GetComponentInChildren<Text>().text = progressBarText;
        }
        else if (progressBar.activeSelf){
            progressBar.SetActive(false);
            scenarioBar.SetActive(true);
            tutorialText.GetComponentInChildren<Text>().text = scenarioBarText;
        }
        else if (scenarioBar.activeSelf){
            scenarioBar.SetActive(false);
            questionBoxes.SetActive(true);
            tutorialText.GetComponentInChildren<Text>().text = questionBoxesText;
        }
        else if (questionBoxes.activeSelf){
            questionBoxes.SetActive(false);
            generalStatus.SetActive(true);
            tutorialText.GetComponentInChildren<Text>().text = generalStatusText;
        }
        else if (generalStatus.activeSelf){
            generalStatus.SetActive(false);
            tutorialText.transform.parent.gameObject.SetActive(false);
            Menu.m.ShowTutorial = false;
        }
        
    }
}
