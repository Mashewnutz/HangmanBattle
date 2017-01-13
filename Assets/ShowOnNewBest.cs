using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnNewBest : MonoBehaviour {
	void Start () {
		if (Game.currentStreak == Game.bestStreak) {
			gameObject.SetActive (true);
		} else {
			gameObject.SetActive (false);
		}
	}
}
