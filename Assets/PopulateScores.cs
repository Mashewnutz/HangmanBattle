using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PopulateScores : MonoBehaviour {

	public Leaderboard leaderboard;
	public GameObject leaderboardEntryPrefab;

	void Awake() {
		leaderboard = FindObjectOfType<Leaderboard> ();
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (leaderboard.FetchScores (OnSuccess, OnFailed));
	}
	
	void OnSuccess(Leaderboard.TopScores scores){
		int rank = 1;
		foreach (Leaderboard.ScoreEntry score in scores.Scores) {
			var leaderboardEntry = GameObject.Instantiate (leaderboardEntryPrefab);
			var texts = leaderboardEntry.GetComponentsInChildren<Text> ();
			texts [0].text = rank.ToString ();
			texts [1].text = score.Member;
			texts [2].text = score.Score.ToString();
			leaderboardEntry.transform.SetParent (transform, false);
			rank++;
		}
	}

	void OnFailed(string error) {
		Debug.LogWarning ("Failed to connect to server: " + error);
	}
}
