using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class GameMain : MonoBehaviour {
	public enum scene{			//ゲーム全体の状態遷移を管理する列挙型
		START_ANIMATION,		//OPのアニメーションシーン
		SELECT_MEMBER_NUM,		//レース参加人数を選択するシーン
		SELECT_CHARA,			//レースで使用するキャラの選択シーン
		RACE,					//レース処理のシーン
		GOAL,					//ゴールシーン
		END						//レース終了シーン
	}
	public scene gameScene = scene.START_ANIMATION;		//状態遷移管理フィールド
	public bool finFlag = false;							//各シーンの終了判定フラグ
	public int memberNumber = -1;							//レース参加人数を収納するフィールド
	public List<string> statusList = new List<string>();	//外部テキストファイルからプレイヤーデッキ各キャラのステータスを収納するリスト
	public bool loaded =false;								//外部テキストファイルの読込終了フラグ
	public int testget;
	public bool fadeFlag;
	public GameObject prefab;
	public int num;

	void Awake(){
		StartCoroutine("LoadText");							//外部テキストファイル読込コルーチンの始動

	}

	void Start(){
		CameraFade.StartAlphaFade(Color.black, true, 1f, 0f);
		
		//prefab = (GameObject)Instantiate(Resources.Load("SaveNum"));
		

	}

	void Update(){
		//ゲーム状態遷移
		switch (this.gameScene) {
		//OPアニメーション処理（実処理はスクリプト「test100.cs」にて処理）
		case scene.START_ANIMATION:
			//各シーン終了フラグ＆外部テキストファイル読込完了フラグ双方がtureならば次シーンへ移行
			if(this.finFlag && this.loaded){
				GameObject.Find("Score").transform.FindChild("Num").GetComponent<GUIText>().text = "100";
				//読み込んだ外部テキストファイルから各キャラのステータスを摘出
				ExtractStatus("tesd1");
				ExtractStatus("tesd2");
				ExtractStatus("tesd3");
				ExtractStatus("tesd4");
				ExtractStatus("tesd5");


				//”フレンドキャラ”のステータスはランダム（ATK:100～500　スタミナ：2～5）で決定
				GameObject.Find("Friend").GetComponent<Mouse>().GetATK = Random.Range(10,1000);
				GameObject.Find("Friend").GetComponent<Mouse>().GetStamina = Random.Range(2,5);

				GameObject.Find ("Scene").transform.Find ("SelectMember").gameObject.SetActive (true);		//次シーンをアクティブに

				gameScene = scene.SELECT_MEMBER_NUM;		//次シーンに移行

				this.finFlag = false;						//シーン終了フラグをfalseに戻す
			}
			break;

		//レース参加人数選択処理（実処理はスクリプト「SelectMember.cs」「ButtonMember.cs」にて処理）
		case scene.SELECT_MEMBER_NUM:
			//レース参加人数選択のボタンが押され,「ButtonMember　->　SelectMember　->　GameMain」経由で「参加人数」を取得すれば次シーンへ移行
			if(this.memberNumber != -1){
				GameObject.Find ("Scene").transform.FindChild("SelectMember").gameObject.SetActive (false);		//次シーンをアクティブに
				GameObject.Find("SetMember").transform.FindChild("Explanation").gameObject.SetActive(true);
				//各キャラのオブジェに設けられたMouseFlagをtrueに
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
				GameObject.Find("SetMember").transform.FindChild("SetPosition").gameObject.SetActive(true);
				//GameObject.Find("SetPosition").transform.FindChild("Set2").gameObject.SetActive(false);
				gameScene = scene.SELECT_CHARA;			//次シーンへ移行
			}
			break;

		//レースに参加するキャラを選択（次処理はスクリプト「Mouse.cs」「test.cs」）
		case scene.SELECT_CHARA:

			if(this.finFlag){
				try{
					GameObject.Find ("tesd1").GetComponent<Mouse>().MouseFlag=false;
				}catch{}
				try{
					GameObject.Find ("tesd2").GetComponent<Mouse>().MouseFlag=false;
				}catch{}
				try{
					GameObject.Find ("tesd3").GetComponent<Mouse>().MouseFlag=false;
				}catch{}
				try{
					GameObject.Find ("tesd4").GetComponent<Mouse>().MouseFlag=false;
				}catch{}
				try{
					GameObject.Find ("tesd5").GetComponent<Mouse>().MouseFlag=false;
				}catch{}
				try{
					GameObject.Find ("Friend").GetComponent<Mouse>().MouseFlag=false;
				}catch{}
				gameScene = scene.RACE;
				this.finFlag = false;
				GameObject.Find ("Scene").transform.FindChild("Race").gameObject.SetActive(true);
				transform.FindChild("Score").gameObject.SetActive(true);
			}
			break;

		//レース処理（実処理はスクリプト「Race.cs」）
		case scene.RACE:
			if(finFlag){
				gameScene = scene.GOAL;
				GameObject.Find("Scene").transform.FindChild("Goal").gameObject.SetActive(true);
			}
			break;

		case scene.GOAL:
			if(finFlag){
				//Instantiate(prefab);
				//prefab.GetComponent<SaveNum>().SaveNumber = GameObject.Find("Race").GetComponent<Race>().GetRanking;
				
				num = GameObject.Find("Race").GetComponent<Race>().GetRanking;
				GameObject.Find("SaveNum").GetComponent<SaveNum>().SaveNumber = num;
				CameraFade.StartAlphaFade (Color.black, false, 2f, 3f, () => { Application.LoadLevel ("end"); });
				gameScene = scene.END;
			}
			break;
		case scene.END:
			break;
		}
	}

	//Race.csからアクセス　シーンをSELECT_CHARAへと移行
	public void GameScene(){
		this.gameScene = scene.SELECT_CHARA;
	}

	//外部ファイルから読み込んだ各キャラステータスをセットするメソッド
	private void ExtractStatus(string objname){
		int i = this.statusList.IndexOf (objname);			//StatusListの各要素からオブジェクト名を検索	ヒット時には要素番号を取得

		GameObject.Find (objname).GetComponent<Mouse> ().GetATK = (int)(int.Parse(this.statusList [i + 1])*Random.Range(0.2f,2.0f));			//オブジェクト名１行下よりATKを取得（外部テキストファイル参照）
		GameObject.Find (objname).GetComponent<Mouse> ().GetStamina = int.Parse(this.statusList [i + 2]);		//オブジェクト名2行下よりスタミナを取得（外部テキストファイル参照）
	}

	
	//外部ファイル読込処理をするメソッド
	public IEnumerator LoadText(){

		string txtBuffer= string.Empty;			//読み込んだテキスト情報1行の一時保管フィールド
		string textFileName = "deck.txt";		//読み込む外部テキストファイルのファイル名
		string Path = string.Empty;				//読み込む外部テキストファイルの相対パス収納フィールド

		Path = Application.dataPath + "/StreamingAssets" + "/" + textFileName;		//読み込む外部テキストファイルのパスを取得


		if (Application.isEditor) {
			StreamReader sr = new StreamReader (Path, System.Text.Encoding.GetEncoding ("utf-8"));		//”utf-8”に変換してテキスト情報を取得
			yield return sr;
			
			//取り出したテキスト情報を1行ずつリスト「statusList」に収納
			while ((txtBuffer = sr.ReadLine()) != null) {
				this.statusList.Add (txtBuffer);
			}
		} else if (Application.isWebPlayer) {
			StartCoroutine(WebLoad(Path));
		} else if (Application.platform == RuntimePlatform.Android) {
			using (UnityEngine.WWW www = new UnityEngine.WWW(Path)) {
				yield return www;
				string line = string.Empty;

				try {
					StringReader SR = new StringReader (www.text);
					while ((line = SR.ReadLine()) != null) {
						statusList.Add (line);
					}
				} catch {
					Application.Quit ();
				}
			}
		}



		this.loaded = true;		//外部テキストファイル読込終了フラグをtrueに
	}

	//WebPlayer用メソッド
	IEnumerator WebLoad(string URL){

		WWW www = new WWW(URL);
		yield return www;
		
		string TEXTSTR = string.Empty;

		try{

			StringReader webSR = new StringReader(www.text);
			while((TEXTSTR = webSR.ReadLine()) != null){
				statusList.Add(TEXTSTR);
			}
		}catch{
			Application.Quit();
		}
	}


	//最大公約数を求める
	static int Gcd(int a, int b){
		if (a < b)
			return Gcd (b, a);
		int d = 0;
		do {
			d = a % b;
			a = b;
			b = d;
		} while(d != 0);
		return a;
	}

	//------------------------------------------------------------------------
	//set get
	//------------------------------------------------------------------------
	public bool FinFlag{
		set{
			this.finFlag = value;
		}
	}

	public int MemberNumber {
		set {
				this.memberNumber = value;
		}
		get{
			return this.memberNumber;
				}
	}

	public bool FadeFlag{
		set{
			this.fadeFlag = value;
		}
		get{
			return this.fadeFlag;
		}
	}
	
}
