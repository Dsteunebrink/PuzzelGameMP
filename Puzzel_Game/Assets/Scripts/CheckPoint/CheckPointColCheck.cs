using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointColCheck : MonoBehaviour
{

    public bool checkPointStart;
    public bool checkPointHit;

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player") && checkPointHit == false) {
            checkPointStart = true;
            checkPointHit = true;
        }
    }
}
