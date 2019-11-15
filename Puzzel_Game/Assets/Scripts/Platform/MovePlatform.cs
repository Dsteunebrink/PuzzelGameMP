using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public bool moveX;
    public bool moveZ;
    private bool moveDirection;
    private bool directionSet;
    private bool firstIgnore;

    private Vector3 startPos;
    private Vector3 endPos;

    private Vector3 moveXVector;
    private Vector3 moveZVector;

    private float speed;
    public float beginSpeed;
    private float movePlatformValue;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.Find ("MoveStartPos").transform.position;
        endPos = this.transform.Find ("MoveEndPos").transform.position;

        Destroy (this.transform.Find ("MoveStartPos").GetComponent<MeshRenderer> ());
        Destroy (this.transform.Find ("MoveEndPos").GetComponent<MeshRenderer> ());

        this.transform.Find ("MoveStartPos").transform.SetParent(null);
        this.transform.Find ("MoveEndPos").transform.SetParent (null);

        movePlatformValue = 0.0f;

        moveXVector = new Vector3 (6,0,0);
        moveZVector = new Vector3 (0,0,6);

        speed = beginSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        MovingPlatform ();
    }

    private void MovingPlatform () {

        if (moveX == true) {
            speed -= Time.deltaTime;
            if (speed <= movePlatformValue) {
                SetDirection ();
                if (moveDirection == true) {
                    this.transform.position += moveXVector;
                }else if (moveDirection == false) {
                    this.transform.position -= moveXVector;
                }
                speed = beginSpeed;
            }
        }
        
        else if (moveZ == true) {
            speed -= Time.deltaTime;
            if (speed <= movePlatformValue) {
                SetDirection ();
                if (moveDirection == true) {
                    this.transform.position += moveZVector;
                } else if (moveDirection == false) {
                    this.transform.position -= moveZVector;
                }
                speed = beginSpeed;
            }
        }
    }

    private void SetDirection () {
        if (directionSet == false) {
            if (moveX == true) {
                if (startPos.x <= endPos.x) {
                    moveDirection = true;
                } else if (startPos.x >= endPos.x) {
                    moveDirection = false;
                }
            } else if (moveZ == true) {
                if (startPos.z <= endPos.z) {
                    moveDirection = true;
                } else if (startPos.z >= endPos.z) {
                    moveDirection = false;
                }
            }
            directionSet = true;
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("MoveStartPos")) {
            if (firstIgnore == true) {
                if (moveDirection == true) {
                    moveDirection = false;
                } else {
                    moveDirection = true;
                }
            } else {
                firstIgnore = true;
            }
        } else if (other.CompareTag ("MoveEndPos")) {
            if (moveDirection == true) {
                moveDirection = false;
            } else {
                moveDirection = true;
            }
        }
    }
}
