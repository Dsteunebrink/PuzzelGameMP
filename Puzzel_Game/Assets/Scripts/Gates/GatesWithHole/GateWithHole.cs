﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateWithHole : MonoBehaviour
{
    private GameObject gate;
    private GameObject outlining;

    private Vector3 endMark;
    [SerializeField] private float speed = 0.5F;
    private float lerpValue = 0.0f;

    private bool openGate;

    // Start is called before the first frame update
    void Start()
    {
        gate = this.transform.Find ("Gate").gameObject;
        endMark = gate.transform.position - new Vector3 (0, 6, 0);

        outlining = this.transform.Find ("Outline").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        OpenGate ();
    }

    private void OpenGate () {
        if (openGate == true) {
            if (gate.transform.position != endMark) {
                lerpValue += Time.deltaTime * speed;
                gate.transform.position = Vector3.Lerp (gate.transform.position, endMark, lerpValue);
            } else {
                Destroy (gate);
                openGate = false;
            }
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("PushCube")) {
            openGate = true;
        }
    }
}
