﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbLadder : MonoBehaviour
{
    private Transform chController;
    private bool insideClimbLadder;
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
        if (insideClimbLadder == true && Input.GetKey ("w")) {
            chController.transform.position += Vector3.up / heightFactor;
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Ladder") {
            chController.GetComponent<CharacterController> ().enabled = false;
            insideClimbLadder = true;
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == "Ladder") {
            chController.GetComponent<CharacterController> ().enabled = true;
            insideClimbLadder = false;
        }
    }
}
