using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleaController : MonoBehaviour {

	public float maxSpeed;
	public GameObject enemyLazerPrefab;
	public AudioClip lazerSound, deathsound;

	private float health;
	private float lazerSpeed;
	private bool fleaSet = false;
	private Vector3 fleaSetPos;

	private void Start() {
		GameObject parent = this.transform.parent.gameObject;
		health = Random.Range(140, 340);
		lazerSpeed = -4.5f;
		FleaSpawner fleaSpawner = GetComponentInParent<FleaSpawner>();
		fleaSetPos = new Vector3(fleaSpawner.fleasSpawned * fleaSpawner.spawnSide, fleaSpawner.spawnRow, 0);
	}

	void Update() {
		if (!fleaSet) { MoveToFormation(); } else if (fleaSet) { HoverAbove(); }
	}

	private void FixedUpdate() {
		int x = Random.Range(0, 360);
		ShootRandomly(x);
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "lazer") {
			Lazer lazer = collider.gameObject.GetComponent<Lazer>();
			health -= lazer.GetDamage();
			if (health <= 0) {
				AudioSource.PlayClipAtPoint(deathsound, transform.position);
				ScoreKeeper.score += 17;
				Destroy(gameObject);}
		}
	}

	void MoveToFormation() {
		Vector3 moveDirection = ( transform.position - fleaSetPos );
		transform.position -= moveDirection.normalized * 0.1f;
		if (moveDirection.magnitude <= 0.1f) {
			transform.position = fleaSetPos;
			fleaSet = true;
		}
	}

	void HoverAbove() {
		float dxMagnitude = Mathf.Cos(( 3.1416f ) * EnemySpawnManager.accumTime);
		float xVelocity = maxSpeed * dxMagnitude * Time.deltaTime;
		transform.position += new Vector3(xVelocity, 0, 0);
	}

	void ShootRandomly(int x) {
		if (x == 0 && fleaSet) {
			GameObject enemyLazer = Instantiate(enemyLazerPrefab, transform.position, Quaternion.identity);
			Rigidbody2D lBody = enemyLazer.GetComponent<Rigidbody2D>();
			lBody.velocity = new Vector3(0, lazerSpeed, 0);
			AudioSource.PlayClipAtPoint(lazerSound, transform.position);
		}

	}



}
