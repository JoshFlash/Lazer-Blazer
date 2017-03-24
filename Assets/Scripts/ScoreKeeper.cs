using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

	private Text scoreText;

	public static int score;

	void Start () {
		scoreText = GetComponent<Text>();
	}
	
	void Update () {
		scoreText.text = score.ToString();
	}
}
