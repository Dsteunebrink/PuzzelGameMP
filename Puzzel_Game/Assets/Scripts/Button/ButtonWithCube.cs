using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWithCube : MonoBehaviour
{
    private bool colorChanged;

    private Vector3 startPos;
    private Vector3 endPos;

    private Vector3 moveYVector;

    private bool goDown;
    private bool goUp;

    private GameObject Cube;

    private float speed;
    private float resetLerpValue;
    private float lerpValue;

    // Start is called before the first frame update
    void Start () {
        Cube = this.transform.Find ("Cube").gameObject;

        startPos = Cube.transform.position;

        resetLerpValue = 0.0f;
        lerpValue = 0.0f;
        speed = 0.5f;

        moveYVector = new Vector3 (0, 6, 0);
    }

    private void Update () {
        MoveCube ();
    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("Player") || other.CompareTag ("PushCube")) {
            goUp = true;
            goDown = false;
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Player") || other.CompareTag ("PushCube")) {
            goUp = false;
            goDown = true;
        }
    }

    private void MoveCube () {
        if (goUp) {
            lerpValue += Time.deltaTime * speed;
            endPos = startPos + moveYVector;
            Cube.transform.position = Vector3.Lerp (Cube.transform.position, endPos, lerpValue);
            if (Cube.transform.position == endPos) {
                lerpValue = resetLerpValue;
                goUp = false;
            }
        } else if (goDown) {
            lerpValue += Time.deltaTime * speed;
            endPos = startPos;
            Cube.transform.position = Vector3.Lerp (Cube.transform.position, endPos, lerpValue);
            if (Cube.transform.position == endPos) {
                lerpValue = resetLerpValue;
                goDown = false;
            }
        }
    }
}
