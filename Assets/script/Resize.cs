using UnityEngine;
using System.Collections;

public class Resize : MonoBehaviour {

	private float Wid_Magnification;						//横幅の倍率
	private float Hei_Magnification;						//縦幅の倍率

	GameObject obj;

	Vector3 Scale;

	public enum aspPattern{
		ON,
		OFF
	}

	public aspPattern aspSwitch;

	void Start(){

		//スクリーンのピクセル取得、倍率計算
		this.Wid_Magnification = (float)Screen.width / 400f;
		this.Hei_Magnification = (float)Screen.height / 600f;

		//アスペクト固定or非固定　分岐
		if (aspSwitch == aspPattern.ON){
		}

		Scale = new Vector3 (gameObject.transform.localScale.x * Wid_Magnification * (1 / Hei_Magnification), gameObject.transform.localScale.y, 1);
		gameObject.transform.localScale = Scale;
	}






	// Use this for initialization
	void Update () {
	}
}
