using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GetLevel : MonoBehaviour
{
    public string levelName;

    // Start is called before the first frame update
    void Start () {
        DontDestroyOnLoad (this.gameObject);
    }

    // Update is called once per frame
    void Update () {

    }

    public void GetLevelFromButton () {
        levelName = Application.dataPath + "/Levels!" + EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }
}
