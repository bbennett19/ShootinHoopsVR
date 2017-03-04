using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreKeeper : MonoBehaviour {

    private UnityEngine.UI.Text scoreText;
	// Use this for initialization
	void Start () {
        scoreText = GetComponentInChildren<Text>();
	}
	
	public void IncrementScore()
    {
        scoreText.text = (Convert.ToInt32(scoreText.text)+1).ToString();
    }
}
