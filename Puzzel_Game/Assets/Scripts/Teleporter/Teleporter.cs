using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    private CheckTeleporterCol teleporter1;
    private CheckTeleporterCol teleporter2;

    private float timer;
    private bool waitForNextTP;
    private float tpAllowed;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find ("Player");

        tpAllowed = 2;

        teleporter1 = this.transform.Find ("Teleport1").GetComponent<CheckTeleporterCol> ();
        teleporter2 = this.transform.Find ("Teleport2").GetComponent<CheckTeleporterCol> ();
    }

    private void Update () {
        WaitForNextTP ();
        teleportPlayer ();
    }

    private void teleportPlayer () {
        if (waitForNextTP == false) {
            if (teleporter1.teleport == true) {
                player.transform.position = teleporter2.gameObject.transform.position;
                waitForNextTP = true;
                teleporter1.teleport = false;
            } else if (teleporter2.teleport == true) {
                player.transform.position = teleporter1.gameObject.transform.position;
                waitForNextTP = true;
                teleporter2.teleport = false;
            }
        }
    }

    private void WaitForNextTP () {
        if (waitForNextTP == true) {
            timer += Time.deltaTime;

            if (timer >= tpAllowed) {
                teleporter1.teleport = false;
                teleporter2.teleport = false;
                waitForNextTP = false;
            }
        }
    }
}
