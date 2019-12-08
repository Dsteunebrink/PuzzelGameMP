using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColLadder : MonoBehaviour
{

    public bool pickUpLadder;

    private void OnTriggerStay (Collider other) {
        if (other.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.E)) {
                pickUpLadder = true;
            }
        }
    }
}
