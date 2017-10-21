using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrManager : MonoBehaviour {

    [SerializeField] GameObject startGo;
    [SerializeField] GameObject personPrefab;

    float timer;


	void Update () {

        timer -= Time.deltaTime;

        if (timer<=0) {
            SpawnPerson();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            Application.LoadLevel(Application.loadedLevel);
        }
	}

    void SpawnPerson() {
        Instantiate(personPrefab,startGo.transform.position,Quaternion.identity);
        timer = Random.Range(0.05f, 1f);
    }

}
