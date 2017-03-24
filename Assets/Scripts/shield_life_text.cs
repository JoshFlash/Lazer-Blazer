using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class shield_life_text : MonoBehaviour {

	public Text shieldText;

	private void Start() {
		shieldText = GetComponent<Text>();
	}


	void Update () {
		float shield = 100*PlayerController.shieldLife;
		shield = Mathf.Clamp(shield, 0f, 200f);
		int shieldInt = (int)shield;
		shieldText.text = shieldInt.ToString();
	}
}
