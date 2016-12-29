using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Game : MonoBehaviour {

	public GameObject word;
	public GameObject hangman;
	public GameObject youWin;
	public GameObject gameOver;
	public GameObject buttons;
	public GameObject reset;
	public GameObject info;
	public GameObject current;
	public GameObject best;

	static int bestStreak =0;
	static int currentStreak = 0;
	static int missCounter =0;

	// Use this for initialization
	void Start () {
		bestStreak = PlayerPrefs.GetInt ("BestStreak", 0);
	}

	void Awake(){
		word = GameObject.Find ("Word");
		hangman = GameObject.Find ("Hangman");
		youWin = GameObject.Find ("YouWin");
		gameOver = GameObject.Find ("GameOver");
		buttons = GameObject.Find ("Buttons");
		reset = GameObject.Find ("Reset");
		info = GameObject.Find ("Info");
		current = GameObject.Find ("CurrentStreak");
		best = GameObject.Find ("BestStreak");
	}

	public void CheckCharacter(string character){
		info.SetActive (false);
		Text wordText = word.GetComponent<Text> ();
		GetWord gameWord = word.GetComponent<GetWord> ();
		if (!gameWord.CheckChar (character)) {
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
				endGame (false);
				currentStreak = 0;


			}
		} else {
			if (wordText.text.Replace(" ",string.Empty) == gameWord.word) {
				Debug.Log ("YouWin");
				youWin.SetActive (true);
				currentStreak++;
				wordText.color = Color.green;
				wordText.fontStyle = FontStyle.Bold;
				endGame (true);

			}
		}
	}

	void ClearKeys(){
		for (int i = 0; i < 26; i++) {
			buttons.transform.GetChild (i).gameObject.SetActive (false);
		}

	}

	void endGame(bool won){
		ClearKeys ();
		reset.SetActive (true);
		GameObject resetChild = reset.transform.GetChild(0).gameObject;
		Text resetText = resetChild.GetComponent<Text> ();

		missCounter = 0;
		current.SetActive (true);
		Text currentText = current.GetComponent<Text> ();
		if (won) {
			resetText.text = "Next";
			currentText.text = "Current Streak: " + currentStreak.ToString ();
			if (currentStreak > bestStreak) {
				currentText.color = Color.yellow;
			}
		} else {			
			bestStreak = Mathf.Max (currentStreak, bestStreak);
			PlayerPrefs.SetInt ("BestStreak", bestStreak);
			PlayerPrefs.Save ();

			resetText.text = "Play again?";
			currentText.text = "Current Streak: " + currentStreak.ToString ();
			best.GetComponent<Text>().text = "Best Streak: " + bestStreak.ToString();
			best.SetActive (true);
			if (currentStreak >= bestStreak) {
				currentText.color = Color.yellow;
				best.GetComponent<Text> ().color = Color.yellow;
			}
		}
	}
}
