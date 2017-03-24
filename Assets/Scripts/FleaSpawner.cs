using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleaSpawner : MonoBehaviour {

	public int spawnRow;
	public short totalFleasToSpawn;
	public bool isRight;
	public GameObject fleaPrefab;

	[HideInInspector]
	public int spawnSide, fleasSpawned;

	void Start() {
		fleasSpawned = 0;
		spawnSide = SpawnSide(isRight);
	}

	void Update() {
		if (fleasSpawned < totalFleasToSpawn) { SpawnFlea(); }
	}

	void SpawnFlea() {
		GameObject flea = Instantiate(fleaPrefab, new Vector3(0, 6, 0), Quaternion.identity) as GameObject;
		flea.transform.parent = this.transform;
		fleasSpawned++;
	}

	int SpawnSide(bool isRight) {
		if (isRight) return 1;
		else return -1;
	}

}
