using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetChar : MonoBehaviour {

	public GameObject word;
	public GameObject hangman;
	public GameObject youWin;
	public GameObject gameOver;
	public GameObject buttons;
	public GameObject reset;
	static int charCounter =0;
	static int missCounter =0;
	// Use this for initialization
	void Start () {
		
	}

	void Awake(){
		word = GameObject.Find ("Word");
		hangman = GameObject.Find ("Hangman");
		youWin = GameObject.Find ("YouWin");
		gameOver = GameObject.Find ("GameOver");
		buttons = GameObject.Find ("Buttons");
		reset = GameObject.Find ("Reset");
	}

	// Update is called once per frame
	void Update () {
		
	}
	public void OnClick(){
		GameObject child = transform.GetChild (0).gameObject;
		Text text = child.GetComponent<Text> ();
		Text wordText = word.GetComponent<Text> ();
		GetWord gameWord = word.GetComponent<GetWord> ();
		if (!gameWord.CheckChar (text.text)) {
			missCounter++;
			hangman.GetComponent<UpdateHangman> ().DrawNextStage ();
			if (missCounter == 11) {
				Debug.Log ("GameOver");
				gameOver.SetActive (true);

				string s = "";
				foreach (char c in gameWord.word) {
					s += c.ToString () + " ";
				}
				wordText.text = s.Trim ();
				wordText.color = new Color(1,0,0);
				wordText.fontStyle = FontStyle.Bold;
				endGame ();
			}
		} else {
			charCounter++;
			Debug.Log ("Increment Char Counter:  " + charCounter.ToString());
			if (wordText.text.Replace(" ",string.Empty) == gameWord.word) {
				Debug.Log ("YouWin");
				youWin.SetActive (true);
				wordText.color = new Color (0, 1, 0);
				wordText.fontStyle = FontStyle.Bold;
				endGame ();

			}
		}
		
		gameObject.SetActive (false);
	}

	void ClearKeys(){
		for (int i = 0; i < 26; i++) {
			buttons.transform.GetChild (i).gameObject.SetActive (false);
		}

	}

	void endGame(){
		ClearKeys ();
		reset.SetActive (true);
		missCounter = 0;
		charCounter = 0;
	}
}
