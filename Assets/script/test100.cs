using UnityEngine;
using System.Collections;

public class test100 : MonoBehaviour {


	GameObject obj;

	Vector2 SPEED = new Vector2(0.5f, 0.05f);
	
	void Start()
	{
		obj = GameObject.Find ("GameMain");
	}
	
	void Update()
	{
		Move();
	}

	void Move()
	{

		Vector2 Position = transform.position;

		Position.x += SPEED.x;

		transform.position = Position;

		if (Position.x >= 19.08186f) {
			//StartCoroutine("AnimeWait");
			obj.GetComponent<GameMain>().FinFlag = true;
			Destroy (gameObject);
		}

	}

	public IEnumerator AnimeWait(){
		yield return new WaitForSeconds (5);
	}

}
