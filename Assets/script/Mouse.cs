using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour{
	
	private bool resetFlag = true;						//各キャラオブジェを定位置へリセットする判定フラグ
	private Vector3 vec = new Vector3(0, 0, 0);			//選択された各キャラの定位置を収納
	private bool mouseFlag = false;						//マウスの各ボタンが反応するかの判定フラグ
	private int ATK;									//各キャラのATKステータス
	private int stamina;								//各キャラのスタミナステータス
	static  bool setFlag = false;						//レース参加キャラの選択状況判定フラグ（false：未選択　　ture:1人選択）

	void Start(){
		setFlag = false;
	}

	void Update(){
		this.resetFlag = true;			//ドラッグ状態にあるキャラオブジェがセットポジションに入っていなければ,リセットフラグをtrueに
	}
	
	void OnMouseDown(){
		//mouseFlagがtrueならば右クリックボタン押下で選択された各キャラオブジェの定位置座標を取得
		if (this.mouseFlag) {
			this.vec = gameObject.transform.position;
		}
	}
	
	void OnMouseDrag(){
		//mouseFlagがtrueならば右クリックボタン押下状態下でのドラッグ時のカーソルのスクリーン座標を取得
		if (this.mouseFlag) {
			gameObject.transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}
	
	void OnMouseUp(){
		//mouseFlagがtrue状態にて処理
		if (this.mouseFlag) {
			//resetFlagがtrue状態でのマウス右ボタンアップで選択状態の各キャラオブジェを定位置にリセット
			if (this.resetFlag) {
				gameObject.transform.position = this.vec;
			//resetFlagがflase状態でのマウス右ボタンアップで選択状態の各キャラオブジェをセット
			} else{
				//レース参加キャラ未選択時での処理
				if( !setFlag ){
					setFlag = true;		//レース参加キャラ選択状況を1人に変更
					GameObject.Find("SetPosition").transform.FindChild("set2").gameObject.SetActive(true);
					GameObject.Find("set2").GetComponent<BoxCollider2D>().enabled = true;		//セットポジション2のコライダーをtrueに
					GameObject.Find("set1").GetComponent<BoxCollider2D>().enabled = false;		//セットポジション１のコライダーをfalseに
					//選択したキャラのオブジェ情報引き渡し先オブジェRaceをアクティブに
					GameObject obj =  GameObject.Find("Scene").transform.FindChild("Race").gameObject;
					obj.SetActive(true);
					//選択したオブジェ情報を渡してRaceをパッシブに
					obj.GetComponent<Race>().SetMember1 = gameObject;
					obj.SetActive(false);
					//セットされたキャラオブジェのコライダーをfalseに
					gameObject.GetComponent<BoxCollider2D>().enabled = false;
					GameObject.Find("Explanation").transform.FindChild("Leader").gameObject.SetActive(false);
					GameObject.Find("Explanation").transform.FindChild("Supporter").gameObject.SetActive(true);
				}
				//レース参加キャラ1人選択時での処理
				else{
					//選択したキャラのオブジェ情報引き渡し先オブジェRaceをアクティブに
					GameObject obj = GameObject.Find ("Scene").transform.FindChild("Race").gameObject;
					obj.SetActive(true);
					//選択したオブジェ情報を渡してRaceをパッシブに
					obj.GetComponent<Race>().SetMember2 = gameObject;
					obj.SetActive(false);
					//GameMainのシーン終了フラグをtrueに
					GameObject.Find("GameMain").GetComponent<GameMain>().FinFlag = true;
					GameObject.Find("SetMember").transform.FindChild("Explanation").gameObject.SetActive(false);
					
				}
			}
		}
	}

	//各キャラオブジェのコライダーがセットポジションのコライダーと接触している状況ではresetFlagはfalseに
	void OnTriggerEnter2D(Collider2D coll){
		this.resetFlag = false;
		//各キャラオブジェのポジションをセットポジションの座標に同期
		gameObject.transform.position = coll.gameObject.transform.position;
	}

	//--------------------------------------------------------------------------------------
	//set get
	//--------------------------------------------------------------------------------------
	public bool MouseFlag{
		set{
			this.mouseFlag = value;
		}
	}
	
	public int GetATK{

		get{
			return this.ATK;		
		}
		set{
			this.ATK = value;
				}
		
	}
	
	public int GetStamina{
		get{
			return this.stamina;		
		}
		set{
			this.stamina = value;
				}
	}
}
/*
public class Mouse : MonoBehaviour {
	bool[] isClicked = new bool[6];
	Vector2 currentPoint;

	private bool mouseFlag;

	float[] X=new float[6];
	float[] Y=new float[6];

	float[] setPositionX = new float[2];
	float[] setPositionY = new float[2];

	public int i;

	bool[] hit = new bool[6];


	void Start()
	{
		mouseFlag = false;
		for (int m=0; m<6; m++) {
			isClicked[m]=false;
			hit[m]=false;
				}

		X[i] = transform.localPosition.x;
		Y[i] = transform.localPosition.y;
		Debug.Log(X[i]);

		setPositionX [0] = GameObject.Find ("set").transform.localPosition.x;
		setPositionX [1] = GameObject.Find ("set2").transform.localPosition.x;
		setPositionY [0] = GameObject.Find ("set").transform.localPosition.y;
		setPositionY [1] = GameObject.Find ("set2").transform.localPosition.y;

	}

	// Update is called once per frame
	void Update () {
		if (hit [i] == true) {
			gameObject.transform.position = new Vector2 (X[i],Y[i]);
		}
		
		if (Input.GetMouseButtonDown (0) && !hit[i]) {
			
			OnMouseDown ();
		}
		if (Input.GetMouseButton (0)&& !hit[i]) {
			OnMouseDrag ();
		}
		if (Input.GetMouseButtonUp (0)&& !hit[i]) {
			OnMouseUp ();
		}




	}

	void OnMouseDown(){
		if (this.mouseFlag) {
			Debug.Log ("OnMouseDown");
			Vector3 screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
			Vector3 newVector = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		
			Vector2 tapPoint = new Vector2 (newVector.x, newVector.y);
			Collider2D colition2d = Physics2D.OverlapPoint (tapPoint);

						//	Debug.Log (screenPoint);
						//	Debug.Log (newVector);
						//	Debug.Log (tapPoint);
						//	Debug.Log (colition2d);

			if (colition2d) {
				RaycastHit2D hitObject = Physics2D.Raycast (tapPoint, -Vector2.up);
				if (hitObject.collider.gameObject.name == gameObject.name) {
					Debug.Log ("hit object is " + hitObject.collider.gameObject.name);
					currentPoint = new Vector2 (tapPoint.x, tapPoint.y);
					isClicked [i] = true;
				}
			}
		}
	}
	
	void OnMouseDrag(){
				if (!isClicked [i]) {
						return;
				}
				if (this.mouseFlag) {
						Debug.Log ("OnMouseDrag");
						Vector3 screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
						Vector3 newVector = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		
						Vector2 tapPoint = new Vector2 (newVector.x, newVector.y);
						gameObject.transform.position = new Vector2 (gameObject.transform.position.x + (tapPoint.x - currentPoint.x), gameObject.transform.position.y + (tapPoint.y - currentPoint.y));
						currentPoint = tapPoint;
				
				}
			
		}
	
	void OnMouseUp(){
		if (this.mouseFlag) {
						isClicked [i] = false;
						gameObject.transform.position = new Vector2 (X [i], Y [i]);
				}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag =="set" && test.set[0]) {
			Debug.Log("hit");
			X[i]=setPositionX[0];

			Y[i]=setPositionY[0];
			hit[i]=true;
			test.set[0]=false;
			//test.set[0]=false;
		}

		if (coll.gameObject.tag =="set2" && test.set[1]) {
			Debug.Log("hit");
			X[i]=setPositionX[1];
			
			Y[i]=setPositionY[1];
			hit[i]=true;
			test.set[1]=false;
		}
		//Destroy (gameObject);
		
	}

	public bool MouseFlag{
		set{
			this.mouseFlag = value;
		}
	}

}
*/

