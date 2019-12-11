using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    private GameObject creditsPanel;
    private GameObject optionsPanel;
    private GameObject mainPanel;
    private GameObject levelPanel;

    private bool creditsActive;
    private bool optionsActive;
    private bool levelSelectActive;

    // Start is called before the first frame update
    void Start()
    {
        creditsPanel = GameObject.Find ("CreditsPanel");
        optionsPanel = GameObject.Find ("OptionsPanel");
        mainPanel = GameObject.Find ("MainPanel");
        levelPanel = GameObject.Find ("LevelPanel");

        creditsPanel.SetActive (false);
        //optionsPanel.SetActive (false);
        levelPanel.SetActive (false);
    }

    public void LevelSelect () {
        if (levelSelectActive == false) {
            levelSelectActive = true;
            levelPanel.SetActive (true);
            mainPanel.SetActive (false);
        } else if (levelSelectActive == true) {
            levelSelectActive = false;
            levelPanel.SetActive (false);
            mainPanel.SetActive (true);
        }
    }

    public void Options () {
        if (optionsActive == false) {
            optionsActive = true;
            optionsPanel.SetActive (true);
            mainPanel.SetActive (false);
        } else if (optionsActive == true) {
            optionsActive = false;
            optionsPanel.SetActive (false);
            mainPanel.SetActive (true);
        }
    }

    public void Credits () {
        if (creditsActive == false) {
            creditsActive = true;
            creditsPanel.SetActive (true);
            mainPanel.SetActive (false);
        } else if (creditsActive == true) {
            creditsActive = false;
            creditsPanel.SetActive (false);
            mainPanel.SetActive (true);
        }
    }

    public void Quit () {
        Application.Quit ();
    }
}
