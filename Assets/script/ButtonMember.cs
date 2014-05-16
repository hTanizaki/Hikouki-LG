using UnityEngine;
using System.Collections;

public class ButtonMember : MonoBehaviour {

	public int memberNum;			//レースの参加人数
	private GameObject obj;			//"SelectMember"を管理しているオブジェ

	void Start(){
		this.obj = GameObject.Find ("SelectMember");		//親オブジェを習得
	}


	void OnMouseDown(){
		this.obj.GetComponent<SelectMember> ().MemberNumber = this.memberNum;	//"SelectMember"を管理しているオブジェ（GameMain）にレース参加人数を渡す
	}
}
