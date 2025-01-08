using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public GameObject friendPrefab;
    public GameObject boxPrefab;
    public GameObject[] spawns;
    private float cooldown = 0;
    private float cooldown1 = 0;
    private float cooldown2 = 0;
    private int random;

    private void Update() {
        if (Time.timeScale == 0) {
            cooldown = 5;
            cooldown1 = 5;
            cooldown2 = 5;
        }
        if (cooldown <= 0) {
            random = Random.Range(0, 5);
            if (random == 1) {
                Instantiate(boxPrefab, spawns[0].transform.position, transform.rotation);
            } 
            else if (random > 1 && random < 4) {
                Instantiate(friendPrefab, spawns[0].transform.position, transform.rotation);
            } 
            else {
                Instantiate(enemyPrefab, spawns[0].transform.position, transform.rotation);
            }
            cooldown = 5;
        }
        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
        }
        if (cooldown1 <= 0) {
            random = Random.Range(0, 5);
            if (random == 1) {
                Instantiate(boxPrefab, spawns[1].transform.position, transform.rotation);
            }
            else if (random > 1 && random < 4) {
                Instantiate(friendPrefab, spawns[1].transform.position, transform.rotation);
            }
            else {
                Instantiate(enemyPrefab, spawns[1].transform.position, transform.rotation);
            }
            cooldown1 = 5;
        }
        if (cooldown1 > 0) {
            cooldown1 -= Time.deltaTime;
        } 
        if (cooldown2 <= 0) {
            random = Random.Range(0, 5);
            if (random == 1) {
                Instantiate(boxPrefab, spawns[2].transform.position, transform.rotation);
            }
            else if (random > 1 && random < 4) {
                Instantiate(friendPrefab, spawns[2].transform.position, transform.rotation);
            }
            else {
                Instantiate(enemyPrefab, spawns[2].transform.position, transform.rotation);
            }
            cooldown2 = 5;
        }
        if (cooldown2 > 0) {
            cooldown2 -= Time.deltaTime;
        }
    }
}
