using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrManager : MonoBehaviour {

    [SerializeField] GameObject startGo;
    [SerializeField] GameObject personPrefab;

    void Start(){
        InvokeRepeating("SpawnPerson",Random.Range(0.01f, 1f),Random.Range(0.01f, 1f));
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Application.LoadLevel(Application.loadedLevel);
        }
	}

    void SpawnPerson() {
        Instantiate(personPrefab,startGo.transform.position,Quaternion.identity);
        
    }

}
