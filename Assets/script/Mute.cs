using UnityEngine;
using System.Collections;

public class Mute : MonoBehaviour {
	private float volMinus  = 0.003f;
	private bool MinEnable = false;

	private float volPlus = 0.003f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.audio.volume <= 0.3f) {
			float vol = this.gameObject.audio.volume;
			vol = vol + volPlus;
			this.gameObject.audio.volume = vol;
		}

		if(MinEnable){
			Minus();
		}

	}

	void Minus(){
		float vol = this.gameObject.audio.volume;
		vol = vol - volMinus;
		this.gameObject.audio.volume = vol;
	}

	void MinusEnabled(){
		MinEnable = true;
	}
}
