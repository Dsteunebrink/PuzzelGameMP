using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    private CheckTeleporterCol teleporter1;
    private CheckTeleporterCol teleporter2;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find ("Player");

        teleporter1 = this.transform.Find ("Teleport1").GetComponent<CheckTeleporterCol> ();
        teleporter2 = this.transform.Find ("Teleport2").GetComponent<CheckTeleporterCol> ();
    }

    private void Update () {
        teleportPlayer ();
    }

    private void teleportPlayer () {
        if (teleporter1.teleport == true) {
            player.transform.position = teleporter2.gameObject.transform.position;
        } else if (teleporter2.teleport == true) {
            player.transform.position = teleporter1.gameObject.transform.position;
        }
    }

    /*private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            if (teleporter1.teleport == true) {
                Debug.Log ("1");
                player.transform.position = teleporter1.gameObject.transform.position;
            } else if (teleporter2.teleport == true) {
                Debug.Log ("2");
                player.transform.position = teleporter2.gameObject.transform.position;
            }
        }
    }*/
}
