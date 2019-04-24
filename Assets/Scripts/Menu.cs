using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu m;
    public Button start, exit, howto;
    public GameObject howtoPanel;
    private Animator howtoAnimator;
    public GameObject showTutorialBox;

    [HideInInspector]
    public bool ShowTutorial;
    private void Awake() {
        m = this;
        showTutorialBox.SetActive(false);
    }
    private void Start() {
        howtoAnimator = howtoPanel.GetComponent<Animator>();
        howtoAnimator.SetBool("ShowPanel", false);
        start.onClick.AddListener(() => StartClicked());
        exit.onClick.AddListener(() => Application.Quit());
        howto.onClick.AddListener(() => howtoAnimator.SetBool("ShowPanel", true));
        howtoPanel.GetComponent<Button>().onClick.AddListener(() => howtoAnimator.SetBool("ShowPanel", false));
    }

    public void SetShowTutorial(bool show){
        ShowTutorial = show;
        StartGame();
    }
    
    public void StartClicked(){
        showTutorialBox.SetActive(true);
    }

    private void StartGame(){
        SceneManager.LoadScene("MainScene");
    }
}
