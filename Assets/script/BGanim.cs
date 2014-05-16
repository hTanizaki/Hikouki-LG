using UnityEngine;
using System.Collections;

public class BGanim : MonoBehaviour {

	
	 public static float scrollSpeed;
	
	void Start ()
	{
		scrollSpeed = 0.3f;
	}
	
	void Update ()
	{
		float speed = Time.deltaTime * scrollSpeed;
		renderer.material.mainTextureOffset = new Vector2( renderer.material.mainTextureOffset.x + speed, 0);
	}
}