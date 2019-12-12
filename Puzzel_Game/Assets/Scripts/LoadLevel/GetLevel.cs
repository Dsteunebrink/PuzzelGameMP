using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GetLevel : MonoBehaviour
{
    private string levelName;

    // Start is called before the first frame update
    void Start () {
        DontDestroyOnLoad (this.gameObject);
    }

    // Update is called once per frame
    void Update () {

    }

    public void GetLevelFromButton () {
        levelName = "/MP_PuzzelGame/Puzzel_Game/Assets/Levels!" + EventSystem.current.currentSelectedGameObject.name;
    }
}
