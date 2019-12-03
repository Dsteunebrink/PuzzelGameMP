using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuMan : MonoBehaviour
{
    private RigidbodyFirstPersonController player;

    private GameObject pauseMenuPanel;
    private GameObject optionMenuPanel;

    private bool optionPanelActive;
    private bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find ("Player").GetComponent<RigidbodyFirstPersonController> ();

        pauseMenuPanel = GameObject.Find ("PauseMenuPanel");
        optionMenuPanel = GameObject.Find ("OptionMenuPanel");

        pauseMenuPanel.SetActive (false);
        optionMenuPanel.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPause ();
    }

    private void CheckForPause () {
        if (Input.GetKeyDown ("escape")) {
            //If the game is not paused pause it.
            if (gamePaused == false) {
                Pause ();
            }
            //If the game is paused resume it.
            if (gamePaused == true) {
                Resume ();
            }
        }
    }

    //Set this function on the Resume button.
    public void Resume () {
        //If the game is paused resume it.
        AudioListener.volume = 1;
        pauseMenuPanel.SetActive (false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.stopCamera = false;
        Time.timeScale = 1;
    }

    public void Pause () {
        pauseMenuPanel.SetActive (true);
        Time.timeScale = 0;
        AudioListener.volume = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.stopCamera = true;
    }

    //Set this function on the Restart button.
    public void Restart () {
        //Reloading current scene.
        Resume ();
        Scene scene = SceneManager.GetActiveScene (); SceneManager.LoadScene (scene.name);
    }

    public void Options () {
        if (optionPanelActive == false) {
            optionMenuPanel.SetActive (true);
            pauseMenuPanel.SetActive (false);
            optionPanelActive = true;
        }else if (optionMenuPanel == true) {
            optionMenuPanel.SetActive (false);
            pauseMenuPanel.SetActive (true);
            optionPanelActive = false;
        }
    }

    public void Quit () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.stopCamera = false;
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync ("MainMenu");
    }
}

