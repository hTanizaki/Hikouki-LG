using UnityEngine;
using System.Collections;

public class ButtonMember : MonoBehaviour {

	public int memberNum;			//レースの参加人数
	private GameObject obj;			//"SelectMember"を管理しているオブジェ
	private GameObject AudioObj;

	void Start(){
		AudioObj = GameObject.Find("SE");
		this.obj = GameObject.Find ("SelectMember");		//親オブジェを習得
	}

	void OnMouseDown(){
		AudioObj.GetComponent<SEbutton> ().OnSEbutton ();
		this.obj.GetComponent<SelectMember> ().MemberNumber = this.memberNum;	//"SelectMember"を管理しているオブジェ（GameMain）にレース参加人数を渡す
	}
}
