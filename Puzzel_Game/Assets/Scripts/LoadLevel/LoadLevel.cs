using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadLevel : MonoBehaviour
{

    private GetLevel getLevel;
    private string levelPath;

    private string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        getLevel = GameObject.Find ("GetLevel").GetComponent<GetLevel> ();
        levelPath = getLevel.levelName;
        Destroy (getLevel.gameObject);

        levelToLoad = File.ReadAllText (levelPath);

        Debug.Log (levelToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
