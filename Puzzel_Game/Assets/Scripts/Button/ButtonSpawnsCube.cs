using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawnsCube : MonoBehaviour
{
    private bool spawnCube;
    private Vector3 spawnPos;

    [SerializeField] private GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = this.transform.Find ("Cross").position;
        Destroy (GameObject.Find ("Cross"));
    }

    private void SpawnCube () {
        GameObject CloneCube = Instantiate (cube, spawnPos, Quaternion.identity);
    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("Player")) {
            SpawnCube ();
        }
    }
}
