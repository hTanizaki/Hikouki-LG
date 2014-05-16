using UnityEngine;
using System.Collections;

public class testf : MonoBehaviour {
	Vector2 SPEED = new Vector2(0.5f, 0.05f);
	// Use this for initialization
	public static bool flag;
	void Start () {
		flag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(flag){
			Move();
		}

	}

	void Move()
	{
		
		Vector2 Position = transform.position;
		
		Position.x += SPEED.x;
		
		transform.position = Position;
		
	}
}
