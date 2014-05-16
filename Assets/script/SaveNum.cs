using UnityEngine;
using System.Collections;

public class SaveNum : MonoBehaviour {

	private int transNum;

	void Awake(){
		DontDestroyOnLoad (this);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (transNum);
	}

	/*public void SaveNumber(int num){
		transNum = num;
	}*/
	public void DesSaveNum(){
		Destroy(gameObject);
	}

	public int SaveNumber{
		set
		{
			transNum = value;
		}
		get
		{
			return transNum;
		}
	}
}
