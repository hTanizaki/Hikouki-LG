using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {
	
	Vector2 SPEED = new Vector2(-0.6f, -0.05f);
	
	void Start()
	{
	}
	
	void Update()
	{
		Move();
		if (gameObject.transform.position.x <= -17) {
			Object.Destroy(gameObject);
		}
	}
	void Move()
	{
		
		Vector2 Position = transform.position;
		
		Position.x += SPEED.x;
		
		transform.position = Position;
		
	}
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "des" ) {
			Destroy (gameObject);
		}
	}

}
