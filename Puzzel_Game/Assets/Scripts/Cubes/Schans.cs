using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schans : MonoBehaviour
{
    private void OnTriggerStay (Collider other) {
        if (other.CompareTag("Player")) {
            this.GetComponent<Rigidbody> ().isKinematic = true;
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Player")) {
            this.GetComponent<Rigidbody> ().isKinematic = false;
        }
    }
}
