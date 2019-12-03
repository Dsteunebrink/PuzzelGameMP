using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbLadder : MonoBehaviour
{
    private Transform chController;
    private bool inside;
    private float heightFactor;

    // Start is called before the first frame update
    void Start()
    {
        chController = this.transform;
        heightFactor = 3.6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (inside == true && Input.GetKey ("w")) {
            chController.transform.position += Vector3.up / heightFactor;
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Ladder") {
            Debug.Log ("in");
            chController.GetComponent<CharacterController> ().enabled = false;
            inside = true;
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == "Ladder") {
            Debug.Log ("out");
            chController.GetComponent<CharacterController> ().enabled = true;
            inside = false;
        }
    }
}
