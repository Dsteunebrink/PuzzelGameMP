using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCubeBorder : MonoBehaviour
{

    public GameObject beginPos;
    public GameObject endPos;

    public bool moveX;
    public bool moveZ;

    // Start is called before the first frame update
    void Start()
    {
        beginPos = this.transform.Find ("BeginPos").gameObject;
        endPos = this.transform.Find ("EndPos").gameObject;

        Destroy (beginPos.GetComponent<MeshRenderer> ());
        Destroy (endPos.GetComponent<MeshRenderer> ());

        beginPos.transform.SetParent (null);
        endPos.transform.SetParent (null);
    }
}
