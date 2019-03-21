using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button start, exit, howto;
    public GameObject howtoPanel;
    private Animator howtoAnimator;
    private void Start() {
        howtoAnimator = howtoPanel.GetComponent<Animator>();
        howtoAnimator.SetBool("ShowPanel", false);
        start.onClick.AddListener(() => SwitchScenes());
        exit.onClick.AddListener(() => Application.Quit());
        howto.onClick.AddListener(() => howtoAnimator.SetBool("ShowPanel", true));
        howtoPanel.GetComponent<Button>().onClick.AddListener(() => howtoAnimator.SetBool("ShowPanel", false));
    }

    private void SwitchScenes(){
        SceneManager.LoadScene("MainScene");
    }
}
