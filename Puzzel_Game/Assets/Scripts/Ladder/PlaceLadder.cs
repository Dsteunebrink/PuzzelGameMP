using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceLadder : MonoBehaviour
{

    public GameObject climbLadder;
    private GameObject ghostClimbLadder;
    private GameObject block;

    private bool inside;
    public bool ladderPlaced;
    public bool noLadders;

    // Start is called before the first frame update
    void Start()
    {
        ghostClimbLadder = this.transform.Find ("GhostUseableLadder").gameObject;
        ghostClimbLadder.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inside == true && Input.GetKeyDown(KeyCode.E)) {
            PlacingLadder ();
        }
    }

    private void PlacingLadder () {
        Instantiate (climbLadder, ghostClimbLadder.transform.position, ghostClimbLadder.transform.rotation);
        ghostClimbLadder.SetActive (false);
        ladderPlaced = true;
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.layer == 11) {
            if (noLadders == false) {
                inside = true;
                ghostClimbLadder.SetActive (true);
                block = other.gameObject;
            }
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.gameObject.layer == 11) {
            inside = false;
            ghostClimbLadder.SetActive (false);
            block = null;
        }
    }
}
