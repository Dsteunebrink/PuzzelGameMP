using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadLevel : MonoBehaviour
{

    private GetLevel getLevel;
    private string levelPath;

    private GetObjects currentObject;

    private string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        getLevel = GameObject.Find ("GetLevel").GetComponent<GetLevel> ();
        levelPath = getLevel.levelName;
        Destroy (getLevel.gameObject);

        levelToLoad = File.ReadAllText (levelPath);
        currentObject = JsonUtility.FromJson<GetObjects> (levelToLoad);

        Debug.Log (currentObject.objects.Lengths);
    }

    [System.Serializable]
    public class GetObjects {

        [System.Serializable]
        public class GetObjectData {
            public string id;
            public int value;
            public string link;
        }

        public GetObjectData[] objects;
    }


}
