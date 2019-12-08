using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMan : MonoBehaviour
{

    private float ladderCount;
    public List<CheckColLadder> ladderList;

    private PlaceLadder placingLadder;

    // Start is called before the first frame update
    void Start()
    {
        placingLadder = GameObject.Find ("Player").GetComponent<PlaceLadder> ();

        ladderList = new List<CheckColLadder> ();
        foreach (GameObject ladObj in GameObject.FindGameObjectsWithTag ("PickUpLadder")) {
            ladderList.Add (ladObj.GetComponent<CheckColLadder>());
        }
    }

    private void Update () {
        PickUpLadder ();
        PlacingLadder ();
    }

    private void PickUpLadder () {
        for (int i = 0; i < ladderList.Count; i++) {
            if (ladderList[i].pickUpLadder == true) {
                ladderCount++;
                ladderList[i].pickUpLadder = false;
                Destroy (ladderList[i].gameObject);
            }
            if (i == ladderList.Count) {
                i = 0;
            }
        }
    }

    private void PlacingLadder () {
        if (ladderCount == 0) {
            Debug.Log (placingLadder.noLadders);
            placingLadder.noLadders = true;
        } else{
            placingLadder.noLadders = false;
            if (placingLadder.ladderPlaced) {
                ladderCount--;
            }
        }
    }
}
