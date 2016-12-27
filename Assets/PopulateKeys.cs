using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopulateKeys : MonoBehaviour {
	public GameObject buttonPrefab;

	// Use this for initialization
	void Awake () {
		for (int i = 0; i < 26; i++) {
			int collumn = i%9;
			int centerThirdRow = 0;
			int row = 0;
			if (i > 8) {
				row = 1;
			}
			if(i > 17) {
				row = 2;
				centerThirdRow = 57/2;
			}
			GameObject button = GameObject.Instantiate (buttonPrefab,new Vector3((20+centerThirdRow)+(collumn*(57)),20-(row*57),0),Quaternion.identity);
			button.transform.SetParent (gameObject.transform,false);
			GameObject text = button.transform.GetChild (0).gameObject;
			Text textComponent = text.GetComponent<Text> ();
			textComponent.text = ((char)('A' + i)).ToString();
			button.name = textComponent.text;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
