using UnityEngine;
using System.Collections;

public class testanim : MonoBehaviour {

	private float animInterval = 0.1f;
	private float animTimer = 0.0f;
	private bool animFlag = true;
	private Vector2 animOffset = new Vector2();
	
	void Start()
	{
	}
	
	void Update()
	{
		Anim();
	}
	
	// アニメーション。
	void Anim()
	{
		animTimer += Time.deltaTime;    
		
		if( animTimer > animInterval )
		{
			animTimer = 0;
			
			animOffset.x = animFlag ? 0.5f : 0.0f;
			animOffset.y = 0.0f;
			//renderer.material.mainTextureOffset = animOffset;
			renderer.sharedMaterial.mainTextureOffset = animOffset;
			
			animFlag = !animFlag;
		}
	}
}
