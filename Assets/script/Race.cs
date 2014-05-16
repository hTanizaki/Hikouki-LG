using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//敵キャラ1機を追い越す時間は0.1s
public class Race : MonoBehaviour {
	//状態遷移管理用の列挙型宣言
	enum scene{
		CALCULATION,		//セットしたキャラステータスから追い越し人数を算出
		ANIMATION,			//追い越しアニメーション
		NEXT				//次の「追い越し」処理への準備
	}
	//------------------------------------------------------------------------
	//Field
	//------------------------------------------------------------------------
	private GameObject setMember1;					//セットポジション１に設置したキャラのオブジェ情報
	private GameObject setMember2;					//セットポジション２に設置したキャラのオブジェ情報
	private int Forerun;							//追い越し人数を収納
	private int token;								//追い越した人数を収納							
	public int currentTurn;						//追い越し処理の未処理回数を収納
	private float time;								//追い越しアニメーション開始時の時間を収納
	private float currentTime;						//現在時間を収納
	private bool waitFlag;							//追い越し処理終了後の待機管理フラグ
	private scene raceScene = scene.CALCULATION;	//レースの状態をCALCULATIONに設定
	private GameObject enemy;						//追い越す敵キャラのオブジェ情報
	private int ranking = 100;						//プレイヤーのレース順位
	
	//------------------------------------------------------------------------
	//Method
	//------------------------------------------------------------------------
	void Start () {
		//各フィールドの初期化
		time = 0;
		currentTime = 0;
		token = 0;
		waitFlag = true;
		currentTurn = GameObject.Find ("GameMain").GetComponent<GameMain>().MemberNumber;		//レース参加人数を取得
		enemy = (GameObject)Resources.Load("enemy");		//「Resourceフォルダ」からenemy（プレハブ）を取得
	}

	void Update () {
		GameObject.Find("Score").transform.FindChild ("Num").GetComponent<GUIText> ().text = ranking.ToString();
		switch (raceScene) {
		//追い越し人数算出処理
		case scene.CALCULATION :
			switch(currentTurn){
			case 1:
				Forerun = (int)(this.setMember1.GetComponent<Mouse>().GetATK * 0.1f);
				break;
			case 2:
				Forerun = (int)(this.setMember2.GetComponent<Mouse>().GetATK * 0.1f);
				break;
			case 3:
				Forerun = (int)(this.setMember2.GetComponent<Mouse>().GetATK * 0.1f);
				break;
			}
		/*	if(currentTurn > 1){
				Forerun = (int)(this.setMember2.GetComponent<Mouse>().GetATK * 0.1f);
			}else{
				Forerun = (int)(this.setMember1.GetComponent<Mouse>().GetATK * 0.1f);
			}
		*/
		
			raceScene = scene.ANIMATION;				//シーン状態をANIMATIONに移行
			this.time = Time.realtimeSinceStartup;		//１アニメーション開始時刻を取得
			BGanim.scrollSpeed = 0.7f;					//BGのスクロール速度を0.3 -> 0.7
			break;

		//追い越しアニメーション処理
		case scene.ANIMATION:
			this.currentTime = Time.realtimeSinceStartup;		//現在時刻を習得
			//timeとcurrentTimeの差分が0.1s　で敵キャラ1機を流す（右から左へ流す）
			if(this.currentTime - this.time >= 0.07f){
				//追い越し予定数と同値まで追い越したらレース状態をANIMATION -> NEXTへ移行
				if(this.Forerun <= this.token || ranking == 1){
					raceScene = scene.NEXT;			//シーンをNEXTへ移行
					this.currentTurn--;				//追い越し処理,未処理回数をデクリメント
					StartCoroutine("WaitNext");		//WaitNextコルーチンを始動
					this.token = 0;					//追い越した数カウンターをゼロリセット
					BGanim.scrollSpeed = 0.3f;		//BGのスクロール速度を0.7 -> 0.3へ戻す
					break;
				}
				//流れる敵キャラオブジェのクローン生成
				Instantiate(enemy,new Vector3(17,Random.Range(-8,8),-1), Quaternion.Euler(0, -90, 90));
				this.time = this.currentTime;
				this.token++;
				if(ranking > 1)
				{
					ranking--;
				}

			}
			break;

		//「追い越し」の後処理　　次の動作を判定（「追い越し」未処理回数によって分岐）
		case scene.NEXT :
			switch(currentTurn){
			case 0 :
				GameObject.Find("GameMain").GetComponent<GameMain>().FinFlag = true;	//GameMainでのシーン終了フラグをtrueへ
				break;

			//コルーチンWaitNextにてwaitFlagがfalseにされればCALCUCATIONへシーン移行
			case 1 :
				if(!this.waitFlag){
				raceScene = scene.CALCULATION;
				
				}
				break;

			//各オブジェのマウスロックフラグをtrueに
			default :
				try{
				GameObject.Find ("tesd1").GetComponent<Mouse>().MouseFlag=true;
				}catch{}
				try{
				GameObject.Find ("tesd2").GetComponent<Mouse>().MouseFlag=true;
				}catch{}
				try{
				GameObject.Find ("tesd3").GetComponent<Mouse>().MouseFlag=true;
				}catch{}
				try{
				GameObject.Find ("tesd4").GetComponent<Mouse>().MouseFlag=true;
				}catch{}
				try{
				GameObject.Find ("tesd5").GetComponent<Mouse>().MouseFlag=true;
				}catch{}
				try{
				GameObject.Find ("Friend").GetComponent<Mouse>().MouseFlag=true;
				}catch{}

				setMember2.SetActive(false);		//セットポジションに設置されたキャラオブジェをパッシブに
			
				raceScene = scene.CALCULATION;		//CALCULATIONへシーン移行
				GameObject.Find ("GameMain").GetComponent<GameMain>().GameScene();		//GameMainのシーンをSELECT_CHARAへ移行
				GameObject.Find("SetMember").transform.FindChild("Explanation").gameObject.SetActive(true);
				GameObject.Find("Explanation").transform.FindChild("Supporter").gameObject.SetActive(true);
				gameObject.SetActive(false);		//一旦Raceオブジェをパッシブに
				break;
			}
			break;
		}	
	}
	//コルーチン
	public IEnumerator WaitNext(){
		yield return new WaitForSeconds (2);
		this.waitFlag = false;
	}
	//------------------------------------------------------------------------
	//set get
	//------------------------------------------------------------------------
	public GameObject SetMember1{
		set{
			this.setMember1 = value;
		}
	}
	
	public GameObject SetMember2{
		set{
			this.setMember2 = value;		
		}
	}

	public int GetRanking{
		get{
			return this.ranking;		
		}
	}
}
