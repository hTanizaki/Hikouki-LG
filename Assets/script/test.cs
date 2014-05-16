using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
	public int i;

	public static float[] X=new float[2];
	public static float[] Y=new float[2];
	// Use this for initialization
	public static bool[] set = new bool[2];
	void Start () {
		X[i] = transform.localPosition.x;
		Y[i] = transform.localPosition.y;
		set[i]=true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll)  {
		if (coll.gameObject.tag =="tesd") {
			//Debug.Log("hit");
			//Destroy (coll.gameObject);
		}
	}

	public float[] Set{
		set{
//			this.mouseFlag = value;
		}
	}

}
