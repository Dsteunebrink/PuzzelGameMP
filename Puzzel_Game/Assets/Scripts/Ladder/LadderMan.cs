using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMan : MonoBehaviour
{

    private float ladderCount;
    public List<CheckColLadder> ladderList;

    // Start is called before the first frame update
    void Start()
    {
        ladderList = new List<CheckColLadder> ();
        foreach (GameObject ladObj in GameObject.FindGameObjectsWithTag ("Ladder")) {
            ladderList.Add (ladObj.GetComponent<CheckColLadder>());
        }
    }

    private void Update () {
        PickUpLadder ();
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
}
