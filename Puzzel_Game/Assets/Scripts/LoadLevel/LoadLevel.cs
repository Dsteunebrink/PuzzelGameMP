using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{

    [SerializeField] private List<GameObject> linkObjects;
    [SerializeField] private List<string> linkIDs;

    private bool Button;
    private bool Gate;
    private bool Cube;
    private bool Cross;
    private bool CheckPointBegin;
    private bool CheckPointEnd;
    private bool Hole;
    private bool MoveEndPos;
    private bool MoveBeginPos;

    private int linkID1;
    private int linkID2;

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
    private Vector3 moveUp;

    [SerializeField] private List<GameObject> spawnableObjects;
    
    private bool gridDone;
    //-------------------------------------

    // Start is called before the first frame update
    void Start ()
    {
        //GameObject linkObjects = new GameObject();

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
        moveUp = new Vector3 (0, 6, 0);
        //-------------------------------------
    }

    private void Update () {
        if (gridDone == false) {
            GenerateGrid ();
        } else {
            checkLinks ();
        }
    }

    [System.Serializable]
    public class GetObjects {

        [System.Serializable]
        public class GetObjectData {

            public int Nextline;

            public int id;
            public int value;
            public string link;
        }

        public GetObjectData[] objects;
    }

    private void GenerateGrid () {
        for (int i = 0; i < currentObject.objects.Length; i++) {
            if (currentObject.objects[i].Nextline == -1) {
                this.transform.position = beginPos + moveRow;
                beginPos = this.transform.position;
            } else {
                IdToObject (i);
                this.transform.position += moveTile;
            }
            if (i == 28) {
                gridDone = true;
            }
        }
    }

    private void IdToObject (int gridNumber) {
        if (currentObject.objects[gridNumber].id >= 100) {
            currentObject.objects[gridNumber].id -= 100;
        }

        if (currentObject.objects[gridNumber].link != "") {
            linkIDs.Add (currentObject.objects[gridNumber].link);
            linkObjects.Add (spawnableObjects[currentObject.objects[gridNumber].id]);
        }

        if (currentObject.objects[gridNumber].id == 0) {
            return;
        } else if (currentObject.objects[gridNumber].id == 1) {
            Instantiate (spawnableObjects[currentObject.objects[gridNumber].id], this.transform.position, Quaternion.identity);
        } else {
            Instantiate (spawnableObjects[1], this.transform.position, Quaternion.identity);
            Instantiate (spawnableObjects[currentObject.objects[gridNumber].id], this.transform.position + moveUp, Quaternion.identity);
        }
    }

    private void checkLinks () {
        for (int i = 0; i < linkIDs.Count; i++) {
            for (int e = 0; i < linkIDs.Count; e++) {
                if (e != i) {
                    if (linkIDs[e] == linkIDs[i]) {
                        Debug.Log (linkIDs[e] + linkIDs[i]);
                        linkIDs.Remove (linkIDs[e]);
                        linkIDs.Remove (linkIDs[i]);
                        spawnLinks (linkObjects[e], linkObjects[i], e, i);
                        return;
                    }
                }
            }
        }
    }

    private void spawnLinks (GameObject linkObject1, GameObject linkObject2, int linkID1, int linkID2) {
        linkObjects.Remove (linkObjects[linkID1]);
        linkObjects.Remove (linkObjects[linkID2]);

        if (linkObject1.name == "Button") {
            Button = true;
            Debug.Log ("Button");
        }else if (linkObject1.name == "Gate") {
            Gate = true;
            Debug.Log ("Gate");
        } else if (linkObject1.name == "Cube") {
            Cube = true;
            Debug.Log ("Cube");
        } else if (linkObject1.name == "Cross") {
            Cross = true;
            Debug.Log ("Cross");
        } else if (linkObject1.name == "CheckPointBegin") {
            CheckPointBegin = true;
            Debug.Log ("CheckPointBegin");
        } else if (linkObject1.name == "CheckPointEnd") {
            CheckPointEnd = true;
            Debug.Log ("CheckPointEnd");
        } else if (linkObject1.name == "Hole") {
            Hole = true;
            Debug.Log ("Hole");
        } else if (linkObject1.name == "MoveEndPos") {
            MoveEndPos = true;
            Debug.Log ("MoveEndPos");
        } else if (linkObject1.name == "MoveBeginPos") {
            MoveBeginPos = true;
            Debug.Log ("MoveBeginPos");
        }

        if (linkObject2.name == "Button") {
            Button = true;
            Debug.Log ("Button");
        } else if (linkObject2.name == "Gate") {
            Gate = true;
            Debug.Log ("Gate");
        } else if (linkObject2.name == "Cube") {
            Cube = true;
            Debug.Log ("Cube");
        } else if (linkObject2.name == "Cross") {
            Cross = true;
            Debug.Log ("Cross");
        } else if (linkObject2.name == "CheckPointBegin") {
            CheckPointBegin = true;
            Debug.Log ("CheckPointBegin");
        } else if (linkObject2.name == "CheckPointEnd") {
            CheckPointEnd = true;
            Debug.Log ("CheckPointEnd");
        } else if (linkObject2.name == "Hole") {
            Hole = true;
            Debug.Log ("Hole");
        } else if (linkObject2.name == "MoveEndPos") {
            MoveEndPos = true;
            Debug.Log ("MoveEndPos");
        } else if (linkObject2.name == "MoveBeginPos") {
            MoveBeginPos = true;
            Debug.Log ("MoveBeginPos");
        }

        if (Button && Gate) {
        }
    }
}
