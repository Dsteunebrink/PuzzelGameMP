using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public bool moveX;
    public bool moveZ;
    private bool moveDirection;
    private bool directionSet;

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

        Destroy (this.transform.Find ("MoveStartPos").gameObject);
        Destroy (this.transform.Find ("MoveEndPos").gameObject);

        movePlatformValue = 0.0f;

        moveXVector = new Vector3 (3,0,0);
        moveZVector = new Vector3 (0,0,3);

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
                if (this.transform.position == endPos) {
                    Debug.Log ("endpos");
                    moveDirection = true;
                } else if (this.transform.position == startPos) {
                    Debug.Log ("startpos");
                    moveDirection = false;
                }
                if (moveDirection == true) {
                    Debug.Log ("true");
                    this.transform.position += moveXVector;
                }else if (moveDirection == false) {
                    Debug.Log ("false");
                    this.transform.position -= moveXVector;
                }
                speed = beginSpeed;
            }
        }else if (moveZ == true) {
            speed -= Time.deltaTime;
            if (speed >= movePlatformValue) {
                SetDirection ();
                if (this.transform.position == endPos || this.transform.position == startPos) {
                    this.transform.position += moveZVector;
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
}
