using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverResults : MonoBehaviour
{
    public Text p1_nrOfEvil, p2_nrOfEvil, p1_nrOfGood, p2_nrOfGood; 

    public GameObject winPanel;

    private void Awake() {
        winPanel.gameObject.SetActive(false);
    }
    private void Start() {
        p1_nrOfEvil.text = GameManager.gm.p1_nrOfEvil.ToString();
        p2_nrOfEvil.text = GameManager.gm.p2_nrOfEvil.ToString();
        p1_nrOfGood.text = GameManager.gm.p1_nrOfGood.ToString();
        p2_nrOfGood.text = GameManager.gm.p2_nrOfGood.ToString();

        InitializeWinPanel();
        winPanel.gameObject.SetActive(true);
    }

    private void InitializeWinPanel(){
        Text title = winPanel.transform.Find("Title").GetComponent<Text>();
        Text txt = winPanel.transform.Find("Text").GetComponent<Text>();

        switch (GameManager.gm.end)
        {
            case GameManager.End.DoubleFailure:
                title.text = "Double Failure!";
                txt.text = "The amount of conflict between you put the Kingdom on a great risk! As the King tries to hold back tears of disappointment, he removes all royalty status from both of you...";
                break;
            
            case GameManager.End.DoubleWin:
                title.text = "Double Win!";
                txt.text = "The Kingdom has never seen better days and the people are proud to be our citizens! Only one issue... The King is indecisive about who should be crowned so neither of you do (yet)";
                break;

            case GameManager.End.p1Win:
                title.text = "Classic Win";
                txt.text = "The Kingdom is still barely holding itself together thanks to [Player 1 Name]! The King is proud of you and crowns you as the next King!!";
                break;

            case GameManager.End.p2Win:
                title.text = "Classic Win";
                txt.text = "The Kingdom is still barely holding itself together thanks to [Player 2 Name]! The King is proud of you and crowns you as the next King!!";
                break;

            default:
                title.text = "N/A";
                txt.text = "N/A";
                break;
        }
    }

    public void ClosePanel(){
        winPanel.transform.gameObject.SetActive(false);
    }

    public void StartNewGame(){
        SceneManager.LoadScene("MainScene");
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene("MenuScene");
    }
}
