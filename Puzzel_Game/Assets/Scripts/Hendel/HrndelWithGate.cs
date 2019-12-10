using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HrndelWithGate : MonoBehaviour
{
    private bool inside;
    private bool GateOpened;

    private GameObject gate;
    private GameObject hendel;

    private Vector3 closed;
    private Vector3 open;
    [SerializeField] private float speed = 0.5F;
    private float lerpValue = 0.0f;
    private float resetLerpValue;

    private bool openGate;

    // Start is called before the first frame update
    void Start()
    {
        gate = this.transform.Find ("Gate").gameObject;
        open = gate.transform.position - new Vector3 (0, 6, 0);
        closed = gate.transform.position;

        resetLerpValue = 0.0f;

        hendel = this.transform.Find ("Hendel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (inside == true){
            OpenGate ();
        } 
    }

    private void OpenGate () {
        if (GateOpened == false) {
            lerpValue += Time.deltaTime * speed;
            gate.transform.position = Vector3.Lerp (gate.transform.position, open, lerpValue);
            if (gate.transform.position == open) {
                GateOpened = true;
                inside = false;
                lerpValue = resetLerpValue;
            }
        } else {
            lerpValue += Time.deltaTime * speed;
            gate.transform.position = Vector3.Lerp (gate.transform.position, closed, lerpValue);
            if (gate.transform.position == closed) {
                GateOpened = false;
                inside = false;
                lerpValue = resetLerpValue;
            }
        }
    }

    private void OnTriggerStay (Collider other) {
        if (other.CompareTag("Player") && Input.GetKeyDown (KeyCode.E)) {
            inside = true;
        }
    }

    private void OnTriggerExit (Collider other) {
        inside = false;
    }
}
