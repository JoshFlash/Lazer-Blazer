using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;
	public AudioClip[] musicClips = new AudioClip[4];

	private AudioSource musicSource;

	private void Awake() {
		musicSource = GetComponent<AudioSource>();
	}

	void Start () {
		OnLoadPlayMusic();
	}

	void OnLoadPlayMusic () {
		musicSource.Stop();
		musicSource.clip = musicClips[SceneManager.GetActiveScene().buildIndex];
		musicSource.Play();
		musicSource.loop = true;
	}
}
