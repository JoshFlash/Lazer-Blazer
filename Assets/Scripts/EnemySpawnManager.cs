using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {
	//the purpose of this class is to intereact with enemy spawners and manage spawned enemies

	public static float accumTime; //all enemies spawned from same spawner oscilate together

	public GameObject[] leftFleaSpawnerPrefabs = new GameObject[4];
	public GameObject[] rightFleaSpawnerPrefabs = new GameObject[4];
	public GameObject[] leftBeeSpawnerPrefabs = new GameObject[4];
	public GameObject[] rightBeeSpawnerPrefabs = new GameObject[4];
	public static bool allDead;

	[HideInInspector]
	public bool spawnBee;

	void Awake() {
		accumTime = 0;
		SpawnFleaEnemies();
		allDead = false;
	}

	void Start() {
		InvokeRepeating("AllDeadChecker", 3f, 2.5f);
	}

	void Update() {
		accumTime += Time.deltaTime;
		if (allDead && spawnBee) {
			SpawnBeeEnemies();
		} else if (allDead && !spawnBee) {
			SpawnFleaEnemies();
		}
	}

	void CreateLeftFleaSpawner(int row) {

		GameObject fleaSpawner = Instantiate(leftFleaSpawnerPrefabs[row], transform.position, Quaternion.identity);
		fleaSpawner.transform.parent = this.transform;

	}

	void CreateRightFleaSpawner(int row) {
		GameObject fleaSpawner = Instantiate(rightFleaSpawnerPrefabs[row], transform.position, Quaternion.identity);
		fleaSpawner.transform.parent = this.transform;

	}

	void CreateLeftBeeSpawner(int row) {

		GameObject fleaSpawner = Instantiate(leftBeeSpawnerPrefabs[row], transform.position, Quaternion.identity);
		fleaSpawner.transform.parent = this.transform;

	}

	void CreateRightBeeSpawner(int row) {
		GameObject fleaSpawner = Instantiate(rightBeeSpawnerPrefabs[row], transform.position, Quaternion.identity);
		fleaSpawner.transform.parent = this.transform;

	}

	bool AllEnemiesDead() {
		foreach (Transform child in transform) {
			if (child.childCount > 0) { return false; }
		}
		return true;
	}

	void SpawnFleaEnemies() {
		for (int i = 0; i < 4; i++) { CreateLeftFleaSpawner(i); }
		for (int i = 0; i < 4; i++) { CreateRightFleaSpawner(i); }
		allDead = false;
		spawnBee = true;
	}
	
	void SpawnBeeEnemies() {
		for (int i = 0; i < 4; i++) { CreateLeftBeeSpawner(i); }
		for (int i = 0; i < 4; i++) { CreateRightBeeSpawner(i); }
		allDead = false;
		spawnBee = false;
	}

	void AllDeadChecker() {
		allDead = AllEnemiesDead();
	}


}
