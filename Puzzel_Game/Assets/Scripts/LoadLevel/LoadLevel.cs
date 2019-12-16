using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadLevel : MonoBehaviour
{
    //Variables for getting objects
    //-------------------------------------
    private GetLevel getLevel;
    private string levelPath;

    private GetObjects currentObject;

    private string levelToLoad;
    //-------------------------------------

    //Spawning cube variables
    //-------------------------------------
    private Vector3 beginPos;
    private Vector3 spawnPos;
    private Vector3 moveTile;
    private Vector3 moveRow;
    
    private bool gridDone;
    //-------------------------------------

    // Start is called before the first frame update
    void Start ()
    {
        //Variables for getting objects
        //-------------------------------------
        getLevel = GameObject.Find ("GetLevel").GetComponent<GetLevel> ();
        levelPath = getLevel.levelName;
        Destroy (getLevel.gameObject);

        levelToLoad = File.ReadAllText (levelPath);
        currentObject = JsonUtility.FromJson<GetObjects> (levelToLoad);

        //Spawning cube variables
        //-------------------------------------
        beginPos = this.transform.position;
        moveTile = new Vector3 (0, 0, 6);
        moveRow = new Vector3 (6, 0, 0);
        //-------------------------------------
    }

    [System.Serializable]
    public class GetObjects {

        [System.Serializable]
        public class GetObjectData {

            public int Nextline;

            public string id;
            public int value;
            public string link;
        }

        public GetObjectData[] objects;
    }

    private void Update () {
        if (gridDone == false) {
            GenerateGrid ();
        }
    }

    private void GenerateGrid () {
        for (int i = 0; i < currentObject.objects.Length; i++) {
            if (currentObject.objects[i].Nextline == -1) {
                this.transform.position = beginPos + moveRow;
                beginPos = this.transform.position;
            } else {
                this.transform.position += moveTile;
            }
            if (i == 28) {
                gridDone = true;
            }
        }
    }
}
