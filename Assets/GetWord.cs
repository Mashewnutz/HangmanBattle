using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetWord : MonoBehaviour {
	public string word = "MUSIC";
	string hiddenWord = "";
	Text text;
	int win;


	public string url = "http://www.setgetgo.com/randomword/get.php";

	void Awake() {
		text = gameObject.GetComponent<Text> ();
		text.text = "";
	}

	// Use this for initialization
	void Start () {		
		word = LoadWord ().ToUpper();

		for (int i = 0; i < word.Length; i++) {
			hiddenWord += "_ ";
		}
		hiddenWord.Trim ();
		text.text = hiddenWord;
	}

	string LoadWord () {
		TextAsset txt = Resources.Load<TextAsset>("words");
		string[] dict = txt.text.Split("\n"[0]);
		int index = Random.Range(0, dict.Length-1);
		Debug.Log (dict [index]);
		return dict [index];
	}
		

	public bool CheckChar(string c){
		//Debug.Log(c);
		if (word.Contains(c)) {
			char[] charArray = text.text.ToCharArray();
			char[] wordCharArray = word.ToCharArray ();
			for(int i = 0; i < word.Length; i++){
				if (wordCharArray[i].ToString() == c) {
					charArray [2 * i] = c.ToCharArray()[0];
				}
				string s = new string(charArray);
				text.text = s;
			}
			return true;
		} else {
			return false;
		}
	}
}