using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private CharacterController player;
    private Vector3 launchVel;

    private GameObject cross;

    [SerializeField] private bool jumpX;
    [SerializeField] private bool jumpY;

    [SerializeField] private int speed;
    private bool jumpDone;
    private bool jumped;

    // Start is called before the first frame update
    void Start()
    {
        cross = this.transform.Find ("Cross").gameObject;
        Destroy (cross);
        player = GameObject.Find ("Player").GetComponent<CharacterController> ();

        if (jumpY) {
            launchVel = new Vector3 (0, 10, -10);
        } else if (jumpX) {
            launchVel = new Vector3 (-6.5f, 15, 0);
        }
        speed = 500;
    }

    private void Update () {
        if (jumped == true) {
            if (jumpDone == false) {
                CheckVelocity ();
            }
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag ("Player")) {
            player.enabled = false;
            other.gameObject.AddComponent<Rigidbody> ();
            other.gameObject.GetComponent<Rigidbody> ().velocity = launchVel;
            other.gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * speed);
            jumped = true;
        }
    }

    private void CheckVelocity () {
        if (jumpY) {
            if (player.gameObject.GetComponent<Rigidbody> ().velocity.magnitude < 4) {
                player.enabled = true;
                Destroy (player.GetComponent<Rigidbody> ());
                jumped = false;
            }
        } else if (jumpX) {
            if (player.gameObject.GetComponent<Rigidbody> ().velocity.magnitude < 6) {
                player.enabled = true;
                Destroy (player.GetComponent<Rigidbody> ());
                jumped = false;
            }
        }

    }
}
