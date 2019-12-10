using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    private bool gameDone;
    private GameObject endCanvas;

    private RigidbodyFirstPersonController player;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find ("Player").GetComponent<RigidbodyFirstPersonController> ();

        endCanvas = GameObject.Find ("EndCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit () {
        Resume ();
        SceneManager.LoadSceneAsync ("MainMenu");
    }

    public void NextLevel () {
        Resume ();
        SceneManager.LoadSceneAsync ("Level_1");
    }

    private void Resume () {
        AudioListener.volume = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.stopCamera = false;
        Time.timeScale = 1;
    }

    private void Pause () {
        Time.timeScale = 0;
        AudioListener.volume = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.stopCamera = true;
    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            endCanvas.SetActive (true);
            Pause ();
        }
    }
}
