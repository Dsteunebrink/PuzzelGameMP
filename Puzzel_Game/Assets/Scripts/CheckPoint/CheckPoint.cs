using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    private RigidbodyFirstPersonController player;

    private GameObject failedCanvas;

    private CheckPointColCheck checkPointBegin;
    private CheckPointColEnd checkPointEnd;

    [SerializeField] private float timer;
    [SerializeField] private float end;

    private Text timerText;
    private GameObject timerCanvas;

    private bool checkPointFail;
    private bool setTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find ("Player").GetComponent<RigidbodyFirstPersonController> ();

        failedCanvas = this.transform.Find ("CheckPointCanvas").gameObject;
        failedCanvas.SetActive (false);

        checkPointBegin = this.transform.Find ("CheckPointBegin").GetComponent<CheckPointColCheck> ();
        checkPointEnd = this.transform.Find ("CheckPointEnd").GetComponent<CheckPointColEnd> ();

        timer = 10f;
        end = 0f;

        timerText = GameObject.Find ("TimerText").GetComponent<Text> ();
        timerCanvas = this.transform.Find ("TimerCanvas").gameObject;

        timerCanvas.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        if (checkPointBegin.checkPointStart) {
            StartTimer ();
        }else if (checkPointEnd.checkPointSucces == false) {
            if (checkPointFail) {
                CheckPointFail ();
            }
        }
        if (checkPointEnd.checkPointSucces == true) {
            checkPointBegin.checkPointStart = false;
            CheckPoinSucceeded ();
            if (setTimer == false) {
                timer = 4f;
                setTimer = true;
            }
        }
    }

    private void StartTimer () {
        timerCanvas.SetActive (true);
        timer -= Time.deltaTime;
        timerText.text = "time Left:" + timer.ToString ("f0");
        if (timer <= end) {
            checkPointFail = true;
            checkPointBegin.checkPointStart = false;
        }
    }

    private void CheckPointFail () {
        Time.timeScale = 0;
        AudioListener.volume = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.stopCamera = true;
        failedCanvas.SetActive (true);
    }

    private void CheckPoinSucceeded () {
        timerText.text = "You were on time.";
        timer -= Time.deltaTime;
        Debug.Log (timer);
        if (timer <= end) {
            Destroy (this.gameObject);
        }
    }
}
