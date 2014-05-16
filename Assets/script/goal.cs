using UnityEngine;
using System.Collections;

public class goal : MonoBehaviour {

	private GameObject obj;
	private bool CheckFadeFlag;
	// Use this for initialization
	void Start () {
		this.obj = GameObject.Find ("testf");
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.x <= -60) {
			BGanim.scrollSpeed = 0.3f;
			if(this.obj.transform.position.x >= 30){
				//Application.Quit();
				
				GameObject.Find("GameMain").GetComponent<GameMain>().FinFlag = true;
			}
			else{
				obj.transform.position = (obj.transform.position += new Vector3(0.3f, 0, 0));
			}
		} else {
			gameObject.transform.position = (gameObject.transform.position -= new Vector3(0.7f, 0f, 0f));
		}
		if (gameObject.transform.position.x < -10) {	
			transform.FindChild ("GoalTexture").gameObject.SetActive (true);
		}
	}
}
