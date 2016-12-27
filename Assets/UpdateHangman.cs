using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateHangman : MonoBehaviour {
	int stage = -1;
	// Use this for initialization
	public GameObject gameOver;
	void Start () {
		foreach (Transform child in transform) {
			child.gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DrawNextStage(){
		stage++;
		if (stage <= 10) {
			transform.GetChild (stage).gameObject.SetActive (true);
		}
	}
}
