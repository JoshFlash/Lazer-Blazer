using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static float shieldLife;


	public float xSpeed, ySpeed;
	public GameObject lazerLeftPrefab;
	public GameObject lazerRightPrefab;
	public GameObject playerShield;
	public GameObject explosion;
	public AudioClip lazerSound, splosionSound, hitSound;
	public SpriteRenderer srenderer;

	private float lazerSpeed;
	private float xMin, xMax, yMin, yMax;
	private float fireRate;
	// private float timeSinceLastFire; //keeps time since lazer was fired (commented out due to InvokeRepeating being used)
	private PlayerShip ship;
	public float shipHealth;

	private struct PlayerShip {

		public float height;
		public float width;
		public float pixelsPerUnit;
		public PlayerShip(float h, float w, float ppu) {
			height = h; width = w; pixelsPerUnit = ppu;
		}
	}

	void Awake() {
		PlayerShip ship = new PlayerShip(0.8f, 0.5f, 108f); // values obtained from sprite
		FindPlaySpace(ship);
	}

	void Start() {
		yMax *= -0.2f;  // constrain ship to lower half of screen
		//timeSinceLastFire = ( 1 / fireRate ) + 0.1f; //possible to shoot initially (commented out due to InvokeRepeating...)
		fireRate = 0.16f;
		lazerSpeed = 7f;
		srenderer = this.gameObject.GetComponent<SpriteRenderer>();
		playerShield.SetActive(false);
	}

	void Update() {
		MoveShip();
		ShootLazer();
		UseShield();

	}

	private void OnTriggerEnter2D(Collider2D collider) {
		Lazer lazer = collider.gameObject.GetComponent<Lazer>();
		if (collider.tag == "enemy_lazer") {
			shipHealth -= lazer.GetDamage() / 3;
			srenderer.color -= new Color(0, .08f, .08f, 0);
			AudioSource.PlayClipAtPoint(hitSound, transform.position);
		}
		if (shipHealth <= -10) {
			ShipDies();
		}
	}

	void FindPlaySpace(PlayerShip ship) {
		//the folloing values determine possible max and min positions (in world units) for the player's ship
		// 0 is the screen's center and we add the width and height of the ship sprite as measured from its center
		xMin = ship.width - Screen.width / ( 2 * ship.pixelsPerUnit );
		xMax = -ship.width + Screen.width / ( 2 * ship.pixelsPerUnit );
		yMin = ship.height - Screen.height / ( 2 * ship.pixelsPerUnit );
		yMax = -ship.height + Screen.height / ( 2 * ship.pixelsPerUnit );
	}

	void MoveShip() {
		if (Input.GetKey(KeyCode.A) && this.transform.position.x > xMin) { this.transform.position += new Vector3(-xSpeed * Time.deltaTime, 0f, 0f); }
		if (Input.GetKey(KeyCode.D) && this.transform.position.x < xMax) { this.transform.position += new Vector3(+xSpeed * Time.deltaTime, 0f, 0f); }
		if (Input.GetKey(KeyCode.S) && this.transform.position.y > yMin) { this.transform.position += new Vector3(0f, -ySpeed * Time.deltaTime, 0f); }
		if (Input.GetKey(KeyCode.W) && this.transform.position.y < yMax) { this.transform.position += new Vector3(0f, ySpeed * Time.deltaTime, 0f); }
	}

	void ShootLazer() {

		if (Input.GetKeyDown(KeyCode.Space)) {
			FireLazer();
			InvokeRepeating("FireLazer", fireRate, fireRate);
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("FireLazer");
		}

		/* //This isegment was replaced by Invokerepeating above - it essentially does the same thing
		if (Input.GetKey(KeyCode.Space) && timeSinceLastFire>(1/fireRate)) {
			GameObject lazerLeft = Instantiate(lazerLeftPrefab, new Vector3(transform.position.x - 0.4f, transform.position.y, 0), Quaternion.identity);
			GameObject lazerRight = Instantiate(lazerRightPrefab, new Vector3(transform.position.x + 0.4f, transform.position.y, 0), Quaternion.identity);
			timeSinceLastFire = 0f;
		}
		timeSinceLastFire += Time.deltaTime; */
	}

	void FireLazer() {
		//used with InvokeRepeating (although my code was actually cleaner* before - just for practice) *the commented out code also prevents successive rapid tapping of the 'fire' button which Invoke doesn't necessarily do
		GameObject lazerLeft = Instantiate(lazerLeftPrefab, new Vector3(transform.position.x - 0.4f, transform.position.y, 0), Quaternion.identity) as GameObject;
		GameObject lazerRight = Instantiate(lazerRightPrefab, new Vector3(transform.position.x + 0.4f, transform.position.y, 0), Quaternion.identity) as GameObject;
		Rigidbody2D lBody = lazerLeft.GetComponent<Rigidbody2D>();
		Rigidbody2D rBody = lazerRight.GetComponent<Rigidbody2D>();
		lBody.velocity = new Vector3(0, lazerSpeed, 0);
		rBody.velocity = new Vector3(0, lazerSpeed, 0);
		AudioSource.PlayClipAtPoint(lazerSound, transform.position);
	}

	void UseShield() {
		if (Input.GetKey(KeyCode.RightShift) && shieldLife >= 0) {
			playerShield.SetActive(true);
			shieldLife -= 1f * Time.deltaTime;
			if (shieldLife < 0) { shieldLife = -1; }
		} else {
			playerShield.SetActive(false);
			if (!Input.GetKey(KeyCode.RightShift) && shieldLife < 2f) {
				shieldLife += 0.5f * Time.deltaTime;
			}
		}
	}

	public void ShipDies() {
		Instantiate(explosion, this.transform.position, Quaternion.identity);
		Invoke("LoadEndScene", 4f);
		AudioSource.PlayClipAtPoint(splosionSound, transform.position);

		//Destroy(gameObject);
		srenderer.sprite = null;
		transform.position = new Vector3(0, 100, 0);
	}

	public void LoadEndScene() {
		SceneManager.LoadScene("EndGame");
	}

}
