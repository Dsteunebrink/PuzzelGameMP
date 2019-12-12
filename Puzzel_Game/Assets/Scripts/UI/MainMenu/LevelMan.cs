using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LevelMan : MonoBehaviour
{

    private string path;
    [SerializeField] private List<Text> buttonText;
    private bool done;


    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/Levels!";
        foreach (string file in System.IO.Directory.GetFiles (path)) {
            int i = 0;
            if (file.Split ('.').Length == 2) {
                buttonText[i].text = file.Split('!')[1];
                buttonText[i].transform.parent.name = file.Split ('!')[1];
                buttonText.RemoveAt (i);
            }
        }
        done = true;
    }

    // Update is called once per frame
    void Update()
    {
        DeleteNonUsedButtons ();
    }

    private void DeleteNonUsedButtons () {
        if (done) {
            for (int i = 0; buttonText.Count > i; i++) {
                Destroy (buttonText[i].transform.parent.gameObject);
                buttonText.RemoveAt (i);
                if (buttonText.Count == 0) {
                    Destroy (this);
                }
            }
        }
    }
}
