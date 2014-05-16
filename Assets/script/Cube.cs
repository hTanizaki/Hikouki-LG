using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	public GameObject enemy;
	public GameObject goal;
	int m;
	bool flag;
	float time;
	float time2;
	int t=0;
	// Use this for initialization
	void Start () {
		flag = true;
		time2=6;
	}
	
	// Update is called once per frame
	void Update () {
		 
		if(!test.set[0] && !test.set[1] &&flag){

			m=Random.Range(5,10);
			//Debug.Log (t);

			time2+=Time.deltaTime;


			if(time2>=3){

				if(t==2){
					flag=false;

					if(Application.loadedLevelName=="test"){
					CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { Application.LoadLevel("test2"); });
					}
					if(Application.loadedLevelName=="test2"){
						CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0.5f, () => { Application.LoadLevel("test"); });
					}

				}else {

			for(int i=0; i<=m; i++){
				BGanim.scrollSpeed=0.7f;
				time+=Time.deltaTime;

				if(time>=2){
				Instantiate(enemy, new Vector3(0,Random.Range(-6,2) , 0), transform.rotation);

				if(i==m){
					//flag=false;
						//time=0;
							t+=1;
						//Application.LoadLevel("test2");
								if(t==2){
									Instantiate(goal, new Vector3(0,-2 , 0), transform.rotation);
								}
								BGanim.scrollSpeed = 0.3f;
							time=0;
								time2=0;
						}
					time=0;
					}
				}



				}

			}
		}
	
	}
}
