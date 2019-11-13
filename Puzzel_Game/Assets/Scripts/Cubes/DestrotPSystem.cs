using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestrotPSystem : MonoBehaviour
{

    private ParticleSystem pSystem;
    private float maxDuration;

    // Start is called before the first frame update
    void Start()
    {
        pSystem = GetComponent<ParticleSystem> ();
        maxDuration = 0.00f;
    }

    // Update is called once per frame
    void Update()
    {
        maxDuration += Time.deltaTime;
        if (pSystem.main.duration == maxDuration) {
            Destroy (this.gameObject);
        }
    }
}
