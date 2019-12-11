﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMan : MonoBehaviour
{

    private string path;
    [SerializeField] private List<Text> buttonText;
    private bool done;


    // Start is called before the first frame update
    void Start()
    {
        path = "/MP_PuzzelGame/Puzzel_Game/Assets/Levels!";
        foreach (string file in System.IO.Directory.GetFiles (path)) {
            int i = 0;
            if (file.Split ('.').Length == 2) {
                Debug.Log (file);
                buttonText[i].text = file.Split('!')[1];
                buttonText.RemoveAt (i);
                i++;
                if (i == System.IO.Directory.GetFiles (path).Length / 2) {
                    done = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (done) {
            for (int i = 0; buttonText.Count < i; i++) {
                Destroy (buttonText[i].gameObject);
                buttonText.RemoveAt (i);
            }
        }
    }
}