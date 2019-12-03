using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private CharacterController Player;
    private Vector3 launchVel;

    private GameObject Cross;

    [SerializeField] private int speed;
    private bool JumpDone;

    // Start is called before the first frame update
    void Start()
    {
        Cross = this.transform.Find ("Cross").gameObject;
        Destroy (Cross);
        Player = GameObject.Find ("Player").GetComponent<CharacterController> ();
        launchVel = new Vector3 (0, 10,-10);
        speed = 500;
    }

    private void Update () {
        if (JumpDone == false) {
            CheckVelocity ();
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag ("Player")) {
            Player.enabled = false;
            other.gameObject.GetComponent<Rigidbody> ().velocity = launchVel;
            other.gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * speed);
        }
    }

    private void CheckVelocity () {
        if (Player.gameObject.GetComponent<Rigidbody>().velocity == new Vector3(0,0,0)) {
            Player.enabled = true;
        }
    }
}
