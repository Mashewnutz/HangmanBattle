using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightRank : MonoBehaviour {

	public Leaderboard leaderboard;
	public GameObject scores;
	public GameObject outlier;

	void Awake() {
		leaderboard = FindObjectOfType<Leaderboard> ();
	}

	// Use this for initialization
	void Start () {		
		StartCoroutine(leaderboard.GetRank (OnSuccess, OnFailed));
	}
	
	// Update is called once per frame
	void OnSuccess (int rank) {
		gameObject.SetActive (true);
		if (rank < leaderboard.topScores.Scores.Length) {
			outlier.SetActive (false);
			var child = scores.transform.GetChild (rank);
			SetHighlight(child);
		} else {
			outlier.SetActive (true);
			var texts = outlier.GetComponentsInChildren<Text> ();
			texts [0].text = rank.ToString ();
			texts [1].text = leaderboard.userName;
			texts [2].text = Game.bestStreak.ToString ();
				

			SetHighlight(outlier.transform);
		}
	}

	void OnFailed() {
		Debug.Log ("Failed to refresh rank");
		gameObject.SetActive (false);
	}

	void SetHighlight(Transform entry){
		transform.position = entry.position;
		var texts = entry.GetComponentsInChildren<Text> ();
		foreach (Text text in texts) {
			text.color = Color.black;
		}

		if (Game.currentStreak == Game.bestStreak) {
			GetComponent<Image> ().color = Color.yellow;
		}
	}
}
