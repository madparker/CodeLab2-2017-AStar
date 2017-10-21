using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrManager : MonoBehaviour {

    [SerializeField] GameObject startGo;
    [SerializeField] GameObject personPrefab;

    float rndSpawnTime;

    void Start(){
        rndSpawnTime = Random.Range(0.05f, 0.11f);
        InvokeRepeating("SpawnPerson",rndSpawnTime,rndSpawnTime);
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Application.LoadLevel(Application.loadedLevel);
        }
	}

    void SpawnPerson() {
        Instantiate(personPrefab,startGo.transform.position,Quaternion.identity);
        rndSpawnTime = Random.Range(0.05f, 0.1f);
    }

}
