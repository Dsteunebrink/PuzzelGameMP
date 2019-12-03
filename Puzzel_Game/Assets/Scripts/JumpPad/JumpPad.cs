using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private CharacterController Player;
    private Vector3 resetVel;
    private Quaternion direction;

    private GameObject Camera;

    public int speed;
    private bool JumpDone;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find ("MainCamera");
        direction = new Quaternion (0.0f, 1.0f, 0.1f, 0.0f);
        Player = GameObject.Find ("Player").GetComponent<CharacterController> ();
        resetVel = new Vector3 (0,0,-100);
        speed = 500;
    }

    private void Update () {
        if (JumpDone == false) {
            CheckVelocity ();
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag ("Player")) {
            Debug.Log ("launch");
            Player.enabled = false;
            //Camera.transform.rotation = direction;
            other.gameObject.GetComponent<Rigidbody> ().velocity = resetVel;
            other.gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * speed);
        }
    }

    private void CheckVelocity () {
        if (Player.gameObject.GetComponent<Rigidbody>().velocity == new Vector3(0,0,0)) {
            Player.enabled = true;
        }
    }
}
