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
	public GameObject nextBtn;
	public GameObject resultsBtn;
	public GameObject restartBtn;
	public GameObject info;
	public GameObject current;
	public Leaderboard leaderboard;

	public static int bestStreak =0;
	public static int currentStreak = 0;
	public static int missCounter =0;

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
		nextBtn = GameObject.Find ("Next");
		resultsBtn = GameObject.Find ("Results");
		restartBtn = GameObject.Find ("Restart");
		info = GameObject.Find ("Info");
		current = GameObject.Find ("CurrentStreak");
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
		nextBtn.SetActive (won);
		resultsBtn.SetActive (!won);
		resultsBtn.GetComponent<Button> ().interactable = false;
		restartBtn.SetActive (!won);

		missCounter = 0;
		current.SetActive (true);
		Text currentText = current.GetComponent<Text> ();
		if (won) {			
			currentText.text = "Streak: " + currentStreak.ToString ();
			if (currentStreak > bestStreak) {
				currentText.color = Color.yellow;
			}
		} else {			
			bestStreak = Mathf.Max (currentStreak, bestStreak);
			postScore (bestStreak);
			PlayerPrefs.SetInt ("BestStreak", bestStreak);
			PlayerPrefs.Save ();

			currentText.text = "Streak: " + currentStreak.ToString ();
			if (currentStreak >= bestStreak) {
				currentText.color = Color.yellow;
				var colors = resultsBtn.GetComponent<Button> ().colors;
				colors.normalColor = Color.yellow;
				resultsBtn.GetComponent<Button> ().colors = colors;
			}
		}
	}

	void postScore(int streak){
		StartCoroutine (leaderboard.PostAndRefreshTopScores (streak, OnSuccess, OnFailed));
	}

	void OnSuccess(){
		resultsBtn.GetComponent<Button> ().interactable = true;
	}

	void OnFailed() {
		Debug.Log ("Failed to refresh:");
	}
}
