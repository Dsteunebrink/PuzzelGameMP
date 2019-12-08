using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesManager : MonoBehaviour
{

    [SerializeField] private List<Material> gateAndHoleMat;
    [SerializeField] private List<GameObject> gatesWithHoles;

    private bool colorChanged;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject GateAndHoleobj in GameObject.FindGameObjectsWithTag ("GatesWithHole")) {
            //Debug.Log (GateAndHoleobj);
            gatesWithHoles.Add (GateAndHoleobj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (colorChanged == false) {
            ChangeColor ();
        }
    }

    private void ChangeColor () {
        if (gatesWithHoles.Count == 1) {
            colorChanged = true;
            return;
        } else if (gatesWithHoles.Count > 4) {
            Destroy (gatesWithHoles[5].gameObject);
            gatesWithHoles.Remove (gatesWithHoles[5]);
            Debug.Log ("You can only have 5 gates with holes.");
            for (int i = 0; i < gatesWithHoles.Count; i++) {
                gatesWithHoles[i].transform.Find ("Outline").GetComponent<Renderer> ().material = gateAndHoleMat[i];
                gatesWithHoles[i].transform.Find ("Gate").GetComponent<Renderer> ().material = gateAndHoleMat[i];
            }
            colorChanged = true;
        } else {
            for (int i = 0; i < gatesWithHoles.Count; i++) {
                gatesWithHoles[i].transform.Find ("Outline").GetComponent<Renderer> ().material = gateAndHoleMat[i];
                gatesWithHoles[i].transform.Find ("Gate").GetComponent<Renderer> ().material = gateAndHoleMat[i];
            }
            colorChanged = true;
        }
    }
}
