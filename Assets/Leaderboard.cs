using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Leaderboard : MonoBehaviour {

	[Serializable]
	public struct ScoreEntry{
		public int Score;
		public string Member;
	}

	[Serializable]
	public struct TopScores {
		public ScoreEntry []Scores;
	}
		
	public string serverUrl;
	public string leaderboardName;
	public string userName;
	public int size;
	public int rank;
	public TopScores topScores;

	void Start() {
		if (FindObjectsOfType<Leaderboard> ().Length > 1) {
			Destroy (gameObject);
		}
	}

	public IEnumerator FetchScores(UnityAction<TopScores> success, UnityAction<string> failed) {
		WWW www = new WWW(serverUrl + "/gettopscores?count=" + size);
		yield return www;

		if (String.IsNullOrEmpty(www.error)) {
			topScores = JsonUtility.FromJson<TopScores> (www.text);
			if(success != null)
				success (topScores);
		} else {
			if(failed != null)
				failed (www.error);
		}
	}

	public IEnumerator PostScore(int score, UnityAction success, UnityAction<string> failed){
		ScoreEntry entry;
		entry.Score = score;
		entry.Member = userName;

		var json = JsonUtility.ToJson (entry);
		byte[] bytes = new byte[json.Length];

		for (int i = 0; i < json.Length; ++i)
			bytes [i] = (byte)(json [i]);

		Debug.Log ("Sending:" + bytes.ToString ());
		WWW www = new WWW (serverUrl + "/postscore", bytes);
		yield return www;

		if (String.IsNullOrEmpty (www.error)) {
			if(success != null)
				success ();
		} else {
			if(failed != null)
				failed (www.error);
		}
	}

	public IEnumerator GetRank(UnityAction<int> success, UnityAction failed){
		WWW www = new WWW (serverUrl + "/getrank?user=" + userName);
		yield return www;
		Debug.Log("Received rank:" + www.text);

		if (String.IsNullOrEmpty (www.error)) {			
			if (int.TryParse (www.text, out rank)) {
				if(success != null)
					success (rank);
			} else {
				if(failed != null)				
					failed ();
			}
		} else {
			if(failed != null)
				failed ();
		}
	}

	public IEnumerator GetScore(UnityAction<int> success, UnityAction failed){
		WWW www = new WWW (serverUrl + "/getscore?user=" + userName);
		yield return www;
		Debug.Log("Received score:" + www.text);

		if (String.IsNullOrEmpty (www.error)) {
			int score;
			if (int.TryParse (www.text, out score)) {
				if(success != null)
					success (score);
			} else {
				if(failed != null)
					failed ();
			}
		} else {
			if(failed != null)
				failed ();
		}
	}

	public IEnumerator PostAndRefreshTopScores(int score, UnityAction success, UnityAction failed){
		var fetchScores = FetchScores (null, null);
		var postScore = PostScore (score, null, null);
		var getRank = GetRank (null, null);

		while (postScore.MoveNext () || getRank.MoveNext () || fetchScores.MoveNext()) {
			yield return null;
		}
	}
}
