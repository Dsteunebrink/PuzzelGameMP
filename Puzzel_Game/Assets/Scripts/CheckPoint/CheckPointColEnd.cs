using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointColEnd : MonoBehaviour
{
    public bool checkPointSucces;
    public bool checkPointHit;

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("Player") && checkPointHit == false) {
            checkPointSucces = true;
            checkPointHit = true;
        }
    }
}
