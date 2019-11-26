using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCubeBorder : MonoBehaviour
{

    private GameObject beginPos;
    private GameObject endPos;

    public bool moveX;
    public bool moveZ;

    // Start is called before the first frame update
    void Start()
    {
        beginPos = this.transform.Find ("BeginPos").gameObject;
        endPos = this.transform.Find ("EndPos").gameObject;
    }
}
