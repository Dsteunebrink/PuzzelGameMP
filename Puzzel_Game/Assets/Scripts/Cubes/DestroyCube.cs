using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{

    public GameObject pSystem;

    private void OnTriggerStay (Collider other) {
        if (other.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.E)) {
                Instantiate (pSystem, this.transform.position, this.transform.rotation);
                Destroy (this.gameObject);
            }
        }
    }
}
