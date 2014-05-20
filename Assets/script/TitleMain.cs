using UnityEngine;
using System.Collections;

public class TitleMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SceneChange(){
		GameObject.Find ("START").GetComponent<BoxCollider>().enabled = false;;
		CameraFade.StartAlphaFade(Color.black, false, 3f, 0f, () => {  Application.LoadLevel("test"); });
	}
}
