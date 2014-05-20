using UnityEngine;
using System.Collections;

public class ButtonAction : MonoBehaviour {

	public enum Trigger{
		OnClick,
	}

	public GameObject[] target;
	public string[] methodName;

	private Trigger trigger = Trigger.OnClick;
	// Use this for initialization
	void Start () {


	}
	// Update is called once per frame
	void Update () {
	


	}

	void OnClick () { 
		//クリックしたとき
		if (trigger == Trigger.OnClick) {
			//GameObjectの要素数
			int num = target.Length; 
			//GameObjectの要素数とmethodの要素数が合っているか
			if(num != methodName.Length)
				SendMessage ("ERROR");
			Send (num);
		}
	}

	void Send (int num)
	{

		for(int j = 0; j <= num-1; j++){

			//念のためエラー処理を入れる
			if (methodName[j] == null || num == 0) {
				break;
			}
			if (target[j] == null || num == 0){
				break;
			}

			//Transformコンポーネントを取得しておく
			Transform[] transforms = target[j].GetComponentsInChildren<Transform>();

			//実行させる
			for (int i = 0, imax = transforms.Length; i < imax; ++i)
			{
				Transform t = transforms[i];
				t.gameObject.SendMessage(methodName[j], gameObject, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}