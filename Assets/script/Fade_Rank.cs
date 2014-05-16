using UnityEngine;
using System.Collections;

public class Fade_Rank : MonoBehaviour {

	private float fAlpha = 0.1f;
	private float fOriginAlpha = 0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FadeAlphaRank(UIWidget w){
		fOriginAlpha = w.alpha;
		fOriginAlpha += fAlpha;
		w.alpha = fOriginAlpha;
	}
}
