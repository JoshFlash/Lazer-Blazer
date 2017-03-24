using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {

	public float damage;

	private float lazerSpeed;
	private float lazerLife;

	// Use this for initialization
	void Start() {
		damage = 150;
		//lazerSpeed = 5f;
		lazerLife = 3f;
	}

	// Update is called once per frame
	void Update() {
		//transform.position += new Vector3(0, lazerSpeed*Time.deltaTime, 0);
		Destroy(gameObject, lazerLife);
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		if		(collider.tag == "enemy" && this.tag == "lazer")		{ Destroy(gameObject); } 
		if		(collider.tag == "player" && this.tag == "enemy_lazer")	{ Destroy(gameObject); }
	}

	public float GetDamage() {
		return damage;
	}
}
