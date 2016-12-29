using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetChar : MonoBehaviour {

	Game game;
	Text text;

	void Awake(){
		game = GameObject.Find ("Game").GetComponent<Game>();
		text = transform.GetChild (0).gameObject.GetComponent<Text>();
	}


	// Update is called once per frame
	void Update () {
		for (KeyCode key = KeyCode.A; key < KeyCode.Z; key++) {
			if (Input.GetKeyDown (key)) {
				char character = (char)((key - KeyCode.A) + 'A');
				if (character.ToString () == text.text) {
					game.CheckCharacter (character.ToString ());
					gameObject.SetActive (false);
				}
			}	
		}
	}

	public void OnClick(){		
		game.CheckCharacter(text.text);
		gameObject.SetActive (false);
	}
}
