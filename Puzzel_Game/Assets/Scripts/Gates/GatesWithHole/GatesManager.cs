using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesManager : MonoBehaviour
{

    public List<Material> GateAndHoleMat;
    private List<GameObject> gatesWithHoles;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject GateAndHoleobj in GameObject.FindGameObjectsWithTag ("GatesWithHole")) {

            gatesWithHoles.Add (GateAndHoleobj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeColor () {
        if (gatesWithHoles.Count == 1) {
            return;
        } else if (gatesWithHoles.Count > 5) {

        }
    }
}
