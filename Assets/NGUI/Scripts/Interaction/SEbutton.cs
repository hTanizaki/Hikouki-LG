using UnityEngine;
using System.Collections;

public class SEbutton : MonoBehaviour {
	
	public AudioClip audioClip;
	AudioSource audioSource;
	
	void Start() {
		audioSource = gameObject.GetComponent<AudioSource>();

	}
	
	void Update () {

	}

	public void OnSEbutton(){
		audioSource.clip = audioClip;
		audioSource.Play ();
	}
}