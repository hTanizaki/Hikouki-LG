using UnityEngine;
using System.Collections;

public class EndMain : MonoBehaviour {


	private GameObject saveNumPrefab;
	private GameObject ChoiceRank;
	public UIWidget wg;
	public UIWidget wg2;
	private float fAlpha = 0.1f;
	private int num;
	private bool trueRank = false;
	private bool trueRankText = false;
	
	// Use this for initialization
	void Start () {
		//saveNumPrefab = (GameObject)Instantiate(Resources.Load("SaveNum"));
		//CameraFade.StartAlphaFade (Color.black, false, 2f, 3f, () => { Application.LoadLevel ("test"); });
		//saveNumPrefab = GameObject.Find ("SaveNum");
		num = GameObject.Find ("SaveNum").GetComponent<SaveNum>().SaveNumber;
		StartCoroutine("SetRank");
	}
	
	// Update is called once per frame
	void Update () {
		//num = saveNumPrefab.GetComponent<SaveNum> ().SaveNumber;
		//Debug.Log (num);
		if (trueRankText) {
			if (wg2.alpha < 1f) {
				GameObject.Find ("Rank").GetComponent<Fade_Rank> ().FadeAlphaRank (wg2);
			}
		}
		if(trueRank){
			if(wg.alpha < 1f){
				GameObject.Find("Rank").GetComponent<Fade_Rank>().FadeAlphaRank(wg);
			}
		}
	}

	IEnumerator SetRank(){

		//StartCoroutine("Wait", 5f);
		//yield return new WaitForSeconds (2f);
		GameObject.Find("Panel").transform.FindChild("Rank").gameObject.SetActive(true);
		GameObject.Find("Panel").transform.FindChild("RankText").gameObject.SetActive(true);
		wg2 = GameObject.Find("Panel").transform.FindChild("RankText").GetComponent<UIWidget>();
		trueRankText = true;
		//StartCoroutine("Wait", 2f);
		yield return new WaitForSeconds(3f);
		if (num >= 60) {
			GameObject.Find ("Rank").transform.FindChild("DRank").gameObject.SetActive (true);
			wg = GameObject.Find ("Rank").transform.FindChild("DRank").GetComponent<UIWidget>();
		} else if (num >= 40) {
			GameObject.Find ("Rank").transform.FindChild("CRank").gameObject.SetActive (true);
			wg = GameObject.Find ("Rank").transform.FindChild("CRank").GetComponent<UIWidget>();
		} else if (num >= 20) {
			GameObject.Find ("Rank").transform.FindChild("BRank").gameObject.SetActive (true);
			wg = GameObject.Find ("Rank").transform.FindChild("BRank").GetComponent<UIWidget>();
		}else if (num > 10) {
			GameObject.Find ("Rank").transform.FindChild("ARank").gameObject.SetActive (true);
			wg = GameObject.Find ("Rank").transform.FindChild("ARank").GetComponent<UIWidget>();
		} else if (num > 0) {
			GameObject.Find ("Rank").transform.FindChild("SRank").gameObject.SetActive (true);
			wg = GameObject.Find ("Rank").transform.FindChild("SRank").GetComponent<UIWidget>();
		}
		trueRank = true;
	}

	void BackTitle(){
		GameObject.Find ("SaveNum").GetComponent<SaveNum> ().DesSaveNum ();
		CameraFade.StartAlphaFade (Color.black, false, 2f, 1f, () => { Application.LoadLevel ("title"); });
	}

	IEnumerator Wait(float Wnum){
		Debug.Log ("1");
		yield return new WaitForSeconds(Wnum);
	}

	public void FadeAlphaRank(UIWidget w){
		Debug.Log ("wid"+fAlpha+"aa"+w.alpha);
		if(w.alpha < 255f){
			w.alpha += fAlpha;
			Debug.Log ("wid"+fAlpha+"aa"+w.alpha);
		}
	}
}
