using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTeleporterCol : MonoBehaviour
{

    public bool teleport;

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            teleport = true;
        }
    }
}
