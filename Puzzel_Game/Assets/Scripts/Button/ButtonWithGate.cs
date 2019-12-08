using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWithGate : MonoBehaviour
{
    private GameObject gate;
    private GameObject Button;

    private Vector3 closed;
    private Vector3 open;
    [SerializeField] private float speed = 0.5F;
    private float lerpValue = 0.0f;
    private float resetLerpValue;

    private bool openGate;

    // Start is called before the first frame update
    void Start () {
        gate = this.transform.Find ("Gate").gameObject;
        open = gate.transform.position - new Vector3 (0, 6, 0);
        closed = gate.transform.position;

        resetLerpValue = 0.0f;

        Button = this.transform.Find ("Button").gameObject;
    }

    // Update is called once per frame
    void Update () {
        OpenGate ();
    }

    private void OpenGate () {
        if (openGate == true) {
            lerpValue += Time.deltaTime * speed;
            gate.transform.position = Vector3.Lerp (gate.transform.position, open, lerpValue);
            if (gate.transform.position == open) {
                lerpValue = resetLerpValue;
            }
        } else if(openGate == false){
            lerpValue += Time.deltaTime * speed;
            gate.transform.position = Vector3.Lerp (gate.transform.position, closed, lerpValue);
            if (gate.transform.position == closed) {
                lerpValue = resetLerpValue;
            }
        }
        
    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("PushCube") || other.CompareTag("Player")) {
            openGate = true;
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.CompareTag ("PushCube") || other.CompareTag ("Player")) {
            openGate = false;
        }
    }
}
