using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrManager : MonoBehaviour {

    [SerializeField] GameObject startGo;
    [SerializeField] GameObject personPrefab;
    [SerializeField] GameObject[] princesses;

    float timer = 0.5f;
    float princessTimer = 2f;


	void Update () {

        timer -= Time.deltaTime;
        princessTimer -= Time.deltaTime;

        if (timer<=0) {
            SpawnPerson();
        }

        if (princessTimer <= 0) {
            SpawnPrincess();
            }
        if (Input.GetKeyDown(KeyCode.Space)) {
            Application.LoadLevel(Application.loadedLevel);
        }
	}

    void SpawnPerson() {
        Instantiate(personPrefab,startGo.transform.position,Quaternion.identity);
        timer = Random.Range(0.05f, 0.5f);
    }

    void SpawnPrincess() {
        int rnd = Random.Range(0, princesses.Length);
        Instantiate(princesses[rnd], startGo.transform.position, Quaternion.identity);
        princessTimer = Random.Range(1f, 5f);
        }

}
