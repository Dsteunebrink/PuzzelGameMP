using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private List<GameObject> linkPrefabs;
    [SerializeField] private List<GameObject> linkObjects;
    [SerializeField] private List<string> linkIDs;

    private bool LinkingDone;

    private bool Button;
    private bool Gate;
    private bool Cube;
    private bool Cross;
    private bool CheckPointBegin;
    private bool CheckPointEnd;
    private bool Hole;
    private bool MoveEndPos;
    private bool MoveBeginPos;
    private bool BeginPos;
    private bool EndPos;
    private bool Teleport1;
    private bool Teleport2;

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
    private Quaternion gateRot;

    private Vector3 beginPos;
    private Vector3 spawnPos;
    private Vector3 moveTile;
    private Vector3 moveRow;
    private Vector3 moveUp;
    private Vector3 moveUpHalved;
    private Vector3 moveDown;
    private Vector3 moveDownHalved;

    private GameObject tempObject;

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
        gateRot = new Quaternion (0,0,90,0);

        beginPos = this.transform.position;
        moveTile = new Vector3 (0, 0, 6);
        moveRow = new Vector3 (6, 0, 0);
        moveUp = new Vector3 (0, 6, 0);
        moveUpHalved = new Vector3 (0, 2, 0);
        moveDown = new Vector3 (0, -6, 0);
        moveDownHalved = new Vector3 (0, -2, 0);
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
            public int StopGrid;

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
            } else if (currentObject.objects[i].StopGrid == -1) {
                Debug.Log ("end");
                gridDone = true;
            } else {
                IdToObject (i);
                this.transform.position += moveTile;
            }
        }
    }

    private void IdToObject (int gridNumber) {
        if (currentObject.objects[gridNumber].id >= 100) {
            currentObject.objects[gridNumber].id -= 100;
        }

        if (currentObject.objects[gridNumber].id == 101) {
            currentObject.objects[gridNumber].id -= 100;
            tempObject = Instantiate (spawnableObjects[currentObject.objects[gridNumber].id], this.transform.position, Quaternion.identity);
            tempObject = Instantiate (spawnableObjects[currentObject.objects[gridNumber].id], this.transform.position + moveUp, Quaternion.identity);
            return;
        }else if (currentObject.objects[gridNumber].id == 0) {
            tempObject = Instantiate (spawnableObjects[currentObject.objects[gridNumber].id], this.transform.position + moveDown, Quaternion.identity);
            return;
        } else if (currentObject.objects[gridNumber].id == 1) {
            tempObject = Instantiate (spawnableObjects[currentObject.objects[gridNumber].id], this.transform.position, Quaternion.identity);
            if (currentObject.objects[gridNumber].link != "") {
                linkIDs.Add (currentObject.objects[gridNumber].link);
                linkObjects.Add (tempObject);
            }
        } else {
            Instantiate (spawnableObjects[1], this.transform.position, Quaternion.identity);
            if (spawnableObjects[currentObject.objects[gridNumber].id].name == "Button") {
                tempObject = Instantiate (spawnableObjects[currentObject.objects[gridNumber].id], this.transform.position + moveDownHalved, Quaternion.identity);
            } else if (spawnableObjects[currentObject.objects[gridNumber].id].name == "Gate") {
                tempObject = Instantiate (spawnableObjects[currentObject.objects[gridNumber].id], this.transform.position + moveUp, spawnableObjects[currentObject.objects[gridNumber].id].transform.rotation);
            } else {
                tempObject = Instantiate (spawnableObjects[currentObject.objects[gridNumber].id], this.transform.position + moveUp, Quaternion.identity);
            }

            if (currentObject.objects[gridNumber].link != "") {
                linkIDs.Add (currentObject.objects[gridNumber].link);
                linkObjects.Add (tempObject);
            }
        }
    }

    private void checkLinks () {
        if (LinkingDone == false) {
            for (int i = 0; i < linkIDs.Count; i++) {
                for (int e = 0; i < linkIDs.Count; e++) {
                    if (e != i) {
                        if (linkIDs[e] == linkIDs[i]) {
                            spawnLinks (linkObjects[e], linkObjects[i], e, i);
                            return;
                        }
                    }
                }
            }
        }
    }

    private void spawnLinks (GameObject linkObject1, GameObject linkObject2, int linkID1, int linkID2) {
        if (linkObject1.name == "Button(Clone)") {
            Button = true;
            Debug.Log ("Button");
        }else if (linkObject1.name == "Gate(Clone)") {
            Gate = true;
            Debug.Log ("Gate");
        } else if (linkObject1.name == "StaticCube(Clone)") {
            Cube = true;
            Debug.Log ("Cube");
        } else if (linkObject1.name == "Cross(Clone)") {
            Cross = true;
            Debug.Log ("Cross");
        } else if (linkObject1.name == "CheckPointBegin(Clone)") {
            CheckPointBegin = true;
            Debug.Log ("CheckPointBegin");
        } else if (linkObject1.name == "CheckPointEnd(Clone)") {
            CheckPointEnd = true;
            Debug.Log ("CheckPointEnd");
        } else if (linkObject1.name == "Hole(Clone)") {
            Hole = true;
            Debug.Log ("Hole");
        } else if (linkObject1.name == "MoveEndPos(Clone)") {
            MoveEndPos = true;
            Debug.Log ("MoveEndPos");
        } else if (linkObject1.name == "MoveBeginPos(Clone)") {
            MoveBeginPos = true;
            Debug.Log ("MoveBeginPos");
        } else if (linkObject1.name == "BeginPos(Clone)") {
            BeginPos = true;
            Debug.Log ("BeginPos");
        } else if (linkObject1.name == "EndPos(Clone)") {
            EndPos = true;
            Debug.Log ("EndPos");
        } else if (linkObject1.name == "Teleport1(Clone)") {
            Teleport1 = true;
            Debug.Log ("Teleport1");
        } else if (linkObject1.name == "Teleport2(Clone)") {
            Teleport2 = true;
            Debug.Log ("Teleport2");
        }

        if (linkObject2.name == "Button(Clone)") {
            Button = true;
            Debug.Log ("Button");
        } else if (linkObject2.name == "Gate(Clone)") {
            Gate = true;
            Debug.Log ("Gate");
        } else if (linkObject2.name == "StaticCube(Clone)") {
            Cube = true;
            Debug.Log ("Cube");
        } else if (linkObject2.name == "Cross(Clone)") {
            Cross = true;
            Debug.Log ("Cross");
        } else if (linkObject2.name == "CheckPointBegin(Clone)") {
            CheckPointBegin = true;
            Debug.Log ("CheckPointBegin");
        } else if (linkObject2.name == "CheckPointEnd(Clone)") {
            CheckPointEnd = true;
            Debug.Log ("CheckPointEnd");
        } else if (linkObject2.name == "Hole(Clone)") {
            Hole = true;
            Debug.Log ("Hole");
        } else if (linkObject2.name == "MoveEndPos(Clone)") {
            MoveEndPos = true;
            Debug.Log ("MoveEndPos");
        } else if (linkObject2.name == "MoveBeginPos(Clone)") {
            MoveBeginPos = true;
            Debug.Log ("MoveBeginPos");
        } else if (linkObject2.name == "BeginPos(Clone)") {
            BeginPos = true;
            Debug.Log ("BeginPos");
        } else if (linkObject2.name == "EndPos(Clone)") {
            EndPos = true;
            Debug.Log ("EndPos");
        } else if (linkObject2.name == "Teleport1(Clone)") {
            Teleport1 = true;
            Debug.Log ("Teleport1");
        } else if (linkObject2.name == "Teleport2(Clone)") {
            Teleport2 = true;
            Debug.Log ("Teleport2");
        }

        /*
         * 0 - ButtonSpawnCube
         * 1 - ButtonWithCube
         * 2 - ButtonWithGate
         * 3 - CheckPoint
         * 4 - PushCubeWithBorders
         * 5 - MovingPlatform
         * 6 - TeleportManager
        */

        if (Button && Gate) {
            GameObject tempMan = Instantiate<GameObject> (linkPrefabs[2], new Vector3(0,0,0), Quaternion.identity) as GameObject;
            if (linkObject1.name == "Button(Clone)") {
                tempMan.GetComponent<BoxCollider> ().center = linkObject1.transform.position + moveUp;
                linkObject1.transform.SetParent (tempMan.transform);
                linkObject2.transform.SetParent (tempMan.transform);
                tempMan.GetComponent<ButtonWithGate> ().Button = linkObject1;
                tempMan.GetComponent<ButtonWithGate> ().gate = linkObject2;
                tempMan.GetComponent<ButtonWithGate> ().SetGateAndButton ();
            } else {
                tempMan.GetComponent<BoxCollider> ().center = linkObject2.transform.position + moveUp;
                linkObject1.transform.SetParent (tempMan.transform);
                linkObject2.transform.SetParent (tempMan.transform);
                tempMan.GetComponent<ButtonWithGate> ().gate = linkObject1;
                tempMan.GetComponent<ButtonWithGate> ().Button = linkObject2;
                tempMan.GetComponent<ButtonWithGate> ().SetGateAndButton ();
            }
            Button = false;
            Gate = false;
        }else if (Button && Cube) {
            GameObject tempMan = Instantiate<GameObject> (linkPrefabs[1], new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
            if (linkObject1.name == "Button(Clone)") {
                tempMan.GetComponent<BoxCollider> ().center = linkObject1.transform.position + moveUp;
                linkObject1.transform.SetParent (tempMan.transform);
                linkObject2.transform.SetParent (tempMan.transform);
                tempMan.GetComponent<ButtonWithCube> ().Cube = linkObject2;
                tempMan.GetComponent<ButtonWithCube> ().SetStartPos ();
            } else {
                tempMan.GetComponent<BoxCollider> ().center = linkObject2.transform.position + moveUp;
                linkObject1.transform.SetParent (tempMan.transform);
                linkObject2.transform.SetParent (tempMan.transform);
                tempMan.GetComponent<ButtonWithCube> ().Cube = linkObject1;
                tempMan.GetComponent<ButtonWithCube> ().SetStartPos ();
            }
            Button = false;
            Cube = false;
        } else if (Button && Cross) {
            GameObject tempMan = Instantiate<GameObject> (linkPrefabs[0], new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
            if (linkObject1.name == "Button(Clone)") {
                tempMan.GetComponent<BoxCollider> ().center = linkObject1.transform.position + moveUp;
                linkObject1.transform.SetParent (tempMan.transform);
                linkObject2.transform.SetParent (tempMan.transform);
                tempMan.GetComponent<ButtonSpawnsCube> ().SetSpawn ();
            } else {
                tempMan.GetComponent<BoxCollider> ().center = linkObject2.transform.position + moveUp;
                linkObject1.transform.SetParent (tempMan.transform);
                linkObject2.transform.SetParent (tempMan.transform);
                tempMan.GetComponent<ButtonSpawnsCube> ().SetSpawn ();
            }
            Button = false;
            Cross = false;
        } else if (CheckPointBegin && CheckPointEnd) {

        } else if (Hole && Gate) {

        } else if (MoveBeginPos && MoveEndPos) {

        } else if (BeginPos && EndPos) {

        } else if (Teleport1 && Teleport2) {

        }

        linkIDs.Remove (linkIDs[linkID1]);
        linkIDs.Remove (linkIDs[linkID2]);
        linkObjects.Remove (linkObjects[linkID1]);
        linkObjects.Remove (linkObjects[linkID2]);
        if (linkIDs.Count == 0) {
            LinkingDone = true;
        }
    }
}
