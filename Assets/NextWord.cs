﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextWord : MonoBehaviour {

	public void Next(){
		SceneManager.LoadScene ("Hangman");
	}
}
