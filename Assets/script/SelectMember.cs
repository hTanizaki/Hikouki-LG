using UnityEngine;
using System.Collections;

public class SelectMember : MonoBehaviour {
	//-------------------------------------------------------------------
	//Field
	//-------------------------------------------------------------------
	private int memberNumber;		//ゲームに参加するキャラクターの人数を収納
	//-------------------------------------------------------------------
	//SetGet
	//-------------------------------------------------------------------
	//子オブジェ「ButtonMember」が反応する事で呼ばれるメソッド
	//「ButtonMember」にてプレイヤーが選択した「レース参加人数」を取得し,親オブジェ「GameMain」に渡す
	public int MemberNumber{
		set{
			this.memberNumber = value;
			GameObject.Find ("GameMain").GetComponent<GameMain>().MemberNumber = this.memberNumber;
		}
	}
}
