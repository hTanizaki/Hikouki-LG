using UnityEngine;
using System.Collections;

public class SEkasoku : MonoBehaviour {
	
	public AudioClip audioClip;
	AudioSource audioSource;
	
	void Start() {
		audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.clip = audioClip;
	}
	
	void Update () {

	}

	public void OnSE(){
		audioSource.clip = audioClip;
		audioSource.Play ();
	}
}